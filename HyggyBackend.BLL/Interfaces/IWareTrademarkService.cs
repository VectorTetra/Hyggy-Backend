using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareTrademarkService
    {
        Task<WareTrademarkDTO?> GetById(long id);
        Task<IEnumerable<WareTrademarkDTO>> GetByName(string nameSubstr);
        Task<WareTrademarkDTO?> GetByWareId(long id);
        Task<IEnumerable<WareTrademarkDTO>> GetPagedWareTrademarks(int pageNumber, int pageSize);
        Task<IEnumerable<WareTrademarkDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareTrademarkDTO>> GetByQuery(WareTrademarkQueryBLL query);
        Task<WareTrademarkDTO> Add(WareTrademarkDTO wareTrademark);
        Task<WareTrademarkDTO> Update(WareTrademarkDTO wareTrademark);
        Task<WareTrademarkDTO> Delete(long id);
    }
}
