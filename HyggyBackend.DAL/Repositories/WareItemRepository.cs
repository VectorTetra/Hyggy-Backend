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
        public async Task<WareItem?> ExistsAsync(long wareId, long storageId)
        {
            return await _context.WareItems
                .FirstOrDefaultAsync(w => w.Ware.Id == wareId && w.Storage.Id == storageId);
        }
        public async Task<IEnumerable<WareItem>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareItem>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
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

            if (maxPrice < minPrice)
            {
                return await _context.WareItems.Where(x => (x.Ware.Price * (1-(x.Ware.Discount / 100))) >= maxPrice && (x.Ware.Price * (1 - (x.Ware.Discount / 100))) <= minPrice).ToListAsync();
            }
            if (minPrice == maxPrice)
            {
                return await _context.WareItems.Where(x => (x.Ware.Price * (1 - (x.Ware.Discount / 100))) == minPrice).ToListAsync();
            }
            return await _context.WareItems.Where(x => (x.Ware.Price * (1 - (x.Ware.Discount / 100))) >= minPrice && (x.Ware.Price * (1 - (x.Ware.Discount / 100))) <= maxPrice).ToListAsync();
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
            return await _context.WareItems.Where(x => x.Ware.Statuses.Any(st => st.Id == statusId)).ToListAsync();
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
            var collections = new List<IEnumerable<WareItem>>();

            // Перевірка наявності QueryAny
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(await GetByArticle(id));
                    collections.Add(await GetByWareId(id));
                    collections.Add(await GetByShopId(id));
                    collections.Add(await GetByStorageId(id));
                    collections.Add(await GetByQuantityRange(id, id));
                    collections.Add(new List<WareItem> { await GetById(id) });
                }
                if (float.TryParse(query.QueryAny, out float val))
                {
                    collections.Add(await GetByWarePriceRange(val, val));
                    collections.Add(await GetByWareDiscountRange(val, val));
                }
                
                collections.Add(await GetByWareName(query.QueryAny));
                collections.Add(await GetByWareDescription(query.QueryAny));


            }
            else
            {
                if (query.Id != null)
                {
                    var proto = await GetById(query.Id.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<WareItem> { proto });
                    }
                }

                if (query.Article != null)
                {
                    collections.Add(await GetByArticle(query.Article.Value));
                }

                if (query.WareId != null)
                {
                    collections.Add(await GetByWareId(query.WareId.Value));
                }

                if (query.WareName != null)
                {
                    collections.Add(await GetByWareName(query.WareName));
                }

                if (query.WareDescription != null)
                {
                    collections.Add(await GetByWareDescription(query.WareDescription));
                }

                if (query.MinPrice != null && query.MaxPrice != null)
                {
                    collections.Add(await GetByWarePriceRange(query.MinPrice.Value, query.MaxPrice.Value));
                }

                if (query.MinDiscount != null && query.MaxDiscount != null)
                {
                    collections.Add(await GetByWareDiscountRange(query.MinDiscount.Value, query.MaxDiscount.Value));
                }

                if (query.StatusId != null)
                {
                    collections.Add(await GetByWareStatusId(query.StatusId.Value));
                }

                if (query.WareCategory3Id != null)
                {
                    collections.Add(await GetByWareCategory3Id(query.WareCategory3Id.Value));
                }

                if (query.WareCategory2Id != null)
                {
                    collections.Add(await GetByWareCategory2Id(query.WareCategory2Id.Value));
                }

                if (query.WareCategory1Id != null)
                {
                    collections.Add(await GetByWareCategory1Id(query.WareCategory1Id.Value));
                }

                if (query.WareImageId != null)
                {
                    collections.Add(await GetByWareImageId(query.WareImageId.Value));
                }

                if (query.PriceHistoryId != null)
                {
                    collections.Add(await GetByPriceHistoryId(query.PriceHistoryId.Value));
                }

                if (query.OrderItemId != null)
                {
                    collections.Add(await GetByOrderItemId(query.OrderItemId.Value));
                }

                if (query.IsDeliveryAvailable != null)
                {
                    collections.Add(await GetByIsDeliveryAvailable(query.IsDeliveryAvailable.Value));
                }

                if (query.StorageId != null)
                {
                    collections.Add(await GetByStorageId(query.StorageId.Value));
                }

                if (query.ShopId != null)
                {
                    collections.Add(await GetByShopId(query.ShopId.Value));
                }

                if (query.MinQuantity != null && query.MaxQuantity != null)
                {
                    collections.Add(await GetByQuantityRange(query.MinQuantity.Value, query.MaxQuantity.Value));
                }

                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<WareItem>();

            // Пагінація за замовчуванням, якщо не знайдено колекцій
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.WareItems
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Об'єднання результатів з QueryAny
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                // Знаходження перетину результатів
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "IdAsc":
                        result = result.OrderBy(ware => ware.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(ware => ware.Id).ToList();
                        break;
                    case "ArticleAsc":
                        result = result.OrderBy(ware => ware.Ware.Article).ToList();
                        break;
                    case "ArticleDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Article).ToList();
                        break;
                    case "WareIdAsc":
                        result = result.OrderBy(ware => ware.Ware.Id).ToList();
                        break;
                    case "WareIdDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Id).ToList();
                        break;
                    case "WareNameAsc":
                        result = result.OrderBy(ware => ware.Ware.Name).ToList();
                        break;
                    case "WareNameDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Name).ToList();
                        break;
                    case "WareDescriptionAsc":
                        result = result.OrderBy(ware => ware.Ware.Description).ToList();
                        break;
                    case "WareDescriptionDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Description).ToList();
                        break;
                    case "PriceAsc":
                        result = result.OrderBy(ware => ware.Ware.Price * (1 - ware.Ware.Discount / 100)).ToList();
                        break;
                    case "PriceDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Price * (1 - ware.Ware.Discount / 100)).ToList();
                        break;
                    case "DiscountAsc":
                        result = result.OrderBy(ware => ware.Ware.Discount).ToList();
                        break;
                    case "DiscountDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Discount).ToList();
                        break;
                    case "WareCategory3IdAsc":
                        result = result.OrderBy(ware => ware.Ware.WareCategory3.Id).ToList();
                        break;
                    case "WareCategory3IdDesc":
                        result = result.OrderByDescending(ware => ware.Ware.WareCategory3.Id).ToList();
                        break;
                    case "WareCategory2IdAsc":
                        result = result.OrderBy(ware => ware.Ware.WareCategory3.WareCategory2.Id).ToList();
                        break;
                    case "WareCategory2IdDesc":
                        result = result.OrderByDescending(ware => ware.Ware.WareCategory3.WareCategory2.Id).ToList();
                        break;
                    case "WareCategory1IdAsc":
                        result = result.OrderBy(ware => ware.Ware.WareCategory3.WareCategory2.WareCategory1.Id).ToList();
                        break;
                    case "WareCategory1IdDesc":
                        result = result.OrderByDescending(ware => ware.Ware.WareCategory3.WareCategory2.WareCategory1.Id).ToList();
                        break;
                    case "QuantityAsc":
                        result = result.OrderBy(ware => ware.Quantity).ToList();
                        break;
                    case "QuantityDesc":
                        result = result.OrderByDescending(ware => ware.Quantity).ToList();
                        break;
                    case "StorageIdAsc":
                        result = result.OrderBy(ware => ware.Storage.Id).ToList();
                        break;
                    case "StorageIdDesc":
                        result = result.OrderByDescending(ware => ware.Storage.Id).ToList();
                        break;
                    case "ShopIdAsc":
                        result = result.OrderBy(ware => ware.Storage.Shop?.Id).ToList();
                        break;
                    case "ShopIdDesc":
                        result = result.OrderByDescending(ware => ware.Storage.Shop?.Id).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null)
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<WareItem>();
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
