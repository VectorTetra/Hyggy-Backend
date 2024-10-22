using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IBlogCategory1Repository
    {
        Task<BlogCategory1> GetById(long id);
        IAsyncEnumerable<BlogCategory1> GetByIdsAsync(IEnumerable<long> ids);
        Task<BlogCategory1> GetByBlogId(long blogId);
        Task<BlogCategory1> GetByBlogCategory2Id(long blogCategory2Id);
        Task<IEnumerable<BlogCategory1>> GetByStringIds(string StringIds);
        Task<IEnumerable<BlogCategory1>> GetByFilePathSubstring(string FilePathSubstring);
        Task<IEnumerable<BlogCategory1>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring);
        Task<IEnumerable<BlogCategory1>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring);
        Task<IEnumerable<BlogCategory1>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring);
        Task<IEnumerable<BlogCategory1>> GetByBlogTitleSubstring(string BlogTitleSubstring);
        Task<IEnumerable<BlogCategory1>> GetByBlogKeywordSubstring(string BlogKeywordSubstring);
        Task<IEnumerable<BlogCategory1>> GetPagedBlogCategories1(int PageNumber, int PageSize);
        Task<IEnumerable<BlogCategory1>> GetByQuery(BlogCategory1QueryDAL queryDAL);
        Task AddBlogCategory1(BlogCategory1 blogCategory1);
        void UpdateBlogCategory1(BlogCategory1 blogCategory1);
        Task DeleteBlogCategory1(long id);
    }
}
