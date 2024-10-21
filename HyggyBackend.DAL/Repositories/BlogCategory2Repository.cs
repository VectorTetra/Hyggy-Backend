using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.DAL.Repositories
{
    public class BlogCategory2Repository : IBlogCategory2Repository
    {
        private readonly HyggyContext _context;

        public BlogCategory2Repository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<BlogCategory2?> GetById(long id)
        {
            return await _context.BlogCategories2.FindAsync(id);
        }
        public async Task<BlogCategory2?> GetByBlogId(long blogId)
        {
            return await _context.BlogCategories2
             .FirstOrDefaultAsync(bc => bc.Blogs.Any(bl => bl.Id == blogId));
        }
        public async Task<IEnumerable<BlogCategory2>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var bbcc22 = new List<BlogCategory2>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var bc2 in GetByIdsAsync(ids))
            {
                bbcc22.Add(bc2);
            }
            return bbcc22;
        }
        public async Task<IEnumerable<BlogCategory2>> GetByBlogCategory1Id(long blogCategory1Id)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.BlogCategory1.Id == blogCategory1Id)
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetByBlogTitle(string title)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.Blogs.Any(bl => bl.BlogTitle.Contains(title)))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetByBlogKeyword(string keyword)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.Blogs.Any(bl => bl.Keywords.Contains(keyword)))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetByFilePathSubstring(string FilePathSubstring)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.Blogs.Any(bl => bl.FilePath.Contains(FilePathSubstring)))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.Blogs.Any(bl => bl.PreviewImagePath.Contains(PreviewImagePathSubstring)))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.BlogCategory1.Name.Contains(BlogCategory1NameSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring)
        {
            return await _context.BlogCategories2
             .Where(bc => bc.Name.Contains(BlogCategory2NameSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory2>> GetPagedBlogCategories2(int PageNumber, int PageSize)
        {
            return await _context.BlogCategories2
             .Skip((PageNumber - 1) * PageSize)
             .Take(PageSize)
             .ToListAsync();
        }

        public async IAsyncEnumerable<BlogCategory2> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var category2 = await GetById(id);  // Виклик методу репозиторію
                if (category2 != null)
                {
                    yield return category2;
                }
            }
        }
        public async Task<IEnumerable<BlogCategory2>> GetByQuery(BlogCategory2QueryDAL query)
        {
            var collections = new List<IEnumerable<BlogCategory2>>();

            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<BlogCategory2> { await GetById(id) });
                }
                collections.Add(await GetByBlogCategory2NameSubstring(query.QueryAny));
                collections.Add(await GetByBlogCategory1NameSubstring(query.QueryAny));
                collections.Add(await GetByBlogTitle(query.QueryAny));
                collections.Add(await GetByBlogKeyword(query.QueryAny));
                collections.Add(await GetByFilePathSubstring(query.QueryAny));
                collections.Add(await GetByPreviewImagePathSubstring(query.QueryAny));
            }
            else
            {
                if (query.Id.HasValue)
                {
                    var proto = await GetById(query.Id.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<BlogCategory2> { proto });
                    }
                }
                if (!string.IsNullOrEmpty(query.BlogCategory2Name))
                {
                    collections.Add(await GetByBlogCategory2NameSubstring(query.BlogCategory2Name));
                }
                if (!string.IsNullOrEmpty(query.BlogCategory1Name))
                {
                    collections.Add(await GetByBlogCategory1NameSubstring(query.BlogCategory1Name));
                }
                if (!string.IsNullOrEmpty(query.BlogTitle))
                {
                    collections.Add(await GetByBlogTitle(query.BlogTitle));
                }
                if (!string.IsNullOrEmpty(query.Keyword))
                {
                    collections.Add(await GetByBlogKeyword(query.Keyword));
                }
                if (!string.IsNullOrEmpty(query.FilePath))
                {
                    collections.Add(await GetByFilePathSubstring(query.FilePath));
                }
                if (!string.IsNullOrEmpty(query.PreviewImagePath))
                {
                    collections.Add(await GetByPreviewImagePathSubstring(query.PreviewImagePath));
                }
                if (query.BlogCategory1Id.HasValue)
                {
                    collections.Add(await GetByBlogCategory1Id(query.BlogCategory1Id.Value));
                }
                if (query.BlogId.HasValue)
                {
                    var proto = await GetByBlogId(query.BlogId.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<BlogCategory2> { proto });
                    }
                }
                if (!string.IsNullOrEmpty(query.StringIds))
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<BlogCategory2>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.BlogCategories2
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Використовуємо Union для об'єднання результатів
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                // Використовуємо Intersect для знаходження записів, які задовольняють всі умови
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "BlogCategory2NameAsc":
                        result = result.OrderBy(bc => bc.Name).ToList();
                        break;
                    case "BlogCategory2NameDesc":
                        result = result.OrderByDescending(bc => bc.Name).ToList();
                        break;
                    case "BlogCategory1NameAsc":
                        result = result.OrderBy(bc => bc.BlogCategory1.Name).ToList();
                        break;
                    case "BlogCategory1NameDesc":
                        result = result.OrderByDescending(bc => bc.BlogCategory1.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<BlogCategory2>();
        }

        public async Task AddBlogCategory2(BlogCategory2 blogCategory2)
        {
            await _context.BlogCategories2.AddAsync(blogCategory2);
        }
        public void UpdateBlogCategory2(BlogCategory2 blogCategory2)
        {
            _context.Entry(blogCategory2).State = EntityState.Modified;
        }
        public async Task DeleteBlogCategory2(long id)
        {
            var blogCategory = await _context.BlogCategories2.FindAsync(id);
            if (blogCategory != null) { _context.BlogCategories2.Remove(blogCategory); }
        }
    }
}
