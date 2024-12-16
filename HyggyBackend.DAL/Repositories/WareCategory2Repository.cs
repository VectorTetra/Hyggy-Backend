using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;


namespace HyggyBackend.DAL.Repositories
{
    public class WareCategory2Repository : IWareCategory2Repository
    {
        public HyggyContext _context { get; set; }

        public WareCategory2Repository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<WareCategory2?> GetById(long id)
        {
            return await _context.WareCategories2.FindAsync(id);
        }

        public async Task<IEnumerable<WareCategory2>> GetPagedCategories(int pageNumber, int pageSize)
        {
            return await _context.WareCategories2
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<WareCategory2>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareCategory2>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }
        public async Task<IEnumerable<WareCategory2>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.WareCategories2.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory2>> GetByWareCategory1Id(long id)
        {

            return await _context.WareCategories2.Where(x => x.WareCategory1.Id == id).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory2>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring)
        {

            return await _context.WareCategories2.Where(x => x.WareCategory1.Name.Contains(WareCategory1NameSubstring)).ToListAsync();

        }
        public async Task<IEnumerable<WareCategory2>> GetByWareCategory3Id(long id)
        {
            return await _context.WareCategories2.Where(x => x.WaresCategory3.Any(m => m.Id == id)).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory2>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring)
        {
            return await _context.WareCategories2.Where(x => x.WaresCategory3.Any(m => m.Name.Contains(WareCategory3NameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<WareCategory2>> GetByQuery(WareCategory2QueryDAL query)
        {
            var collections = new List<IEnumerable<WareCategory2>>();

            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<WareCategory2> { await GetById(id) });
                }
                collections.Add(await GetByNameSubstring(query.QueryAny));
                collections.Add(await GetByWareCategory1NameSubstring(query.QueryAny));
                collections.Add(await GetByWareCategory3NameSubstring(query.QueryAny));
            }
            else
            {
                if (query.Id != null)
                {
                    collections.Add(await _context.WareCategories2.Where(x => x.Id == query.Id).ToListAsync());
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

            var result = new List<WareCategory2>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = await _context.WareCategories2
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
                    result = new List<WareCategory2>(); // Повертаємо порожній список, якщо всі колекції були порожні
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
                    case "WareCategory1IdAsc":
                        result = result.OrderBy(ware => ware.WareCategory1.Id).ToList();
                        break;
                    case "WareCategory1IdDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory1.Id).ToList();
                        break;
                    case "WareCategory1NameAsc":
                        result = result.OrderBy(ware => ware.WareCategory1.Name).ToList();
                        break;
                    case "WareCategory1NameDesc":
                        result = result.OrderByDescending(ware => ware.WareCategory1.Name).ToList();
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

            return result.Any() ? result : new List<WareCategory2>();
        }


        public async IAsyncEnumerable<WareCategory2> GetByIdsAsync(IEnumerable<long> ids)
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
        public async Task Create(WareCategory2 category2)
        {
            await _context.WareCategories2.AddAsync(category2);
        }
        public void Update(WareCategory2 category2)
        {
            _context.Entry(category2).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var category2 = await GetById(id);
            if (category2 != null)
            {
                _context.WareCategories2.Remove(category2);
            }
        }

    }
}
