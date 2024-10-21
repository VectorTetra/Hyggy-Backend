using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly HyggyContext _context;
        public BlogRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<Blog?> GetById(long id)
        {
            return await _context.Blogs.FindAsync(id);
        }
        public async Task<IEnumerable<Blog>> GetByKeywordSubstring(string keyword)
        {
            return await _context.Blogs
             .Where(b => b.Keywords.Contains(keyword))
             .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<Blog>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }
        public async Task<IEnumerable<Blog>> GetByTitleSubstring(string title)
        {
            return await _context.Blogs
             .Where(b => b.BlogTitle.Contains(title))
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByFilePathSubstring(string FilePathSubstring)
        {
            return await _context.Blogs
             .Where(b => b.FilePath.Contains(FilePathSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring)
        {
            return await _context.Blogs
             .Where(b => b.PreviewImagePath.Contains(PreviewImagePathSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByBlogCategory1Id(long blogCategory1Id)
        {
            return await _context.Blogs
             .Where(b => b.BlogCategory2.BlogCategory1.Id == blogCategory1Id)
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByBlogCategory2NameSubstring(string blogCategory2NameSubstring)
        {
            return await _context.Blogs
             .Where(b => b.BlogCategory2.Name.Contains(blogCategory2NameSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByBlogCategory2Id(long blogCategory2Id)
        {
            return await _context.Blogs
             .Where(b => b.BlogCategory2.Id == blogCategory2Id)
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring)
        {
            return await _context.Blogs
             .Where(b => b.BlogCategory2.BlogCategory1.Name.Contains(BlogCategory1NameSubstring))
             .ToListAsync();
        }
        public async Task<IEnumerable<Blog>> GetPagedBlogs(int PageNumber, int PageSize)
        {
            return await _context.Blogs
             .Skip((PageNumber - 1) * PageSize)
             .Take(PageSize)
             .ToListAsync();
        }

        public async IAsyncEnumerable<Blog> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var blog = await GetById(id);  // Виклик методу репозиторію
                if (blog != null)
                {
                    yield return blog;
                }
            }
        }
        public async Task<IEnumerable<Blog>> GetByQuery(BlogQueryDAL query)
        {
            var collections = new List<IEnumerable<Blog>>();

            if (!string.IsNullOrEmpty(query.QueryAny))
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<Blog> { await GetById(id) });
                }
                collections.Add(await GetByKeywordSubstring(query.QueryAny));
                collections.Add(await GetByTitleSubstring(query.QueryAny));
                collections.Add(await GetByFilePathSubstring(query.QueryAny));
                collections.Add(await GetByPreviewImagePathSubstring(query.QueryAny));
                collections.Add(await GetByBlogCategory2NameSubstring(query.QueryAny));
                collections.Add(await GetByBlogCategory1NameSubstring(query.QueryAny));
            }
            else
            {
                if (query.Id.HasValue)
                {
                    var res = await GetById(query.Id.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Blog> { res });
                    }
                }
                if (!string.IsNullOrEmpty(query.Keyword))
                {
                    collections.Add(await GetByKeywordSubstring(query.Keyword));
                }
                if (!string.IsNullOrEmpty(query.BlogTitle))
                {
                    collections.Add(await GetByTitleSubstring(query.BlogTitle));
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
                if (!string.IsNullOrEmpty(query.BlogCategory2Name))
                {
                    collections.Add(await GetByBlogCategory2NameSubstring(query.BlogCategory2Name));
                }
                if (query.BlogCategory2Id.HasValue)
                {
                    collections.Add(await GetByBlogCategory2Id(query.BlogCategory2Id.Value));
                }
                if (!string.IsNullOrEmpty(query.BlogCategory1Name))
                {
                    collections.Add(await GetByBlogCategory1NameSubstring(query.BlogCategory1Name));
                }
                if (!string.IsNullOrEmpty(query.StringIds))
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<Blog>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Blogs
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (!string.IsNullOrEmpty(query.QueryAny) && collections.Any())
            {
                // Використовуємо Union для об'єднання результатів
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                // Використовуємо Intersect для знаходження записів, які задовольняють всі умови
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }

            if (query.Sorting != null)
            {
                // Сортування за вказаними критеріями
                switch (query.Sorting)
                {
                    case "BlogTitleAsc":
                        result = result.OrderBy(b => b.BlogTitle).ToList();
                        break;
                    case "BlogTitleDesc":
                        result = result.OrderByDescending(b => b.BlogTitle).ToList();
                        break;
                    case "FilePathAsc":
                        result = result.OrderBy(b => b.FilePath).ToList();
                        break;
                    case "FilePathDesc":
                        result = result.OrderByDescending(b => b.FilePath).ToList();
                        break;
                    case "PreviewImagePathAsc":
                        result = result.OrderBy(b => b.PreviewImagePath).ToList();
                        break;
                    case "PreviewImagePathDesc":
                        result = result.OrderByDescending(b => b.PreviewImagePath).ToList();
                        break;
                    case "BlogCategory1IdAsc":
                        result = result.OrderBy(b => b.BlogCategory2.BlogCategory1.Id).ToList();
                        break;
                    case "BlogCategory1IdDesc":
                        result = result.OrderByDescending(b => b.BlogCategory2.BlogCategory1.Id).ToList();
                        break;
                    case "BlogCategory2IdAsc":
                        result = result.OrderBy(b => b.BlogCategory2.Id).ToList();
                        break;
                    case "BlogCategory2IdDesc":
                        result = result.OrderByDescending(b => b.BlogCategory2.Id).ToList();
                        break;
                    case "IdAsc":
                        result = result.OrderBy(b => b.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(b => b.Id).ToList();
                        break;
                    case "KeywordAsc":
                        result = result.OrderBy(b => b.Keywords).ToList();
                        break;
                    case "KeywordDesc":
                        result = result.OrderByDescending(b => b.Keywords).ToList();
                        break;
                    case "BlogCategory1NameAsc":
                        result = result.OrderBy(b => b.BlogCategory2.BlogCategory1.Name).ToList();
                        break;
                    case "BlogCategory1NameDesc":
                        result = result.OrderByDescending(b => b.BlogCategory2.BlogCategory1.Name).ToList();
                        break;
                    case "BlogCategory2NameAsc":
                        result = result.OrderBy(b => b.BlogCategory2.Name).ToList();
                        break;
                    case "BlogCategory2NameDesc":
                        result = result.OrderByDescending(b => b.BlogCategory2.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<Blog>();
        }

        public async Task AddBlog(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
        }
        public void UpdateBlog(Blog blog)
        {
            _context.Entry(blog).State = EntityState.Modified;
        }
        public async Task DeleteBlog(long id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null) { _context.Blogs.Remove(blog); }
        }
    }
}
