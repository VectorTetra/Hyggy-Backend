using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareImageService
    {
        Task<WareImageDTO?> GetById(long id);
        Task<IEnumerable<WareImageDTO>> GetByWareId(long wareId);
        Task<IEnumerable<WareImageDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareImageDTO>> GetByWareArticle(long wareArticle);
        Task<IEnumerable<WareImageDTO>> GetByPathSubstring(string path);
        Task<IEnumerable<WareImageDTO>> GetByQuery(WareImageQueryBLL query);
        Task<WareImageDTO> Create(WareImageDTO wareImage);
        Task<WareImageDTO> Update(WareImageDTO wareImage);
        Task<WareImageDTO> Delete(long id);
    }

}