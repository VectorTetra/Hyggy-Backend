using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem?> GetById(long id);

        Task<IEnumerable<OrderItem>> GetByStringIds(string stringIds);
        Task<IEnumerable<OrderItem>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<OrderItem>> GetByQuery(OrderItemQueryDAL query);
        Task<IEnumerable<OrderItem>> GetByOrderId(long orderId);
        Task<IEnumerable<OrderItem>> GetByWareId(long wareId);
        Task<IEnumerable<OrderItem>> GetByPriceHistoryId(long priceHistoryId);
        Task<IEnumerable<OrderItem>> GetByCount(int count);
        IAsyncEnumerable<OrderItem> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(OrderItem orderItem);
        void Update(OrderItem orderItem);
        Task Delete(long id);
    }
}
