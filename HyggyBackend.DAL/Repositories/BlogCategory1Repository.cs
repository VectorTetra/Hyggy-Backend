using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<BlogCategory1>> GetByQuery(BlogCategory1QueryDAL queryDAL)
        {
            var categoryCollections = new List<IEnumerable<BlogCategory1>>();
            if (queryDAL.Id.HasValue)
            {
                categoryCollections.Add(new List<BlogCategory1> { await GetById(queryDAL.Id.Value) });
            }
            if (queryDAL.BlogId.HasValue)
            {
                categoryCollections.Add(new List<BlogCategory1> { await GetByBlogId(queryDAL.BlogId.Value) });
            }
            if (queryDAL.BlogCategory2Id.HasValue)
            {
                categoryCollections.Add(new List<BlogCategory1> { await GetByBlogCategory2Id(queryDAL.BlogCategory2Id.Value) });
            }
            if (!string.IsNullOrEmpty(queryDAL.FilePath))
            {
                categoryCollections.Add(await GetByFilePathSubstring(queryDAL.FilePath));
            }
            if (!string.IsNullOrEmpty(queryDAL.PreviewImagePath))
            {
                categoryCollections.Add(await GetByPreviewImagePathSubstring(queryDAL.PreviewImagePath));
            }
            if (!string.IsNullOrEmpty(queryDAL.BlogCategory2Name))
            {
                categoryCollections.Add(await GetByBlogCategory2NameSubstring(queryDAL.BlogCategory2Name));
            }
            if (!string.IsNullOrEmpty(queryDAL.BlogCategory1Name))
            {
                categoryCollections.Add(await GetByBlogCategory1NameSubstring(queryDAL.BlogCategory1Name));
            }
            if (!string.IsNullOrEmpty(queryDAL.BlogTitle))
            {
                categoryCollections.Add(await GetByBlogTitleSubstring(queryDAL.BlogTitle));
            }
            if (!string.IsNullOrEmpty(queryDAL.Keyword))
            {
                categoryCollections.Add(await GetByBlogKeywordSubstring(queryDAL.Keyword));
            }
            if (!categoryCollections.Any())
            {
                return new List<BlogCategory1>();
            }
            if (queryDAL.PageNumber.HasValue && queryDAL.PageSize.HasValue)
            {
                return categoryCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                   .Skip((queryDAL.PageNumber.Value - 1) * queryDAL.PageSize.Value)
                   .Take(queryDAL.PageSize.Value);
            }
            return categoryCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
