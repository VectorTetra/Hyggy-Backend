using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareCategory2Service
    {
        Task<WareCategory2DTO?> GetById(long id);
        Task<IEnumerable<WareCategory2DTO>> GetPagedCategories(int pageNumber, int pageSize);
        Task<IEnumerable<WareCategory2DTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareCategory2DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring);
        Task<IEnumerable<WareCategory2DTO>> GetByWareCategory1Id(long id);
        Task<IEnumerable<WareCategory2DTO>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring);
        Task<IEnumerable<WareCategory2DTO>> GetByWareCategory3Id(long id);
        Task<IEnumerable<WareCategory2DTO>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring);
        Task<IEnumerable<WareCategory2DTO>> GetByQuery(WareCategory2QueryBLL query);
        Task<WareCategory2DTO> Create(WareCategory2DTO order);
        Task<WareCategory2DTO> Update(WareCategory2DTO order);
        Task<WareCategory2DTO> Delete(long id);
    }
}
