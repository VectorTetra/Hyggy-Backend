using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Ware>> GetPagedWares(int pageNumber, int pageSize)
        {
            return await _context.Wares
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory1Id(long category1Id)
        {
            return await _context.Wares.Where(x => x.WareCategory3.WareCategory2.WareCategory1.Id == category1Id).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory2Id(long category2Id)
        {
            return await _context.Wares.Where(x => x.WareCategory3.WareCategory2.Id == category2Id).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory3Id(long category3Id)
        {
            return await _context.Wares.Where(x => x.WareCategory3.Id == category3Id).ToListAsync();
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
            return await _context.Wares.Where(x => x.WareCategory3.WareCategory2.WareCategory1.Name.Contains(category1NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory2NameSubstring(string category2NameSubstring)
        {
            return await _context.Wares.Where(x => x.WareCategory3.WareCategory2.Name.Contains(category2NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByCategory3NameSubstring(string category3NameSubstring)
        {
            return await _context.Wares.Where(x => x.WareCategory3.Name.Contains(category3NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByTrademarkId(long trademarkId)
        {
            return await _context.Wares.Where(x => x.WareTrademark.Id == trademarkId).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByTrademarkNameSubstring(string trademarkNameSubstring)
        {
            return await _context.Wares.Where(x => x.WareTrademark.Name.Contains(trademarkNameSubstring)).ToListAsync();
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
            return await _context.Wares.Where(x => x.Statuses.Any(st=>st.Id == statusId)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            return await _context.Wares.Where(x => x.Statuses.Any(s=>s.Name.Contains(statusNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            return await _context.Wares.Where(x => x.Statuses.Any(s=>s.Description.Contains(statusDescriptionSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByImagePathSubstring(string imagePathSubstring)
        {
            return await _context.Wares.Where(x => x.Images.Any(y => y.Path.Contains(imagePathSubstring))).ToListAsync();
        }

        public async Task<IEnumerable<Ware>> GetFavoritesByCustomerId(string customerId)
        {
            return await _context.Wares.Where(x => x.CustomerFavorites.Any(cu => cu.Id == customerId)).ToListAsync();
        }

        public async IAsyncEnumerable<Ware> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var ware = await GetById(id);  // Виклик методу репозиторію
                if (ware != null)
                {
                    yield return ware;
                }
            }
        }

        /*
        public async Task<float> GetAverageRatingByWareIdAsync(long wareId)
        {
            // Отримуємо товар за його Id з репозиторію
            var ware = await _context.Wares.FindAsync(wareId);
            // Якщо товар не знайдений або немає відгуків, повертаємо 0
            if (ware == null || ware.Reviews == null || !ware.Reviews.Any())
            {
                return 0;
            }
            // Обчислюємо середню оцінку
            return ware.Reviews.Average(review => (float)review.Rating);
        }
        */

        public async Task<IEnumerable<Ware>> GetByQuery(WareQueryDAL queryDAL)
        {
            var wareCollections = new List<IEnumerable<Ware>>();

            if (queryDAL.Id != null)
            {
                wareCollections.Add(new List<Ware> { await GetById(queryDAL.Id.Value) });
            }

            if (queryDAL.Article != null)
            {
                wareCollections.Add(new List<Ware> { await GetByArticle(queryDAL.Article.Value) });
            }

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

            if (queryDAL.TrademarkId != null)
            {
                wareCollections.Add(await GetByTrademarkId(queryDAL.TrademarkId.Value));
            }

            if (queryDAL.TrademarkNameSubstring != null)
            {
                wareCollections.Add(await GetByTrademarkNameSubstring(queryDAL.TrademarkNameSubstring));
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

            if (queryDAL.CustomerId != null)
            {
                wareCollections.Add(await GetFavoritesByCustomerId(queryDAL.CustomerId));
            }

            if (!wareCollections.Any())
            {
                return new List<Ware>();
            }

            var result = wareCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());

            // Сортування
            if (queryDAL.Sorting != null)
            {
                switch (queryDAL.Sorting)
                {
                    case "PriceAsc":
                        result = result.OrderBy(ware => ware.Price * (1 - ware.Discount / 100)).ToList(); // Ціна з урахуванням знижки
                        break;
                    case "PriceDesc":
                        result = result.OrderByDescending(ware => ware.Price * (1 - ware.Discount / 100)).ToList(); // Ціна з урахуванням знижки
                        break;
                    case "NameAsc":
                        result = result.OrderBy(ware => ware.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(ware => ware.Name).ToList();
                        break;
                    case "Rating":
                        result = result.OrderByDescending(ware => ware.Reviews.Average(review => (float)review.Rating)).ToList(); // Сортування за рейтингом
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (queryDAL.PageNumber != null && queryDAL.PageSize != null)
            {
                result = result
                    .Skip((queryDAL.PageNumber.Value - 1) * queryDAL.PageSize.Value)
                    .Take(queryDAL.PageSize.Value)
                    .ToList();
            }

            return result;

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
