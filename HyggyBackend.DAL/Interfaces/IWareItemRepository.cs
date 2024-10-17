using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareItemRepository
    {
        Task<WareItem?> GetById(long id);
        Task<IEnumerable<WareItem>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareItem>> GetByArticle(long article);
        Task<IEnumerable<WareItem>> GetByWareId(long wareId);
        Task<IEnumerable<WareItem>> GetByWareName(string wareName);
        Task<IEnumerable<WareItem>> GetByWareDescription(string wareDescription);
        Task<IEnumerable<WareItem>> GetByWarePriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<WareItem>> GetByWareDiscountRange(float minDiscount, float maxDiscount);
        Task<IEnumerable<WareItem>> GetByWareStatusId(long statusId); 
        Task<IEnumerable<WareItem>> GetByWareCategory3Id(long wareCategory3Id); 
        Task<IEnumerable<WareItem>> GetByWareCategory2Id(long wareCategory2Id); 
        Task<IEnumerable<WareItem>> GetByWareCategory1Id(long wareCategory1Id);
        Task<IEnumerable<WareItem>> GetByWareImageId(long wareImageId);
        Task<IEnumerable<WareItem>> GetByPriceHistoryId(long priceHistoryId);
        Task<IEnumerable<WareItem>> GetByOrderItemId(long orderItemId);
        Task<IEnumerable<WareItem>> GetByIsDeliveryAvailable(bool isDeliveryAvailable);
        Task<IEnumerable<WareItem>> GetByStorageId(long storageId);
        Task<IEnumerable<WareItem>> GetByShopId(long shopId);
        Task<IEnumerable<WareItem>> GetByQuantityRange(long minQuantity, long maxQuantity);
        Task<IEnumerable<WareItem>> GetPagedWareItems(int pageNumber, int pageSize);
        Task<IEnumerable<WareItem>> GetByQuery(WareItemQueryDAL query);

        IAsyncEnumerable<WareItem> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WareItem wareItem);
        void Update(WareItem wareItem);
        Task Delete(long id);
    }
}
