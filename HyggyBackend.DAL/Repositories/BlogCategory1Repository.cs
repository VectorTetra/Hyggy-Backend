using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.DAL.Repositories
{
    public class BlogCategory1Repository : IBlogCategory1Repository
    {
        private readonly HyggyContext _context;

        public BlogCategory1Repository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<BlogCategory1> GetById(long id)
        {
            return await _context.BlogCategories1.FindAsync(id);
        }
        public async Task<BlogCategory1?> GetByBlogId(long blogId)
        {
            return await _context.BlogCategories1
             .FirstOrDefaultAsync(bc => bc.BlogCategories2.Any(bl => bl.Blogs.Any(b => b.Id == blogId)));
        }
        public async Task<BlogCategory1?> GetByBlogCategory2Id(long blogCategory2Id)
        {
            return await _context.BlogCategories1
             .FirstOrDefaultAsync(bc => bc.BlogCategories2.Any(bl => bl.Id == blogCategory2Id));
        }
        public async Task<IEnumerable<BlogCategory1>> GetByStringIds(string StringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = StringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<BlogCategory1>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }
        public async Task<IEnumerable<BlogCategory1>> GetByFilePathSubstring(string FilePathSubstring)
        {
            return await _context.BlogCategories1
             .Where(bc => bc.BlogCategories2.Any(bl => bl.Blogs.Any(b => b.FilePath.Contains(FilePathSubstring))))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory1>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring)
        {
            return await _context.BlogCategories1
             .Where(bc => bc.BlogCategories2.Any(bl => bl.Blogs.Any(b => b.PreviewImagePath.Contains(PreviewImagePathSubstring))))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory1>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring)
        {
            return await _context.BlogCategories1
             .Where(bc => bc.BlogCategories2.Any(bl => bl.Name.Contains(BlogCategory2NameSubstring)))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory1>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring)
        {
            return await _context.BlogCategories1
             .Where(bc => bc.Name.Contains(BlogCategory1NameSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory1>> GetByBlogTitleSubstring(string BlogTitleSubstring)
        {
            return await _context.BlogCategories1
             .Where(bc => bc.BlogCategories2.Any(bl => bl.Blogs.Any(b => b.BlogTitle.Contains(BlogTitleSubstring))))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory1>> GetByBlogKeywordSubstring(string BlogKeywordSubstring)
        {
            return await _context.BlogCategories1
             .Where(bc => bc.BlogCategories2.Any(bl => bl.Blogs.Any(b => b.Keywords.Contains(BlogKeywordSubstring))))
             .ToListAsync();
        }
        public async Task<IEnumerable<BlogCategory1>> GetPagedBlogCategories1(int PageNumber, int PageSize)
        {
            return await _context.BlogCategories1
             .Skip((PageNumber - 1) * PageSize)
             .Take(PageSize)
             .ToListAsync();
        }

        public async IAsyncEnumerable<BlogCategory1> GetByIdsAsync(IEnumerable<long> ids)
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
        public async Task<IEnumerable<BlogCategory1>> GetByQuery(BlogCategory1QueryDAL query)
        {
            var collections = new List<IEnumerable<BlogCategory1>>();

            // Обробка QueryAny
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<BlogCategory1> { await GetById(id) });
                }
                collections.Add(await GetByFilePathSubstring(query.QueryAny));
                collections.Add(await GetByPreviewImagePathSubstring(query.QueryAny));
                collections.Add(await GetByBlogCategory2NameSubstring(query.QueryAny));
                collections.Add(await GetByBlogCategory1NameSubstring(query.QueryAny));
                collections.Add(await GetByBlogTitleSubstring(query.QueryAny));
                collections.Add(await GetByBlogKeywordSubstring(query.QueryAny));
                
            }
            else
            {
                if (query.Id.HasValue)
                {
                    var proto = await GetById(query.Id.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<BlogCategory1> { proto });
                    }
                }
                if (query.BlogId.HasValue)
                {
                    var proto = await GetByBlogId(query.BlogId.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<BlogCategory1> { proto });
                    }
                }
                if (query.BlogCategory2Id.HasValue)
                {
                    var proto = await GetByBlogCategory2Id(query.BlogCategory2Id.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<BlogCategory1> { proto });
                    }
                }
                if (!string.IsNullOrEmpty(query.FilePath))
                {
                    collections.Add(await GetByFilePathSubstring(query.FilePath));
                }
                if (!string.IsNullOrEmpty(query.PreviewImagePath))
                {
                    collections.Add(await GetByPreviewImagePathSubstring(query.PreviewImagePath));
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
                    collections.Add(await GetByBlogTitleSubstring(query.BlogTitle));
                }
                if (!string.IsNullOrEmpty(query.Keyword))
                {
                    collections.Add(await GetByBlogKeywordSubstring(query.Keyword));
                }
                if (!string.IsNullOrEmpty(query.StringIds))
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<BlogCategory1>();

            // Обробка результатів
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.BlogCategories1
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
                var nonEmptyCollections = collections.Where(collection => collection.Any()).ToList();

                // Перетин результатів з відфільтрованих колекцій
                if (nonEmptyCollections.Any())
                {
                    result = nonEmptyCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
                }
                else
                {
                    result = new List<BlogCategory1>(); // Повертаємо порожній список, якщо всі колекції були порожні
                }
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "IdAsc":
                        result = result.OrderBy(bc => bc.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(bc => bc.Id).ToList();
                        break;
                    case "NameAsc":
                        result = result.OrderBy(bc => bc.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(bc => bc.Name).ToList();
                        break;
                    case "BlogCategory2IdAsc":
                        result = result.OrderBy(bc => bc.BlogCategories2.First().Id).ToList();
                        break;
                    case "BlogCategory2IdDesc":
                        result = result.OrderByDescending(bc => bc.BlogCategories2.First().Id).ToList();
                        break;
                    case "BlogCategory2NameAsc":
                        result = result.OrderBy(bc => bc.BlogCategories2.First().Name).ToList();
                        break;
                    case "BlogCategory2NameDesc":
                        result = result.OrderByDescending(bc => bc.BlogCategories2.First().Name).ToList();
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

            return result.Any() ? result : new List<BlogCategory1>();
        }
        public async Task AddBlogCategory1(BlogCategory1 blogCategory1)
        {
            await _context.BlogCategories1.AddAsync(blogCategory1);
        }
        public void UpdateBlogCategory1(BlogCategory1 blogCategory1)
        {
            _context.Entry(blogCategory1).State = EntityState.Modified;
        }
        public async Task DeleteBlogCategory1(long id)
        {
            var blogCategory1 = await GetById(id);
            if (blogCategory1 != null)
            {
                _context.BlogCategories1.Remove(blogCategory1);
            }
        }
    }
}
