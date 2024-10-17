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

        public async Task<IEnumerable<WareImage>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareImage>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
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
        public async Task<IEnumerable<WareImage>> GetByQuery(WareImageQueryDAL query)
        {
            var collections = new List<IEnumerable<WareImage>>();

            if (query.Id != null)
            {
                var proto = await GetById(query.Id.Value);
                if (proto != null)
                {
                    collections.Add(new List<WareImage> { proto });
                }
            }
            if (query.WareId != null)
            {
                collections.Add(await GetByWareId(query.WareId.Value));
            }
            if (query.WareArticle != null)
            {
                collections.Add(await GetByWareArticle(query.WareArticle.Value));
            }
            if (query.Path != null)
            {
                collections.Add(await GetByPathSubstring(query.Path));
            }
            if (query.StringIds != null)
            {
                collections.Add(await GetByStringIds(query.StringIds));
            }
            var result = new List<WareImage>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.WareImages
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .ToList();
            }
            else
            {
                result = (List<WareImage>)collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
                    case "WareIdAsc":
                        result = result.OrderBy(ware => ware.Ware.Id).ToList();
                        break;
                    case "WareIdDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Id).ToList();
                        break;
                    case "WareArticleAsc":
                        result = result.OrderBy(ware => ware.Ware.Article).ToList();
                        break;
                    case "WareArticleDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Article).ToList();
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
                return new List<WareImage>();
            }
            return result;

        }

        public async IAsyncEnumerable<WareImage> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var wareImage = await GetById(id);  // Виклик методу репозиторію
                if (wareImage != null)
                {
                    yield return wareImage;
                }
            }
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
