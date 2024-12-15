using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO?> GetById(long id);
        Task<IEnumerable<OrderDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<OrderDTO>> GetPagedOrders(int pageNumber, int pageSize);
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
        Task<IEnumerable<OrderDTO>> GetByDeliveryTypeId(long deliveryTypeId);
        Task<IEnumerable<OrderDTO>> GetByDeliveryTypeName(string DeliveryTypeName);
        Task<IEnumerable<OrderDTO>> GetByDeliveryTypeDescription(string DeliveryTypeDescription);
        Task<IEnumerable<OrderDTO>> GetByDeliveryTypePriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<OrderDTO>> GetByDeliveryTypeDeliveryTimeInDaysRange(int minDeliveryTimeInDays, int maxDeliveryTimeInDays);
        Task<IEnumerable<OrderDTO>> GetByOrderItemId(long orderItemId);
        Task<IEnumerable<OrderDTO>> GetByWareId(long wareId);
        Task<IEnumerable<OrderDTO>> GetByWarePriceHistoryId(long warePriceHistoryId);
        Task<IEnumerable<OrderDTO>> GetByCustomerId(string customerId);
        Task<IEnumerable<OrderDTO>> GetByShopId(long shopId);
        Task<IEnumerable<OrderDTO>> GetByQuery(OrderQueryBLL query);
        Task<OrderDTO> Create(OrderDTO order);
        Task<OrderDTO> CreateByProcess(OrderCreationProcessDTO orderCreationProcessDTO);
        Task<OrderDTO> Update(OrderDTO order);
        Task<OrderDTO> Delete(long id);
    }
}
