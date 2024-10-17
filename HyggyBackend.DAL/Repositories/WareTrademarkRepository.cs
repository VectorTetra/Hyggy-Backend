using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class WareTrademarkRepository : IWareTrademarkRepository
    {
        private readonly HyggyContext _context;

        public WareTrademarkRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<WareTrademark?> GetById(long id)
        {
            return await _context.WareTrademarks.FindAsync(id);
        }
        public async Task<IEnumerable<WareTrademark>> GetByName(string nameSubstr)
        {
            return await _context.WareTrademarks.Where(wt => wt.Name.Contains(nameSubstr)).ToListAsync();
        }
        public async Task<WareTrademark?> GetByWareId(long id)
        {
            return await _context.WareTrademarks.FirstOrDefaultAsync(wt => wt.Wares.Any(wa => wa.Id == id));
        }
        public async Task<IEnumerable<WareTrademark>> GetPagedWareTrademarks(int pageNumber, int pageSize)
        {
            return await _context.WareTrademarks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<WareTrademark>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareTrademark>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async IAsyncEnumerable<WareTrademark> GetByIdsAsync(IEnumerable<long> ids)
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
        public async Task<IEnumerable<WareTrademark>> GetByQuery(WareTrademarkQueryDAL query)
        {
            var collections = new List<IEnumerable<WareTrademark>>();

            if (query.Id != null)
            {
                var proto = await GetById(query.Id.Value);
                if (proto != null)
                {
                    collections.Add(new List<WareTrademark> { proto });
                }
            }

            if (query.Name != null)
            {
                collections.Add(await GetByName(query.Name));
            }

            if (query.WareId != null)
            {
                var proto = await GetByWareId(query.WareId.Value);
                if (proto != null)
                {
                    collections.Add(new List<WareTrademark> { proto });
                }
            }

            if (query.StringIds != null)
            {
                collections.Add(await GetByStringIds(query.StringIds));
            }

            var result = new List<WareTrademark>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.WareTrademarks
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .ToList();
            }
            else
            {
                result = (List<WareTrademark>)collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
            if (!result.Any())
            {
                return new List<WareTrademark>();
            }
            return result;

        }
        public async Task Add(WareTrademark wareTrademark)
        {
            await _context.WareTrademarks.AddAsync(wareTrademark);
        }
        public void Update(WareTrademark wareTrademark)
        {
            _context.Entry(wareTrademark).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var wareTrademark = await _context.WareTrademarks.FindAsync(id);
            if (wareTrademark != null) { _context.WareTrademarks.Remove(wareTrademark); }
        }
    }
}
