using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IEnumerable<Ware>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<Ware>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }
        public async Task<IEnumerable<Ware>> GetByStringTrademarkIds(string stringTrademarkIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringTrademarkIds.Split('|').Select(long.Parse).ToList();

            // Виконуємо запит до контексту для отримання товарів, у яких ідентифікатор торгової марки міститься у списку
            var waress = await _context.Wares
                .Where(ware => ware.WareTrademark != null && ids.Contains(ware.WareTrademark.Id))
                .ToListAsync();

            return waress;
        }
        public async Task<IEnumerable<Ware>> GetByStringStatusIds(string stringStatusIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringStatusIds.Split('|').Select(long.Parse).ToList();

            // Виконуємо запит до контексту для отримання товарів, у яких ідентифікатор статусу міститься у списку
            var waress = await _context.Wares
                .Where(ware => ware.Statuses.Any(status => ids.Contains(status.Id)))
                .ToListAsync();

            return waress;
        }
        public async Task<IEnumerable<Ware>> GetByStringCategory1Ids(string stringCategory1Ids)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringCategory1Ids.Split('|').Select(long.Parse).ToList();

            // Виконуємо запит до контексту для отримання товарів, у яких ідентифікатор категорії 1 міститься у списку
            var waress = await _context.Wares
                .Where(ware => ware.WareCategory3.WareCategory2.WareCategory1 != null && ids.Contains(ware.WareCategory3.WareCategory2.WareCategory1.Id))
                .ToListAsync();

            return waress;
        }
        public async Task<IEnumerable<Ware>> GetByStringCategory2Ids(string stringCategory2Ids)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringCategory2Ids.Split('|').Select(long.Parse).ToList();

            // Виконуємо запит до контексту для отримання товарів, у яких ідентифікатор категорії 2 міститься у списку
            var waress = await _context.Wares
                .Where(ware => ware.WareCategory3.WareCategory2 != null && ids.Contains(ware.WareCategory3.WareCategory2.Id))
                .ToListAsync();

            return waress;
        }
        public async Task<IEnumerable<Ware>> GetByStringCategory3Ids(string stringCategory3Ids)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringCategory3Ids.Split('|').Select(long.Parse).ToList();

            // Виконуємо запит до контексту для отримання товарів, у яких ідентифікатор категорії 3 міститься у списку
            var waress = await _context.Wares
                .Where(ware => ware.WareCategory3 != null && ids.Contains(ware.WareCategory3.Id))
                .ToListAsync();

            return waress;
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
        public async Task<IEnumerable<Ware>> GetByStructureFilePathSubstring(string StructureFilePathSubstring)
        {
            return await _context.Wares.Where(x => x.StructureFilePath.Contains(StructureFilePathSubstring)).ToListAsync();
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
                return await _context.Wares.Where(x => (x.Price * ((100 - x.Discount)/100)) <= minPrice && (x.Price * ((100 - x.Discount) / 100)) >= maxPrice).ToListAsync();
            }
            if (minPrice == maxPrice)
            {
                return await _context.Wares.Where(x => (x.Price * ((100 - x.Discount) / 100)) == minPrice).ToListAsync();
            }
            return await _context.Wares.Where(x => (x.Price * ((100 - x.Discount) / 100)) >= minPrice && (x.Price * ((100 - x.Discount) / 100)) <= maxPrice).ToListAsync();
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
            return await _context.Wares.Where(x => x.Statuses.Any(st => st.Id == statusId)).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            return await _context.Wares.Where(x => x.Statuses.Any(s => s.Name.Contains(statusNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Ware>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            return await _context.Wares.Where(x => x.Statuses.Any(s => s.Description.Contains(statusDescriptionSubstring))).ToListAsync();
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

        public async Task<IEnumerable<Ware>> GetByQuery(WareQueryDAL query)
        {
            var collections = new List<IEnumerable<Ware>>();
            var isAnySearch = query.QueryAny != null;
            var isAllSearch = (query.Id != null
                || query.Article != null
                || query.Category1Id != null
                || query.Category1NameSubstring != null
                || query.Category2Id != null
                || query.Category2NameSubstring != null
                || query.Category3Id != null
                || query.Category3NameSubstring != null
                || query.CustomerId != null
                || query.DescriptionSubstring != null
                || query.ImagePath != null
                || query.IsDeliveryAvailable != null
                || query.MaxDiscount != null
                || query.MinDiscount != null
                || query.MaxPrice != null
                || query.MinPrice != null
                || query.NameSubstring != null
                || query.StatusDescription != null
                || query.StatusId != null
                || query.StatusName != null
                || query.StringCategory1Ids != null
                || query.StringCategory2Ids != null
                || query.StringCategory3Ids != null
                || query.StringIds != null
                || query.StringStatusIds != null
                || query.StringTrademarkIds != null
                || query.TrademarkId != null
                || query.TrademarkNameSubstring != null
                );
            var isFlexSearch = isAnySearch && isAllSearch;

            // Перевірка наявності QueryAny
            if (isAnySearch)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<Ware> { await GetById(id) }); // Можливий артикул
                    collections.Add(new List<Ware> { await GetByArticle(id) }); // Можливий артикул
                    collections.Add(await GetByCategory1Id(id));
                    collections.Add(await GetByCategory2Id(id));
                    collections.Add(await GetByCategory3Id(id));
                    collections.Add(await GetByTrademarkId(id));
                    // Інші методи для отримання
                }
                if (float.TryParse(query.QueryAny, out float val))
                {
                    collections.Add(await GetByPriceRange(val, val)); // Якщо це ціна
                    collections.Add(await GetByDiscountRange(val, val)); // Якщо це знижка
                }

                // Пошук за рядками
                collections.Add(await GetByNameSubstring(query.QueryAny));
                collections.Add(await GetByDescriptionSubstring(query.QueryAny));
                collections.Add(await GetByTrademarkNameSubstring(query.QueryAny));
                collections.Add(await GetByCategory1NameSubstring(query.QueryAny));
                collections.Add(await GetByCategory2NameSubstring(query.QueryAny));
                collections.Add(await GetByCategory3NameSubstring(query.QueryAny));
                collections.Add(await GetByStatusNameSubstring(query.QueryAny));
                collections.Add(await GetByStatusDescriptionSubstring(query.QueryAny));
                collections.Add(await GetByStructureFilePathSubstring(query.QueryAny));
                collections.Add(await GetByImagePathSubstring(query.QueryAny));

                // Додайте інші можливі пошукові методи
            }
            if (isAllSearch)
            {
                // Ваша логіка для інших полів, що не включають QueryAny
                if (query.Id != null)
                {
                    var res = await GetById(query.Id.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Ware> { res });
                    }
                }
                if (query.Article != null)
                {
                    var res = await GetByArticle(query.Article.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Ware> { res });
                    }
                }
                if (query.Category1Id != null)
                {
                    collections.Add(await GetByCategory1Id(query.Category1Id.Value));
                }
                if (query.Category2Id != null)
                {
                    collections.Add(await GetByCategory2Id(query.Category2Id.Value));
                }
                if (query.Category3Id != null)
                {
                    collections.Add(await GetByCategory3Id(query.Category3Id.Value));
                }
                if (query.CustomerId != null)
                {
                    collections.Add(await GetFavoritesByCustomerId(query.CustomerId));
                }
                if (query.DescriptionSubstring != null)
                {
                    collections.Add(await GetByDescriptionSubstring(query.DescriptionSubstring));
                }
                if (query.ImagePath != null)
                {
                    collections.Add(await GetByImagePathSubstring(query.ImagePath));
                }
                if (query.IsDeliveryAvailable != null)
                {
                    collections.Add(await GetByIsDeliveryAvailable(query.IsDeliveryAvailable.Value));
                }
                if (query.MaxDiscount != null && query.MinDiscount != null)
                {
                    collections.Add(await GetByDiscountRange(query.MinDiscount.Value, query.MaxDiscount.Value));
                }
                if (query.MaxPrice != null && query.MinPrice != null)
                {
                    collections.Add(await GetByPriceRange(query.MinPrice.Value, query.MaxPrice.Value));
                }
                if (query.NameSubstring != null)
                {
                    collections.Add(await GetByNameSubstring(query.NameSubstring));
                }
                if (query.StatusDescription != null)
                {
                    collections.Add(await GetByStatusDescriptionSubstring(query.StatusDescription));
                }
                if (query.StatusId != null)
                {
                    collections.Add(await GetByStatusId(query.StatusId.Value));
                }
                if (query.StatusName != null)
                {
                    collections.Add(await GetByStatusNameSubstring(query.StatusName));
                }
                if (query.StringCategory1Ids != null)
                {
                    collections.Add(await GetByStringCategory1Ids(query.StringCategory1Ids));
                }
                if (query.StringCategory2Ids != null)
                {
                    collections.Add(await GetByStringCategory2Ids(query.StringCategory2Ids));
                }
                if (query.StringCategory3Ids != null)
                {
                    collections.Add(await GetByStringCategory3Ids(query.StringCategory3Ids));
                }
                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
                if (query.StringStatusIds != null)
                {
                    collections.Add(await GetByStringStatusIds(query.StringStatusIds));
                }
                if (query.StringTrademarkIds != null)
                {
                    collections.Add(await GetByStringTrademarkIds(query.StringTrademarkIds));
                }
                if (query.TrademarkId != null)
                {
                    collections.Add(await GetByTrademarkId(query.TrademarkId.Value));
                }
                if (query.TrademarkNameSubstring != null)
                {
                    collections.Add(await GetByTrademarkNameSubstring(query.TrademarkNameSubstring));
                }
            }

            var result = new List<Ware>();

            // Пагінація за замовчуванням, якщо не знайдено колекцій
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Wares
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (isAnySearch && !isFlexSearch && collections.Any())
            {
                // Об'єднання результатів з QueryAny
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else if (isAllSearch && !isFlexSearch && collections.Any())
            {
                // Перетин результатів з усіх запитів
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }
            else if (isFlexSearch)
            {
                // Відфільтруємо непорожні колекції
                var nonEmptyCollections = collections.Where(collection => collection.Any()).ToList();

                // Перетин результатів з відфільтрованих колекцій
                if (nonEmptyCollections.Any())
                {
                    result = nonEmptyCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
                }
                else
                {
                    result = new List<Ware>(); // Повертаємо порожній список, якщо всі колекції були порожні
                }
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
                        result = result.OrderByDescending(ware =>
                            ware.Reviews.Any()
                                ? ware.Reviews.Average(review => (float)review.Rating)
                                : 0 // Встановлення рейтингу для товарів без відгуків
                        ).ToList();
                        break;

                    case "DiscountAsc":
                        result = result.OrderBy(ware => ware.Discount).ToList();
                        break;
                    case "DiscountDesc":
                        result = result.OrderByDescending(ware => ware.Discount).ToList();
                        break;
                    case "IsDeliveryAvailable":
                        result = result.OrderByDescending(ware => ware.IsDeliveryAvailable).ToList();
                        break;
                    case "ArticleAsc":
                        result = result.OrderBy(ware => ware.Article).ToList();
                        break;
                    case "ArticleDesc":
                        result = result.OrderByDescending(ware => ware.Article).ToList();
                        break;
                    case "StructureFilePathAsc":
                        result = result.OrderBy(ware => ware.StructureFilePath).ToList();
                        break;
                    case "StructureFilePathDesc":
                        result = result.OrderByDescending(ware => ware.StructureFilePath).ToList();
                        break;
                    case "DescriptionAsc":
                        result = result.OrderBy(ware => ware.Description).ToList();
                        break;
                    case "DescriptionDesc":
                        result = result.OrderByDescending(ware => ware.Description).ToList();
                        break;
                    case "Category1NameAsc":
                        result = result.OrderBy(ware => ware.WareCategory3.WareCategory2.WareCategory1.Name).ToList();
                        break;
                    case "Category1NameDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory3.WareCategory2.WareCategory1.Name).ToList();
                        break;
                    case "Category2NameAsc":
                        result = result.OrderBy(ware => ware.WareCategory3.WareCategory2.Name).ToList();
                        break;
                    case "Category2NameDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory3.WareCategory2.Name).ToList();
                        break;
                    case "Category3NameAsc":
                        result = result.OrderBy(ware => ware.WareCategory3.Name).ToList();
                        break;
                    case "Category3NameDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory3.Name).ToList();
                        break;
                    case "TrademarkNameAsc":
                        result = result.OrderBy(ware => ware.WareTrademark?.Name).ToList();
                        break;
                    case "TrademarkNameDesc":
                        result = result.OrderByDescending(ware => ware.WareTrademark?.Name).ToList();
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

            return result.Any() ? result : new List<Ware>();
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
