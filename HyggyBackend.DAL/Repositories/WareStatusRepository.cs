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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.DAL.Repositories
{
    public class WareStatusRepository: IWareStatusRepository
    {
        private readonly HyggyContext _context;

        public WareStatusRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<WareStatus?> GetById(long id)
        {
            return await _context.WareStatuses.FindAsync(id);
        }
        public async Task<WareStatus?> GetByWareId(long id)
        {
            return await _context.WareStatuses.FirstOrDefaultAsync(x => x.Wares.Any(w => w.Id == id));
        }
        public async Task<IEnumerable<WareStatus>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareStatus>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }
        public async Task<WareStatus?> GetByWareArticle(long article)
        {
            return await _context.WareStatuses.FirstOrDefaultAsync(x => x.Wares.Any(w => w.Article == article));
        }
        public async Task<IEnumerable<WareStatus>> GetPagedWareStatuses(int pageNumber, int pageSize)
        {
            return await _context.WareStatuses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<WareStatus>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.WareStatuses.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareStatus>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.WareStatuses.Where(x => x.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async IAsyncEnumerable<WareStatus> GetByIdsAsync(IEnumerable<long> ids)
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
        public async Task<IEnumerable<WareStatus>> GetByQuery(WareStatusQueryDAL query)
        {
            var collections = new List<IEnumerable<WareStatus>>();

            // Перевірка наявності QueryAny
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<WareStatus> { await GetById(id) }); // Можливий ID
                    collections.Add(await _context.WareStatuses.Where(x => x.Wares.Any(w => w.Id == id)).ToListAsync()); // За WareId
                    collections.Add(await _context.WareStatuses.Where(x => x.Wares.Any(w => w.Article == id)).ToListAsync()); // За WareArticle
                }

                // Пошук за рядками
                collections.Add(await _context.WareStatuses.Where(x => x.Name.Contains(query.QueryAny)).ToListAsync());
                collections.Add(await _context.WareStatuses.Where(x => x.Description.Contains(query.QueryAny)).ToListAsync());
            }
            else
            {
                // Інші параметри запиту
                if (query.Id != null)
                {
                    collections.Add(await _context.WareStatuses.Where(x => x.Id == query.Id).ToListAsync());
                }

                if (query.WareId != null)
                {
                    collections.Add(await _context.WareStatuses.Where(x => x.Wares.Any(w => w.Id == query.WareId)).ToListAsync());
                }

                if (query.WareArticle != null)
                {
                    collections.Add(await _context.WareStatuses.Where(x => x.Wares.Any(w => w.Article == query.WareArticle)).ToListAsync());
                }

                if (query.NameSubstring != null)
                {
                    collections.Add(await _context.WareStatuses.Where(x => x.Name.Contains(query.NameSubstring)).ToListAsync());
                }

                if (query.DescriptionSubstring != null)
                {
                    collections.Add(await _context.WareStatuses.Where(x => x.Description.Contains(query.DescriptionSubstring)).ToListAsync());
                }

                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<WareStatus>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.WareStatuses
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
                var nonEmptyCollections = collections.Where(collection => collection.Any()).ToList();

                // Перетин результатів з відфільтрованих колекцій
                if (nonEmptyCollections.Any())
                {
                    result = nonEmptyCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
                }
                else
                {
                    result = new List<WareStatus>(); // Повертаємо порожній список, якщо всі колекції були порожні
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
                    case "DescriptionAsc":
                        result = result.OrderBy(ware => ware.Description).ToList();
                        break;
                    case "DescriptionDesc":
                        result = result.OrderByDescending(ware => ware.Description).ToList();
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

            return result.Any() ? result : new List<WareStatus>();
        }

        public async Task Create(WareStatus wareStatus)
        {
            await _context.WareStatuses.AddAsync(wareStatus);
        }
        public void Update(WareStatus wareStatus) 
        {
            _context.Entry(wareStatus).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var wareStatus = await _context.WareStatuses.FindAsync(id);
            if (wareStatus != null)
            {
                _context.WareStatuses.Remove(wareStatus);
            }
        }
    }
}
