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
            var WareCategories2Collection = new List<IEnumerable<WareCategory2>>();

            if (query.Id != null)
            {
                WareCategories2Collection.Add(await _context.WareCategories2.Where(x => x.Id == query.Id).ToListAsync());
            }

            if (query.NameSubstring != null)
            {
                WareCategories2Collection.Add(await GetByNameSubstring(query.NameSubstring));
            }
            if (query.WareCategory1Id != null)
            {
                WareCategories2Collection.Add(await GetByWareCategory1Id(query.WareCategory1Id.Value));
            }
            if (query.WareCategory1NameSubstring != null)
            {
                WareCategories2Collection.Add(await GetByWareCategory1NameSubstring(query.WareCategory1NameSubstring));
            }
            if (query.WareCategory3Id != null)
            {
                WareCategories2Collection.Add(await GetByWareCategory3Id(query.WareCategory3Id.Value));
            }
            if (query.WareCategory3NameSubstring != null)
            {
                WareCategories2Collection.Add(await GetByWareCategory3NameSubstring(query.WareCategory3NameSubstring));
            }

            if (!WareCategories2Collection.Any())
            {
                return new List<WareCategory2>();
            }

            if(query.PageNumber != null && query.PageSize != null)
            {
                return WareCategories2Collection.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                   .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                   .Take(query.PageSize.Value);
            }
            return WareCategories2Collection.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
