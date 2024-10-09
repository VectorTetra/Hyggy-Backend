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
            var WareCategories3Collection = new List<IEnumerable<WareCategory3>>();


            if(query.Id != null)
            {
                WareCategories3Collection.Add(new List<WareCategory3> { await GetById(query.Id.Value) });
            }

            if (query.NameSubstring != null)
            {
                WareCategories3Collection.Add(await GetByNameSubstring(query.NameSubstring));
            }
            if (query.WareCategory1Id != null)
            {
                WareCategories3Collection.Add(await GetByWareCategory1Id(query.WareCategory1Id.Value));
            }
            if (query.WareCategory1NameSubstring != null)
            {
                WareCategories3Collection.Add(await GetByWareCategory1NameSubstring(query.WareCategory1NameSubstring));
            }
            if (query.WareCategory2Id != null)
            {
                WareCategories3Collection.Add(await GetByWareCategory2Id(query.WareCategory2Id.Value));
            }
            if (query.WareCategory2NameSubstring != null)
            {
                WareCategories3Collection.Add(await GetByWareCategory2NameSubstring(query.WareCategory2NameSubstring));
            }
            if (query.WareId != null)
            {
                WareCategories3Collection.Add(await GetByWareId(query.WareId.Value));
            }
            if (query.WareArticle != null)
            {
                WareCategories3Collection.Add(await GetByWareArticle(query.WareArticle.Value));
            }
            if (query.WareNameSubstring != null)
            {
                WareCategories3Collection.Add(await GetByWareNameSubstring(query.WareNameSubstring));
            }
            if (query.WareDescriptionSubstring != null)
            {
                WareCategories3Collection.Add(await GetByWareDescriptionSubstring(query.WareDescriptionSubstring));
            }
            if (!WareCategories3Collection.Any())
            {
                return new List<WareCategory3>();
            }
            if (query.PageNumber != null && query.PageSize != null)
            {
                return WareCategories3Collection.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value);
            }
            return WareCategories3Collection.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
