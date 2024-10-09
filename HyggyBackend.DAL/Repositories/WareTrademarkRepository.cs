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
            return await _context.WareTrademarks.FirstOrDefaultAsync(wt => wt.Wares.Any(wa=>wa.Id == id));
        }
        public async Task<IEnumerable<WareTrademark>> GetPagedWareTrademarks(int pageNumber, int pageSize)
        {
            return await _context.WareTrademarks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
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
            var wareTrademarkCollections = new List<IEnumerable<WareTrademark>>();

            if (query.Id != null)
            {
                wareTrademarkCollections.Add(new List<WareTrademark> { await GetById(query.Id.Value) });
            }

            if (query.Name != null)
            {
                wareTrademarkCollections.Add(await GetByName(query.Name) );
            }

            if (query.WareId != null)
            {
                wareTrademarkCollections.Add(new List<WareTrademark> { await GetByWareId(query.WareId.Value) });
            }

            if (!wareTrademarkCollections.Any())
            {
                return new List<WareTrademark>();
            }

            var result = wareTrademarkCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
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
