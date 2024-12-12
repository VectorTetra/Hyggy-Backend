using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IOrderDeliveryTypeService
    {
        Task<OrderDeliveryTypeDTO?> GetById(long id);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByOrderId(long orderId);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByName(string nameSubstring);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByDescription(string descriptionSubstring);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByDeliveryTimeInDaysRange(int minDeliveryTimeInDays, int maxDeliveryTimeInDays);
        Task<IEnumerable<OrderDeliveryTypeDTO>> GetByQuery(OrderDeliveryTypeQueryBLL query);
        Task<OrderDeliveryTypeDTO> Create(OrderDeliveryTypeDTO orderDeliveryType);
        Task<OrderDeliveryTypeDTO> Update(OrderDeliveryTypeDTO orderDeliveryType);
        Task<OrderDeliveryTypeDTO> Delete(long id);
    }
}
