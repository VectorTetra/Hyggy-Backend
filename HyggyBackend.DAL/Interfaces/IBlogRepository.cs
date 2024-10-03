using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> GetById(long id);
        Task<IEnumerable<Blog>> GetByKeywordSubstring(string keyword);
        Task<IEnumerable<Blog>> GetByTitleSubstring(string title);
        Task<IEnumerable<Blog>> GetByFilePathSubstring(string FilePathSubstring);
        Task<IEnumerable<Blog>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring);
        Task<IEnumerable<Blog>> GetByBlogCategory1Id(long blogCategory1Id);
        Task<IEnumerable<Blog>> GetByBlogCategory2NameSubstring(string blogCategory2NameSubstring);
        Task<IEnumerable<Blog>> GetByBlogCategory2Id(long blogCategory2Id);
        Task<IEnumerable<Blog>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring);
        Task<IEnumerable<Blog>> GetPagedBlogs(int PageNumber, int PageSize);
        IAsyncEnumerable<Blog> GetByIdsAsync(IEnumerable<long> ids);
        Task<IEnumerable<Blog>> GetByQuery(BlogQueryDAL queryDAL);
        Task AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        Task DeleteBlog(long id);
    }
}
