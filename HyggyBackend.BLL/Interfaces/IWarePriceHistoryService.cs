using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWarePriceHistoryService
    {
        Task<WarePriceHistoryDTO?> GetById(long id);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByWareId(long wareId);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<WarePriceHistoryDTO>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<WarePriceHistoryDTO>> GetByQuery(WarePriceHistoryQueryBLL query);
        Task<WarePriceHistoryDTO> Create(WarePriceHistoryDTO warePriceHistoryDTO);
        Task<WarePriceHistoryDTO> Update(WarePriceHistoryDTO warePriceHistoryDTO);
        Task<WarePriceHistoryDTO> Delete(long id);
    }
}
