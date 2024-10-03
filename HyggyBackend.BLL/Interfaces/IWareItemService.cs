using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareItemService
    {
        Task<WareItemDTO?> GetById(long id);
        Task<IEnumerable<WareItemDTO>> GetByArticle(long article);
        Task<IEnumerable<WareItemDTO>> GetByWareId(long wareId);
        Task<IEnumerable<WareItemDTO>> GetByWareName(string wareName);
        Task<IEnumerable<WareItemDTO>> GetByWareDescription(string wareDescription);
        Task<IEnumerable<WareItemDTO>> GetByWarePriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<WareItemDTO>> GetByWareDiscountRange(float minDiscount, float maxDiscount);
        Task<IEnumerable<WareItemDTO>> GetByWareStatusId(long statusId);
        Task<IEnumerable<WareItemDTO>> GetByWareCategory3Id(long wareCategory3Id);
        Task<IEnumerable<WareItemDTO>> GetByWareCategory2Id(long wareCategory2Id);
        Task<IEnumerable<WareItemDTO>> GetByWareCategory1Id(long wareCategory1Id);
        Task<IEnumerable<WareItemDTO>> GetByWareImageId(long wareImageId);
        Task<IEnumerable<WareItemDTO>> GetByPriceHistoryId(long priceHistoryId);
        Task<IEnumerable<WareItemDTO>> GetByOrderItemId(long orderItemId);
        Task<IEnumerable<WareItemDTO>> GetByIsDeliveryAvailable(bool isDeliveryAvailable);
        Task<IEnumerable<WareItemDTO>> GetByStorageId(long storageId);
        Task<IEnumerable<WareItemDTO>> GetByShopId(long shopId);
        Task<IEnumerable<WareItemDTO>> GetByQuantityRange(long minQuantity, long maxQuantity);
        Task<IEnumerable<WareItemDTO>> GetPagedWareItems(int pageNumber, int pageSize);
        Task<IEnumerable<WareItemDTO>> GetByQuery(WareItemQueryBLL query);
        Task<WareItemDTO> Create(WareItemDTO WareItemDTO);
        Task<WareItemDTO> Update(WareItemDTO WareItemDTO);
        Task<WareItemDTO> Delete(long id);
    }
}
