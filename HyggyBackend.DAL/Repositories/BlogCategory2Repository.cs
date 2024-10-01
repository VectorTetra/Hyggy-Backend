using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<BlogCategory2>> GetByQuery(BlogCategory2QueryDAL queryDAL)
        {
            var categoryCollections = new List<IEnumerable<BlogCategory2>>();
            if (queryDAL.Id.HasValue)
            {
                categoryCollections.Add(new List<BlogCategory2> { await GetById(queryDAL.Id.Value) });
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
                categoryCollections.Add(await GetByBlogTitle(queryDAL.BlogTitle));
            }
            if (!string.IsNullOrEmpty(queryDAL.Keyword))
            {
                categoryCollections.Add(await GetByBlogKeyword(queryDAL.Keyword));
            }
            if (!string.IsNullOrEmpty(queryDAL.FilePath))
            {
                categoryCollections.Add(await GetByFilePathSubstring(queryDAL.FilePath));
            }
            if (!string.IsNullOrEmpty(queryDAL.PreviewImagePath))
            {
                categoryCollections.Add(await GetByPreviewImagePathSubstring(queryDAL.PreviewImagePath));
            }
            if (queryDAL.BlogCategory1Id.HasValue)
            {
                categoryCollections.Add(await GetByBlogCategory1Id(queryDAL.BlogCategory1Id.Value));
            }
            if (queryDAL.BlogId.HasValue)
            {
                categoryCollections.Add(new List<BlogCategory2> { await GetByBlogId(queryDAL.BlogId.Value) });
            }

            if (!categoryCollections.Any())
            {
                return new List<BlogCategory2>();
            }

            if (queryDAL.PageNumber.HasValue && queryDAL.PageSize.HasValue)
            {
                return categoryCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                   .Skip((queryDAL.PageNumber.Value - 1) * queryDAL.PageSize.Value)
                   .Take(queryDAL.PageSize.Value);
            }
            return categoryCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
