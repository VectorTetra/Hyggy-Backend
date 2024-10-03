using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IBlogCategory2Service
    {
        Task<BlogCategory2DTO> GetById(long id);
        Task<BlogCategory2DTO> GetByBlogId(long blogId);
        Task<IEnumerable<BlogCategory2DTO>> GetByBlogCategory1Id(long blogCategory1Id);
        Task<IEnumerable<BlogCategory2DTO>> GetByBlogTitle(string title);
        Task<IEnumerable<BlogCategory2DTO>> GetByBlogKeyword(string keyword);
        Task<IEnumerable<BlogCategory2DTO>> GetByFilePathSubstring(string filePath);
        Task<IEnumerable<BlogCategory2DTO>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring);
        Task<IEnumerable<BlogCategory2DTO>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring);
        Task<IEnumerable<BlogCategory2DTO>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring);
        Task<IEnumerable<BlogCategory2DTO>> GetPagedBlogCategories2(int PageNumber, int PageSize);
        Task<IEnumerable<BlogCategory2DTO>> GetByQuery(BlogCategory2QueryBLL queryBLL);
        Task<BlogCategory2DTO> AddBlogCategory2(BlogCategory2DTO blogCategory2);
        Task<BlogCategory2DTO> UpdateBlogCategory2(BlogCategory2DTO blogCategory2);
        Task<BlogCategory2DTO> DeleteBlogCategory2(long id);
    }
}
