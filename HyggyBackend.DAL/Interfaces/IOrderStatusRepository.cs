using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IOrderStatusRepository
    {
        Task<OrderStatus?> GetById(long id);
        Task<IEnumerable<OrderStatus>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<OrderStatus>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<OrderStatus>> GetByQuery(OrderStatusQueryDAL query);
        IAsyncEnumerable<OrderStatus> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(OrderStatus orderStatus);
        void Update(OrderStatus orderStatus);
        Task Delete(long id);
    }
}
