using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWarePriceHistoryRepository
    {
        Task<WarePriceHistory?> GetById(long id);
        Task<IEnumerable<WarePriceHistory>> GetByWareId(long wareId);
        Task<IEnumerable<WarePriceHistory>> GetByStringIds(string stringIds);
        Task<IEnumerable<WarePriceHistory>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<WarePriceHistory>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<WarePriceHistory>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<WarePriceHistory>> GetByQuery(WarePriceHistoryQueryDAL query);
        IAsyncEnumerable<WarePriceHistory> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WarePriceHistory warePriceHistory);
        void Update(WarePriceHistory warePriceHistory);
        Task Delete(long id);
    }
}
