using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Repositories
{
    public class WareItemRepository : IWareItemRepository
    {
        private readonly HyggyContext _context;

        public WareItemRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<WareItem?> GetById(long id)
        {
            return await _context.WareItems.FindAsync(id);
        }
        public async Task<IEnumerable<WareItem>> GetByArticle(long article)
        {
            return await _context.WareItems.Where(x => x.Ware.Article == article).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareId(long wareId)
        {
            return await _context.WareItems.Where(x => x.Ware.Id == wareId).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareName(string wareName)
        {
            return await _context.WareItems.Where(x => x.Ware.Name.Contains(wareName)).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareDescription(string wareDescription)
        {
            return await _context.WareItems.Where(x => x.Ware.Description.Contains(wareDescription)).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWarePriceRange(float minPrice, float maxPrice)
        {
            if(maxPrice < minPrice)
            {
                return await _context.WareItems.Where(x => x.Ware.Price >= maxPrice && x.Ware.Price <= minPrice).ToListAsync();
            }
            if(minPrice == maxPrice)
            {
                return await _context.WareItems.Where(x => x.Ware.Price == minPrice).ToListAsync();
            }
            return await _context.WareItems.Where(x => x.Ware.Price >= minPrice && x.Ware.Price <= maxPrice).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetPagedWareItems(int pageNumber, int pageSize)
        {
            return await _context.WareItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareDiscountRange(float minDiscount, float maxDiscount)
        {
            if (maxDiscount < minDiscount)
            {
                return await _context.WareItems.Where(x => x.Ware.Discount >= maxDiscount && x.Ware.Discount <= minDiscount).ToListAsync();
            }
            if (minDiscount == maxDiscount)
            {
                return await _context.WareItems.Where(x => x.Ware.Discount == minDiscount).ToListAsync();
            }
            return await _context.WareItems.Where(x => x.Ware.Discount >= minDiscount && x.Ware.Discount <= maxDiscount).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareStatusId(long statusId)
        {
            return await _context.WareItems.Where(x => x.Ware.Statuses.Any(st => st.Id == statusId) ).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareCategory3Id(long wareCategory3Id)
        {
            return await _context.WareItems.Where(x => x.Ware.WareCategory3.Id == wareCategory3Id).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareCategory2Id(long wareCategory2Id)
        {
            return await _context.WareItems.Where(x => x.Ware.WareCategory3.WareCategory2.Id == wareCategory2Id).ToListAsync();
        }
        public async Task<IEnumerable<WareItem>> GetByWareCategory1Id(long wareCategory1Id)
        {
            return await _context.WareItems.Where(x => x.Ware.WareCategory3.WareCategory2.WareCategory1.Id == wareCategory1Id).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByWareImageId(long wareImageId)
        {
            return await _context.WareItems.Where(x => x.Ware.Images.Any(y => y.Id == wareImageId)).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByPriceHistoryId(long priceHistoryId)
        {
            return await _context.WareItems.Where(x => x.Ware.PriceHistories.Any(prh => prh.Id == priceHistoryId)).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByOrderItemId(long orderItemId)
        {
            return await _context.WareItems.Where(x => x.Ware.OrderItems.Any(ordi => ordi.Id == orderItemId)).ToListAsync();

        }

        public async Task<IEnumerable<WareItem>> GetByIsDeliveryAvailable(bool isDeliveryAvailable)
        {
            return await _context.WareItems.Where(x => x.Ware.IsDeliveryAvailable == isDeliveryAvailable).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByStorageId(long storageId)
        {
            return await _context.WareItems.Where(x => x.Storage.Id == storageId).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByShopId(long shopId)
        {
            return await _context.WareItems.Where(x => x.Storage.Shop.Id == shopId).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByQuantityRange(long minQuantity, long maxQuantity)
        {
            if (maxQuantity < minQuantity)
            {
                return await _context.WareItems.Where(x => x.Quantity >= maxQuantity && x.Quantity <= minQuantity).ToListAsync();
            }
            if (minQuantity == maxQuantity)
            {
                return await _context.WareItems.Where(x => x.Quantity == minQuantity).ToListAsync();
            }
            return await _context.WareItems.Where(x => x.Quantity >= minQuantity && x.Quantity <= maxQuantity).ToListAsync();
        }

        public async Task<IEnumerable<WareItem>> GetByQuery(WareItemQueryDAL query)
        {
            var wareCollections = new List<IEnumerable<WareItem>>();

            if(query.Id != null)
            {
                wareCollections.Add(new List<WareItem> { await GetById(query.Id.Value) });
            }

            if (query.Article != null)
            {
                wareCollections.Add(await GetByArticle(query.Article.Value));
            }

            if (query.WareId != null)
            {
                wareCollections.Add(await GetByWareId(query.WareId.Value));
            }

            if (query.WareName != null)
            {
                wareCollections.Add(await GetByWareName(query.WareName));
            }

            if (query.WareDescription != null)
            {
                wareCollections.Add(await GetByWareDescription(query.WareDescription));
            }

            if (query.MinPrice != null && query.MaxPrice != null)
            {
                wareCollections.Add(await GetByWarePriceRange(query.MinPrice.Value, query.MaxPrice.Value));
            }

            if (query.MinDiscount != null && query.MaxDiscount != null)
            {
                wareCollections.Add(await GetByWareDiscountRange(query.MinDiscount.Value, query.MaxDiscount.Value));
            }

            if (query.StatusId != null)
            {
                wareCollections.Add(await GetByWareStatusId(query.StatusId.Value));
            }

            if (query.WareCategory3Id != null)
            {
                wareCollections.Add(await GetByWareCategory3Id(query.WareCategory3Id.Value));
            }

            if (query.WareCategory2Id != null)
            {
                wareCollections.Add(await GetByWareCategory2Id(query.WareCategory2Id.Value));
            }

            if (query.WareCategory1Id != null)
            {
                wareCollections.Add(await GetByWareCategory1Id(query.WareCategory1Id.Value));
            }

            if (query.WareImageId != null)
            {
                wareCollections.Add(await GetByWareImageId(query.WareImageId.Value));
            }

            if (query.PriceHistoryId != null)
            {
                wareCollections.Add(await GetByPriceHistoryId(query.PriceHistoryId.Value));
            }

            if (query.OrderItemId != null)
            {
                wareCollections.Add(await GetByOrderItemId(query.OrderItemId.Value));
            }

            if (query.IsDeliveryAvailable != null)
            {
                wareCollections.Add(await GetByIsDeliveryAvailable(query.IsDeliveryAvailable.Value));
            }

            if (query.StorageId != null)
            {
                wareCollections.Add(await GetByStorageId(query.StorageId.Value));
            }

            if (query.ShopId != null)
            {
                wareCollections.Add(await GetByShopId(query.ShopId.Value));
            }

            if (query.MinQuantity != null && query.MaxQuantity != null)
            {
                wareCollections.Add(await GetByQuantityRange(query.MinQuantity.Value, query.MaxQuantity.Value));
            }

            if (!wareCollections.Any())
            {
                return new List<WareItem>();
            }

            if (query.PageNumber != null && query.PageSize != null)
            {
                return wareCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value);
            }
            return wareCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }

        public async IAsyncEnumerable<WareItem> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var wareItem = await GetById(id);  // Виклик методу репозиторію
                if (wareItem != null)
                {
                    yield return wareItem;
                }
            }
        }
        public async Task Create(WareItem wareItem)
        {
            await _context.WareItems.AddAsync(wareItem);
        }
        public void Update(WareItem wareItem)
        {
            _context.Entry(wareItem).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var ware = await _context.WareItems.FindAsync(id);
            if (ware != null) { _context.WareItems.Remove(ware); }
        }
    }
}
