using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO?> GetById(long id);
        Task<IEnumerable<OrderDTO>> GetByDeliveryAddressSubstring(string deliveryAddressSubstring);
        Task<IEnumerable<OrderDTO>> GetAll();
        Task<IEnumerable<OrderDTO>> GetByOrderDateRange(DateTime minOrderDate, DateTime maxOrderDate);
        Task<IEnumerable<OrderDTO>> GetByPhoneSubstring(string phoneSubstring);
        Task<IEnumerable<OrderDTO>> GetByCommentSubstring(string commentSubstring);
        Task<IEnumerable<OrderDTO>> GetByStatusId(long statusId);
        Task<IEnumerable<OrderDTO>> GetByStatusNameSubstring(string statusNameSubstring);
        Task<IEnumerable<OrderDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring);
        Task<IEnumerable<OrderDTO>> GetByOrderItemId(long orderItemId);
        Task<IEnumerable<OrderDTO>> GetByWareId(long wareId);
        Task<IEnumerable<OrderDTO>> GetByWarePriceHistoryId(long warePriceHistoryId);
        Task<IEnumerable<OrderDTO>> GetByCustomerId(long customerId);
        Task<IEnumerable<OrderDTO>> GetByShopId(long shopId);
        Task<IEnumerable<OrderDTO>> GetByQuery(OrderQueryBLL query);
        Task Create(OrderDTO order);
        Task Update(OrderDTO order);
        Task Delete(long id);
    }
}
