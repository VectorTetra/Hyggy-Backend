using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Storage>> GetAll();
        // Отримати всі центральні склади, не пов'язані з магазинами
        Task<IEnumerable<Storage>> GetGlobalStorages();
        // Отримати всі склади, які пов'язані з магазинами
        Task<IEnumerable<Storage>> GetNonGlobalStorages();
        Task<IEnumerable<Storage>> GetByQuery(StorageQueryDAL query);
        Task<IEnumerable<Storage>> GetByStringIds(string stringIds);
        Task<IEnumerable<Storage>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<Storage>> GetByCity(string city);
        Task<IEnumerable<Storage>> GetByShopName(string shopName);
        Task<IEnumerable<Storage>> GetByStreet(string Street);
        Task<IEnumerable<Storage>> GetByPostalCode(string postalCode);
        Task<IEnumerable<Storage>> GetByState(string state);
        Task<IEnumerable<Storage>> GetByHouseNumber(string HouseNumber);
        Task<Storage?> GetById(long id);
        Task<Storage?> GetByAddressId(long addressId);
        Task<Storage?> GetByShopId(long ShopId);
        Task<Storage?> GetByWareItemId(long WareItemId);
        Task<Storage?> GetByStorageEmployeeId(string storageEmployeeId);
        Task<Storage?> GetByShopEmployeeId(string shopEmployeeId);
        IAsyncEnumerable<Storage> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(Storage storage);
        void Update(Storage storage);
        Task Delete(long id);
    }
}
