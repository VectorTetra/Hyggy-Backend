using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareService
    {
        Task<WareDTO?> GetById(long id);
        Task<WareDTO?> GetByArticle(long article);
        Task<IEnumerable<WareDTO>> GetPagedWares(int pageNumber, int pageSize);
        Task<IEnumerable<WareDTO>> GetByCategory1Id(long category1Id);
        Task<IEnumerable<WareDTO>> GetByCategory2Id(long category2Id);
        Task<IEnumerable<WareDTO>> GetByCategory3Id(long category3Id);
        Task<IEnumerable<WareDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<WareDTO>> GetByCategory1NameSubstring(string category1NameSubstring);
        Task<IEnumerable<WareDTO>> GetByCategory2NameSubstring(string category2NameSubstring);
        Task<IEnumerable<WareDTO>> GetByCategory3NameSubstring(string category3NameSubstring);
        Task<IEnumerable<WareDTO>> GetByTrademarkId(long trademarkId);
        Task<IEnumerable<WareDTO>> GetByTrademarkNameSubstring(string trademarkNameSubstring);
        Task<IEnumerable<WareDTO>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<WareDTO>> GetByDiscountRange(float minDiscount, float maxDiscount);
        Task<IEnumerable<WareDTO>> GetByIsDeliveryAvailable(bool isDeliveryAvailable);
        Task<IEnumerable<WareDTO>> GetByStatusId(long statusId);
        Task<IEnumerable<WareDTO>> GetByStatusNameSubstring(string statusNameSubstring);
        Task<IEnumerable<WareDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring);
        Task<IEnumerable<WareDTO>> GetByImagePathSubstring(string imagePathSubstring);
        Task<IEnumerable<WareDTO>> GetFavoritesByCustomerId(string customerId);
        Task<IEnumerable<WareDTO>> GetByQuery(WareQueryBLL queryDAL);
        Task<WareDTO> Create(WareDTO ware);
        Task<WareDTO> Update(WareDTO ware);
        Task<WareDTO> Delete(long id);
    }
}
