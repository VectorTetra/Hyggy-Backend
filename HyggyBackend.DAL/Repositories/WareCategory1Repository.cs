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

        public async Task<IEnumerable<WareCategory1>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.WareCategories1.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<WareCategory1>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            return await _context.WareCategories1.Where(x => x.JSONStructureFilePath.Contains(JSONStructureFilePathSubstring)).ToListAsync();
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

        public async Task<IEnumerable<WareCategory1>> GetByQuery(WareCategory1QueryDAL query)
        {
            var WareCategories1Collection = new List<IEnumerable<WareCategory1>>();

            if (query.JSONStructureFilePathSubstring != null)
            {
                WareCategories1Collection.Add(await GetByJSONStructureFilePathSubstring(query.JSONStructureFilePathSubstring));
            }

            if (query.WareCategory2Id != null)
            {
                WareCategories1Collection.Add(await GetByWareCategory2Id(query.WareCategory2Id.Value));
            }

            if (query.WareCategory2NameSubstring != null)
            {
                WareCategories1Collection.Add(await GetByWareCategory2NameSubstring(query.WareCategory2NameSubstring));
            }

            if (query.WareCategory3Id != null)
            {
                WareCategories1Collection.Add(await GetByWareCategory3Id(query.WareCategory3Id.Value));
            }

            if (query.WareCategory3NameSubstring != null)
            {
                WareCategories1Collection.Add(await GetByWareCategory3NameSubstring(query.WareCategory3NameSubstring));
            }

            return WareCategories1Collection.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
