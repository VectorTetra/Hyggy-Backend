using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareCategory1Service
    {
        Task<WareCategory1DTO?> GetById(long id);
        Task<IEnumerable<WareCategory1DTO>> GetPagedCategories(int pageNumber, int pageSize);
        Task<IEnumerable<WareCategory1DTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareCategory1DTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareCategory1DTO>> GetByWareCategory2Id(long id);
        Task<IEnumerable<WareCategory1DTO>> GetByWareCategory2NameSubstring(string WareCategory2NameSubstring);
        Task<IEnumerable<WareCategory1DTO>> GetByWareCategory3Id(long id);
        Task<IEnumerable<WareCategory1DTO>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring);
        Task<IEnumerable<WareCategory1DTO>> GetByQuery(WareCategory1QueryBLL query);
        Task<WareCategory1DTO> Create(WareCategory1DTO order);
        Task<WareCategory1DTO> Update(WareCategory1DTO order);
        Task<WareCategory1DTO> Delete(long id);
    }
}
