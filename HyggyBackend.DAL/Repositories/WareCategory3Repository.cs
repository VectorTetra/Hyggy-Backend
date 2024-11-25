using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class WareCategory3Repository : IWareCategory3Repository
    {
        public HyggyContext _context { get; set; }

        public WareCategory3Repository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<WareCategory3?> GetById(long id)
        {
            return await _context.WareCategories3.FindAsync(id);
        }

        public async Task<IEnumerable<WareCategory3>> GetPagedCategories(int pageNumber, int pageSize)
        {
            return await _context.WareCategories3
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<WareCategory3>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareCategory3>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }
        public async Task<IEnumerable<WareCategory3>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.WareCategories3.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareCategory1Id(long id)
        {
            return await _context.WareCategories3.Where(x => x.WareCategory2.WareCategory1.Id == id).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring)
        {
            return await _context.WareCategories3.Where(x => x.WareCategory2.WareCategory1.Name.Contains(WareCategory1NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareCategory2Id(long id)
        {
            return await _context.WareCategories3.Where(x => x.WareCategory2.Id == id).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareCategory2NameSubstring(string WareCategory3NameSubstring)
        {
            return await _context.WareCategories3.Where(x => x.WareCategory2.Name.Contains(WareCategory3NameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareId(long id)
        {
            return await _context.WareCategories3.Where(x => x.Wares.Any(m => m.Id == id)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareArticle(long Article)
        {
            return await _context.WareCategories3.Where(x => x.Wares.Any(m => m.Article == Article)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareNameSubstring(string WareNameSubstring)
        {
            return await _context.WareCategories3.Where(x => x.Wares.Any(m => m.Name.Contains(WareNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory3>> GetByWareDescriptionSubstring(string WareDescriptionSubstring)
        {
            return await _context.WareCategories3.Where(x => x.Wares.Any(m => m.Description.Contains(WareDescriptionSubstring))).ToListAsync();
        }

        public async IAsyncEnumerable<WareCategory3> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var item = await GetById(id);  // Виклик методу репозиторію
                if (item != null)
                {
                    yield return item;
                }
            }
        }
        public async Task<IEnumerable<WareCategory3>> GetByQuery(WareCategory3QueryDAL query)
        {
            var collections = new List<IEnumerable<WareCategory3>>();

            // Обробка запиту за QueryAny
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(await GetByWareId(id));
                    collections.Add(await GetByWareArticle(id));
                    collections.Add(new List<WareCategory3> { await GetById(id) });
                }
                collections.Add(await GetByNameSubstring(query.QueryAny));
                collections.Add(await GetByWareCategory1NameSubstring(query.QueryAny));
                collections.Add(await GetByWareCategory2NameSubstring(query.QueryAny));
                collections.Add(await GetByWareNameSubstring(query.QueryAny));
                collections.Add(await GetByWareDescriptionSubstring(query.QueryAny));
            }
            else
            {
                // Обробка інших параметрів запиту
                if (query.Id != null)
                {
                    var proto = await GetById(query.Id.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<WareCategory3> { proto });
                    }
                }

                if (query.NameSubstring != null)
                {
                    collections.Add(await GetByNameSubstring(query.NameSubstring));
                }
                if (query.WareCategory1Id != null)
                {
                    collections.Add(await GetByWareCategory1Id(query.WareCategory1Id.Value));
                }
                if (query.WareCategory1NameSubstring != null)
                {
                    collections.Add(await GetByWareCategory1NameSubstring(query.WareCategory1NameSubstring));
                }
                if (query.WareCategory2Id != null)
                {
                    collections.Add(await GetByWareCategory2Id(query.WareCategory2Id.Value));
                }
                if (query.WareCategory2NameSubstring != null)
                {
                    collections.Add(await GetByWareCategory2NameSubstring(query.WareCategory2NameSubstring));
                }
                if (query.WareId != null)
                {
                    collections.Add(await GetByWareId(query.WareId.Value));
                }
                if (query.WareArticle != null)
                {
                    collections.Add(await GetByWareArticle(query.WareArticle.Value));
                }
                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<WareCategory3>();

            // Пагінація без запитів
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = await _context.WareCategories3
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToListAsync();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Об'єднання результатів за QueryAny
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else if (query.QueryAny == null && collections.Any())
            {
                // Знаходження перетину
                result = collections.Aggregate(new List<WareCategory3>(), (previousList, nextList) =>
                    previousList.Intersect(nextList).ToList());
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
                    case "NameAsc":
                        result = result.OrderBy(ware => ware.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(ware => ware.Name).ToList();
                        break;
                    case "WareCategory1IdAsc":
                        result = result.OrderBy(ware => ware.WareCategory2.WareCategory1.Id).ToList();
                        break;
                    case "WareCategory1IdDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory2.WareCategory1.Id).ToList();
                        break;
                    case "WareCategory1NameAsc":
                        result = result.OrderBy(ware => ware.WareCategory2.WareCategory1.Name).ToList();
                        break;
                    case "WareCategory1NameDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory2.WareCategory1.Name).ToList();
                        break;
                    case "WareCategory2IdAsc":
                        result = result.OrderBy(ware => ware.WareCategory2.Id).ToList();
                        break;
                    case "WareCategory2IdDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory2.Id).ToList();
                        break;
                    case "WareCategory2NameAsc":
                        result = result.OrderBy(ware => ware.WareCategory2.Name).ToList();
                        break;
                    case "WareCategory2NameDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory2.Name).ToList();
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

            return result.Any() ? result : new List<WareCategory3>();
        }

        public async Task Create(WareCategory3 category3)
        {
            _context.WareCategories3.Add(category3);
        }
        public void Update(WareCategory3 category3)
        {
            _context.Entry(category3).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var category3 = await GetById(id);
            if (category3 != null)
            {
                _context.WareCategories3.Remove(category3);
            }
        }
    }
}
