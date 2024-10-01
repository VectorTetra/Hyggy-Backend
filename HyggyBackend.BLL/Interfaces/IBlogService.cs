using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IBlogService
    {
        Task<BlogDTO> GetById(long id);
        Task<IEnumerable<BlogDTO>> GetByKeywordSubstring(string keyword);
        Task<IEnumerable<BlogDTO>> GetByTitleSubstring(string title);
        Task<IEnumerable<BlogDTO>> GetByFilePathSubstring(string FilePathSubstring);
        Task<IEnumerable<BlogDTO>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring);
        Task<IEnumerable<BlogDTO>> GetByBlogCategory1Id(long blogCategory1Id);
        Task<IEnumerable<BlogDTO>> GetByBlogCategory2NameSubstring(string blogCategory2NameSubstring);
        Task<IEnumerable<BlogDTO>> GetByBlogCategory2Id(long blogCategory2Id);
        Task<IEnumerable<BlogDTO>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring);
        Task<IEnumerable<BlogDTO>> GetPagedBlogs(int PageNumber, int PageSize);
        Task<IEnumerable<BlogDTO>> GetByQuery(BlogQueryBLL queryBLL);
        Task<BlogDTO> AddBlog(BlogDTO BlogDTO);
        Task<BlogDTO> UpdateBlog(BlogDTO BlogDTO);
        Task<BlogDTO> DeleteBlog(long id);
    }
}
