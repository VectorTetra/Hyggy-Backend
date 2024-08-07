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
    public class WareRepository : IWareRepository
    {
        private readonly HyggyContext _context;

        public WareRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<Ware?> GetById(long id)
        {
            return await _context.Wares.FindAsync(id);
        }
        public async Task<Ware?> GetByArticle(long article)
        {
            return await _context.Wares.FirstOrDefaultAsync(x => x.Article == article);
        }
        public async Task<IEnumerable<Ware>> GetAll()
        {
            return await _context.Wares.ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory1Id(long category1Id)
        {
            return await _context.Wares.Where(x => x.Category3.WareCategory2.WareCategory1.Id == category1Id).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory2Id(long category2Id)
        {
            return await _context.Wares.Where(x => x.Category3.WareCategory2.Id == category2Id).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory3Id(long category3Id)
        {
            return await _context.Wares.Where(x => x.Category3.Id == category3Id).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.Wares.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.Wares.Where(x => x.Description.Contains(descriptionSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory1NameSubstring(string category1NameSubstring)
        {
            return await _context.Wares.Where(x => x.Category3.WareCategory2.WareCategory1.Name.Contains(category1NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory2NameSubstring(string category2NameSubstring)
        {
            return await _context.Wares.Where(x => x.Category3.WareCategory2.Name.Contains(category2NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory3NameSubstring(string category3NameSubstring)
        {
            return await _context.Wares.Where(x => x.Category3.Name.Contains(category3NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByPriceRange(float minPrice, float maxPrice)
        {
            if (minPrice > maxPrice)
            {
                return await _context.Wares.Where(x => x.Price <= minPrice && x.Price >= maxPrice).ToListAsync();
            }
            if (minPrice == maxPrice)
            {
                return await _context.Wares.Where(x => x.Price == minPrice).ToListAsync();
            }
            return await _context.Wares.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByDiscountRange(float minDiscount, float maxDiscount)
        {
            if (minDiscount > maxDiscount)
            {
                return await _context.Wares.Where(x => x.Discount <= minDiscount && x.Discount >= maxDiscount).ToListAsync();
            }
            if (minDiscount == maxDiscount)
            {
                return await _context.Wares.Where(x => x.Discount == minDiscount).ToListAsync();
            }
            return await _context.Wares.Where(x => x.Discount >= minDiscount && x.Discount <= maxDiscount).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByIsDeliveryAvailable(bool isDeliveryAvailable)
        {
            return await _context.Wares.Where(x => x.IsDeliveryAvailable == isDeliveryAvailable).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusId(long statusId)
        {
            return await _context.Wares.Where(x => x.Status.Id == statusId).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            return await _context.Wares.Where(x => x.Status.Name.Contains(statusNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            return await _context.Wares.Where(x => x.Status.Description.Contains(statusDescriptionSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByImagePathSubstring(string imagePathSubstring)
        {
            return await _context.Wares.Where(x => x.Images.Any(y => y.Path.Contains(imagePathSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByQuery(WareQueryDAL queryDAL)
        {
            var wareCollections = new List<IEnumerable<Ware>>();

            if (queryDAL.Category1Id != null)
            {
                wareCollections.Add(await GetByCategory1Id(queryDAL.Category1Id.Value));
            }

            if (queryDAL.Category2Id != null)
            {
                wareCollections.Add(await GetByCategory2Id(queryDAL.Category2Id.Value));
            }

            if (queryDAL.Category3Id != null)
            {
                wareCollections.Add(await GetByCategory3Id(queryDAL.Category3Id.Value));
            }

            if (queryDAL.NameSubstring != null)
            {
                wareCollections.Add(await GetByNameSubstring(queryDAL.NameSubstring));
            }

            if (queryDAL.DescriptionSubstring != null)
            {
                wareCollections.Add(await GetByDescriptionSubstring(queryDAL.DescriptionSubstring));
            }

            if (queryDAL.Category1NameSubstring != null)
            {
                wareCollections.Add(await GetByCategory1NameSubstring(queryDAL.Category1NameSubstring));
            }

            if (queryDAL.Category2NameSubstring != null)
            {
                wareCollections.Add(await GetByCategory2NameSubstring(queryDAL.Category2NameSubstring));
            }

            if (queryDAL.Category3NameSubstring != null)
            {
                wareCollections.Add(await GetByCategory3NameSubstring(queryDAL.Category3NameSubstring));
            }

            if (queryDAL.MinPrice != null && queryDAL.MaxPrice != null)
            {
                wareCollections.Add(await GetByPriceRange(queryDAL.MinPrice.Value, queryDAL.MaxPrice.Value));
            }

            if (queryDAL.MinDiscount != null && queryDAL.MaxDiscount != null)
            {
                wareCollections.Add(await GetByDiscountRange(queryDAL.MinDiscount.Value, queryDAL.MaxDiscount.Value));
            }

            if (queryDAL.IsDeliveryAvailable != null)
            {
                wareCollections.Add(await GetByIsDeliveryAvailable(queryDAL.IsDeliveryAvailable.Value));
            }

            if (queryDAL.StatusId != null)
            {
                wareCollections.Add(await GetByStatusId(queryDAL.StatusId.Value));
            }

            if (queryDAL.StatusName != null)
            {
                wareCollections.Add(await GetByStatusNameSubstring(queryDAL.StatusName));
            }

            if (queryDAL.StatusDescription != null)
            {
                wareCollections.Add(await GetByStatusDescriptionSubstring(queryDAL.StatusDescription));
            }

            if (queryDAL.ImagePath != null)
            {
                wareCollections.Add(await GetByImagePathSubstring(queryDAL.ImagePath));
            }

            if (!wareCollections.Any())
            {
                return new List<Ware>();
            }

            return wareCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        public async Task Create(Ware ware)
        {
            await _context.Wares.AddAsync(ware);
        }
        public void Update(Ware ware)
        {
            _context.Entry(ware).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var ware = await _context.Wares.FindAsync(id);
            if (ware != null) { _context.Wares.Remove(ware); }
        }
    }
}
