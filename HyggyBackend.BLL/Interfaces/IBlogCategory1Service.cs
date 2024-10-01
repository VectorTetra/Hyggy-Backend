using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IBlogCategory1Service
    {
        Task<BlogCategory1DTO> GetById(long id);
        Task<BlogCategory1DTO> GetByBlogId(long blogId);
        Task<BlogCategory1DTO> GetByBlogCategory2Id(long blogCategory2Id);
        Task<IEnumerable<BlogCategory1DTO>> GetByFilePathSubstring(string FilePathSubstring);
        Task<IEnumerable<BlogCategory1DTO>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring);
        Task<IEnumerable<BlogCategory1DTO>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring);
        Task<IEnumerable<BlogCategory1DTO>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring);
        Task<IEnumerable<BlogCategory1DTO>> GetByBlogTitleSubstring(string BlogTitleSubstring);
        Task<IEnumerable<BlogCategory1DTO>> GetByBlogKeywordSubstring(string BlogKeywordSubstring);
        Task<IEnumerable<BlogCategory1DTO>> GetPagedBlogCategories1(int PageNumber, int PageSize);
        Task<IEnumerable<BlogCategory1DTO>> GetByQuery(BlogCategory1QueryBLL queryBLL);
        Task<BlogCategory1DTO> AddBlogCategory1(BlogCategory1DTO BlogCategory1DTO);
        Task<BlogCategory1DTO> UpdateBlogCategory1(BlogCategory1DTO BlogCategory1DTO);
        Task<BlogCategory1DTO> DeleteBlogCategory1(long id);
    }
}
