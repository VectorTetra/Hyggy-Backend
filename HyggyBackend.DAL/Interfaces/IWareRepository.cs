using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareRepository
    {
        Task<Ware?> GetById(long id);
        Task<Ware?> GetByArticle(long article);
        Task<IEnumerable<Ware>> GetPagedWares(int pageNumber, int pageSize);
        Task<IEnumerable<Ware>> GetByStringIds(string stringIds);
        Task<IEnumerable<Ware>> GetByStringTrademarkIds(string stringTrademarkIds);
        Task<IEnumerable<Ware>> GetByStringStatusIds(string stringStatusIds);
        Task<IEnumerable<Ware>> GetByStringCategory1Ids(string stringCategory1Ids);
        Task<IEnumerable<Ware>> GetByStringCategory2Ids(string stringCategory2Ids);
        Task<IEnumerable<Ware>> GetByStringCategory3Ids(string stringCategory3Ids);
        Task<IEnumerable<Ware>> GetByCategory1Id(long category1Id);
        Task<IEnumerable<Ware>> GetByCategory2Id(long category2Id);
        Task<IEnumerable<Ware>> GetByCategory3Id(long category3Id);
        Task<IEnumerable<Ware>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<Ware>> GetByStructureFilePathSubstring(string StructureFilePathSubstring);
        Task<IEnumerable<Ware>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<Ware>> GetByCategory1NameSubstring(string category1NameSubstring);
        Task<IEnumerable<Ware>> GetByCategory2NameSubstring(string category2NameSubstring);
        Task<IEnumerable<Ware>> GetByCategory3NameSubstring(string category3NameSubstring);
        Task<IEnumerable<Ware>> GetByTrademarkId(long trademarkId);
        Task<IEnumerable<Ware>> GetByTrademarkNameSubstring(string trademarkNameSubstring);
        Task<IEnumerable<Ware>> GetByPriceRange(float minPrice, float maxPrice);
        Task<IEnumerable<Ware>> GetByDiscountRange(float minDiscount, float maxDiscount);
        Task<IEnumerable<Ware>> GetByIsDeliveryAvailable(bool isDeliveryAvailable);
        Task<IEnumerable<Ware>> GetByStatusId(long statusId);
        Task<IEnumerable<Ware>> GetByStatusNameSubstring(string statusNameSubstring);
        Task<IEnumerable<Ware>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring);
        Task<IEnumerable<Ware>> GetByImagePathSubstring(string imagePathSubstring);
        Task<IEnumerable<Ware>> GetFavoritesByCustomerId(string customerId);
        IAsyncEnumerable<Ware> GetByIdsAsync(IEnumerable<long> ids);
        Task<IEnumerable<Ware>> GetByQuery(WareQueryDAL queryDAL);
        Task Create(Ware ware);
        void Update(Ware ware);
        Task Delete(long id);

        //Task<IEnumerable<Ware>> Get200Last();
        //Task<IEnumerable<Ware>> GetByLabelSubstring(string labelSubstring);
        //Task<IEnumerable<Ware>> GetByDescriptionSubstring(string descriptionSubstring);
    }
}
