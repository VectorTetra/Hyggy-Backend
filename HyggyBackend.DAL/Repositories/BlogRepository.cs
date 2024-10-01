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
        public async Task<IEnumerable<Blog>> GetByQuery(BlogQueryDAL queryDAL)
        {
            var blogCollections = new List<IEnumerable<Blog>>();
            if (queryDAL.Id.HasValue)
            {
                var rr = await GetById(queryDAL.Id.Value);
                if (rr != null) blogCollections.Add(new List<Blog> { rr });
            }
            if (!string.IsNullOrEmpty(queryDAL.Keyword))
            {
                blogCollections.Add(await GetByKeywordSubstring(queryDAL.Keyword));
            }
            if (!string.IsNullOrEmpty(queryDAL.BlogTitle))
            {
                blogCollections.Add(await GetByTitleSubstring(queryDAL.BlogTitle));
            }
            if (!string.IsNullOrEmpty(queryDAL.FilePath))
            {
                blogCollections.Add(await GetByFilePathSubstring(queryDAL.FilePath));
            }
            if (!string.IsNullOrEmpty(queryDAL.PreviewImagePath))
            {
                blogCollections.Add(await GetByPreviewImagePathSubstring(queryDAL.PreviewImagePath));
            }
            if (queryDAL.BlogCategory1Id.HasValue)
            {
                blogCollections.Add(await GetByBlogCategory1Id(queryDAL.BlogCategory1Id.Value));
            }
            if (!string.IsNullOrEmpty(queryDAL.BlogCategory2Name))
            {
                blogCollections.Add(await GetByBlogCategory2NameSubstring(queryDAL.BlogCategory2Name));
            }
            if (queryDAL.BlogCategory2Id.HasValue)
            {
                blogCollections.Add(await GetByBlogCategory2Id(queryDAL.BlogCategory2Id.Value));
            }
            if (!string.IsNullOrEmpty(queryDAL.BlogCategory1Name))
            {
                blogCollections.Add(await GetByBlogCategory1NameSubstring(queryDAL.BlogCategory1Name));
            }

            if (!blogCollections.Any())
            {
                return new List<Blog>();
            }
            if (queryDAL.PageNumber.HasValue && queryDAL.PageSize.HasValue)
            {
                return blogCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                   .Skip((queryDAL.PageNumber.Value - 1) * queryDAL.PageSize.Value)
                   .Take(queryDAL.PageSize.Value);
            }
            return blogCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
