using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO?> GetById(long id);
        Task<IEnumerable<OrderDTO>> GetByAddressId(long addressId);
        Task<IEnumerable<OrderDTO>> GetByStreet(string streetSubstring);
        Task<IEnumerable<OrderDTO>> GetByHouseNumber(string houseNumber);
        Task<IEnumerable<OrderDTO>> GetByCity(string city);
        Task<IEnumerable<OrderDTO>> GetByPostalCode(string postalCode);
        Task<IEnumerable<OrderDTO>> GetByState(string state);
        Task<IEnumerable<OrderDTO>> GetByLatitudeAndLongitude(double latitude, double longitude);
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
        Task<OrderDTO> Create(OrderDTO order);
        Task<OrderDTO> Update(OrderDTO order);
        Task<OrderDTO> Delete(long id);
    }
}
