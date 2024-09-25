using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class WareImageRepository : IWareImageRepository
    {
        private readonly HyggyContext _context;

        public WareImageRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<WareImage?> GetById(long id) 
        {
            return await _context.WareImages.FindAsync(id);
        }
        public async Task<IEnumerable<WareImage>> GetByWareId(long wareId) 
        {
            return await _context.WareImages.Where(x => x.Ware.Id == wareId).ToListAsync();
        }

        public async Task<IEnumerable<WareImage>> GetByWareArticle(long wareArticle)
        {
            return await _context.WareImages.Where(x => x.Ware.Article == wareArticle).ToListAsync();
        }
        public async Task<IEnumerable<WareImage>> GetByPathSubstring(string path) 
        {
            return await _context.WareImages.Where(x => x.Path.Contains(path)).ToListAsync();
        }
        public async Task<IEnumerable<WareImage>> GetByQuery(WareImageQueryDAL queryDAL)
        {
            var wareImageCollections = new List<IEnumerable<WareImage>>();

            if (queryDAL.Id != null)
            {
                wareImageCollections.Add(new List<WareImage> { await GetById(queryDAL.Id.Value) });
            }

            if (queryDAL.WareId != null)
            {
                wareImageCollections.Add(await GetByWareId(queryDAL.WareId.Value));
            }

            if (queryDAL.WareArticle != null)
            {
                wareImageCollections.Add(await GetByWareArticle(queryDAL.WareArticle.Value));
            }
            
            if (queryDAL.Path != null)
            {
                wareImageCollections.Add(await GetByPathSubstring(queryDAL.Path));
            }
            return wareImageCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());

        }
        public async Task Create(WareImage wareImage) 
        {
            await _context.WareImages.AddAsync(wareImage);
        }
        public void Update(WareImage wareImage) 
        {
            _context.Entry(wareImage).State = EntityState.Modified;
        }
        public async Task Delete(long id) 
        {
            var wareImage = await _context.WareImages.FindAsync(id);
            if (wareImage != null) { _context.WareImages.Remove(wareImage); }
        }
    }
}
