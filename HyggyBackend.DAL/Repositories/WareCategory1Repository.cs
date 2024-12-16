using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class WareCategory1Repository : IWareCategory1Repository
    {
        private readonly HyggyContext _context;

        public WareCategory1Repository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<WareCategory1?> GetById(long id)
        {
            return await _context.WareCategories1.FindAsync(id);
        }

        public async Task<IEnumerable<WareCategory1>> GetPagedCategories(int pageNumber, int pageSize)
        {
            return await _context.WareCategories1
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<WareCategory1>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareCategory1>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async Task<IEnumerable<WareCategory1>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.WareCategories1.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }


        public async Task<IEnumerable<WareCategory1>> GetByWareCategory2Id(long id)
        {
            return await _context.WareCategories1.Where(x => x.WaresCategory2.Any(m => m.Id == id)).ToListAsync();
        }

        public async Task<IEnumerable<WareCategory1>> GetByWareCategory2NameSubstring(string WareCategory2NameSubstring)
        {
            return await _context.WareCategories1.Where(x => x.WaresCategory2.Any(m => m.Name.Contains(WareCategory2NameSubstring))).ToListAsync();
        }

        public async Task<IEnumerable<WareCategory1>> GetByWareCategory3Id(long id)
        {
            return await _context.WareCategories1.Where(x => x.WaresCategory2.Any(m => m.WaresCategory3.Any(n => n.Id == id))).ToListAsync();
        }

        public async Task<IEnumerable<WareCategory1>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring)
        {
            return await _context.WareCategories1.Where(x => x.WaresCategory2.Any(m => m.WaresCategory3.Any(n => n.Name.Contains(WareCategory3NameSubstring)))).ToListAsync();
        }

        public async IAsyncEnumerable<WareCategory1> GetByIdsAsync(IEnumerable<long> ids)
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
        public async Task<IEnumerable<WareCategory1>> GetByQuery(WareCategory1QueryDAL query)
        {
            var collections = new List<IEnumerable<WareCategory1>>();

            // Пошук по QueryAny
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<WareCategory1> { await GetById(id) });
                }
                collections.Add(await GetByNameSubstring(query.QueryAny));
                collections.Add(await GetByWareCategory2NameSubstring(query.QueryAny));
                collections.Add(await GetByWareCategory3NameSubstring(query.QueryAny));
            }
            else
            {
                if (query.Id != null)
                {
                    collections.Add(await _context.WareCategories1.Where(x => x.Id == query.Id).ToListAsync());
                }

                if (query.NameSubstring != null)
                {
                    collections.Add(await GetByNameSubstring(query.NameSubstring));
                }

                if (query.WareCategory2Id != null)
                {
                    collections.Add(await GetByWareCategory2Id(query.WareCategory2Id.Value));
                }

                if (query.WareCategory2NameSubstring != null)
                {
                    collections.Add(await GetByWareCategory2NameSubstring(query.WareCategory2NameSubstring));
                }

                if (query.WareCategory3Id != null)
                {
                    collections.Add(await GetByWareCategory3Id(query.WareCategory3Id.Value));
                }

                if (query.WareCategory3NameSubstring != null)
                {
                    collections.Add(await GetByWareCategory3NameSubstring(query.WareCategory3NameSubstring));
                }

                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<WareCategory1>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = await _context.WareCategories1
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToListAsync();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Об'єднання результатів
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                var nonEmptyCollections = collections.Where(collection => collection.Any()).ToList();

                // Перетин результатів з відфільтрованих колекцій
                if (nonEmptyCollections.Any())
                {
                    result = nonEmptyCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
                }
                else
                {
                    result = new List<WareCategory1>(); // Повертаємо порожній список, якщо всі колекції були порожні
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
                    case "NameAsc":
                        result = result.OrderBy(ware => ware.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(ware => ware.Name).ToList();
                        break;
                    case "WareCategory2IdAsc":
                        result = result.OrderBy(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory2IdDesc":
                        result = result.OrderByDescending(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory3IdAsc":
                        result = result.OrderBy(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory3IdDesc":
                        result = result.OrderByDescending(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory2NameAsc":
                        result = result.OrderBy(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory2NameDesc":
                        result = result.OrderByDescending(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory3NameAsc":
                        result = result.OrderBy(ware => ware.WaresCategory2).ToList();
                        break;
                    case "WareCategory3NameDesc":
                        result = result.OrderByDescending(ware => ware.WaresCategory2).ToList();
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

            return result.Any() ? result : new List<WareCategory1>();
        }


        public async Task Create(WareCategory1 wareCategory1)
        {
            _context.WareCategories1.Add(wareCategory1);
        }

        public void Update(WareCategory1 wareCategory1)
        {
            _context.Entry(wareCategory1).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var wareCategory1 = await GetById(id);
            if (wareCategory1 != null)
            {
                _context.WareCategories1.Remove(wareCategory1);
            }
        }

    }
}
