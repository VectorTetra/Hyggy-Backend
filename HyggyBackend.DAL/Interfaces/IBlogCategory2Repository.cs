using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IBlogCategory2Repository
    {
        Task<BlogCategory2> GetById(long id);
        Task<BlogCategory2> GetByBlogId(long blogId);
        Task<IEnumerable<BlogCategory2>> GetByStringIds(string stringIds);
        Task<IEnumerable<BlogCategory2>> GetByBlogCategory1Id(long blogCategory1Id);
        Task<IEnumerable<BlogCategory2>> GetByBlogTitle(string title);
        Task<IEnumerable<BlogCategory2>> GetByBlogKeyword(string keyword);
        Task<IEnumerable<BlogCategory2>> GetByFilePathSubstring(string filePath);
        Task<IEnumerable<BlogCategory2>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring);
        Task<IEnumerable<BlogCategory2>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring);
        Task<IEnumerable<BlogCategory2>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring);
        Task<IEnumerable<BlogCategory2>> GetPagedBlogCategories2(int PageNumber, int PageSize);
        IAsyncEnumerable<BlogCategory2> GetByIdsAsync(IEnumerable<long> ids);
        Task<IEnumerable<BlogCategory2>> GetByQuery(BlogCategory2QueryDAL queryDAL);
        Task AddBlogCategory2(BlogCategory2 blogCategory2);
        void UpdateBlogCategory2(BlogCategory2 blogCategory2);
        Task DeleteBlogCategory2(long id);
    }
}
