using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IOrderDeliveryTypeRepository
    {
        Task<OrderDeliveryType?> GetById(long id);
        Task<IEnumerable<OrderDeliveryType>> GetByStringIds(string stringIds);
        Task<IEnumerable<OrderDeliveryType>> GetByOrderId(long orderId);
        Task<IEnumerable<OrderDeliveryType>> GetByName(string nameSubstring);
        Task<IEnumerable<OrderDeliveryType>> GetByDescription(string descriptionSubstring);
        Task<IEnumerable<OrderDeliveryType>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<OrderDeliveryType>> GetByDeliveryTimeInDaysRange(int minDeliveryTimeInDays, int maxDeliveryTimeInDays);
        Task<IEnumerable<OrderDeliveryType>> GetByQuery(OrderDeliveryTypeQueryDAL query);
        IAsyncEnumerable<OrderDeliveryType> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(OrderDeliveryType orderDeliveryType);
        void Update(OrderDeliveryType orderDeliveryType);
        Task Delete(long id);
    }
}
