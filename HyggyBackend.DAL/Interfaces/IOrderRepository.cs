using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetById(long id);
        Task<IEnumerable<Order>> GetByStringIds(string stringIds);
        Task<IEnumerable<Order>> GetByAddressId(long addressId);
        Task<IEnumerable<Order>> GetByStreet(string streetSubstring);
        Task<IEnumerable<Order>> GetByHouseNumber(string houseNumber);
        Task<IEnumerable<Order>> GetByCity(string city);
        Task<IEnumerable<Order>> GetByPostalCode(string postalCode);
        Task<IEnumerable<Order>> GetByState(string state);
        Task<IEnumerable<Order>> GetByLatitudeAndLongitude(double latitude, double longitude);
        Task<IEnumerable<Order>> GetPagedOrders(int pageNumber, int pageSize);
        Task<IEnumerable<Order>> GetByOrderDateRange(DateTime minOrderDate, DateTime maxOrderDate);
        Task<IEnumerable<Order>> GetByPhoneSubstring(string phoneSubstring);
        Task<IEnumerable<Order>> GetByCommentSubstring(string commentSubstring);
        Task<IEnumerable<Order>> GetByStatusId(long statusId);
        Task<IEnumerable<Order>> GetByStatusNameSubstring(string statusNameSubstring);
        Task<IEnumerable<Order>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring);
        Task<IEnumerable<Order>> GetByOrderItemId(long orderItemId);
        Task<IEnumerable<Order>> GetByWareId(long wareId);
        Task<IEnumerable<Order>> GetByWarePriceHistoryId(long warePriceHistoryId);
        Task<IEnumerable<Order>> GetByCustomerId(string customerId);
        Task<IEnumerable<Order>> GetByShopId(long shopId);
        Task<IEnumerable<Order>> GetByQuery(OrderQueryDAL query);
        IAsyncEnumerable<Order> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(Order order);
        void Update(Order order);
        Task Delete(long id);
    }
}
