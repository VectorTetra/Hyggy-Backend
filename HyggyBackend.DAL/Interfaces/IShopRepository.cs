using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IShopRepository
	{
		Task<Shop?> GetById(long id);
		Task<Shop?> GetByAddressId(long addressId);
		Task<Shop?> GetByOrderId(long orderId);
		Task<Shop?> GetByStorageId(long storageId);
		Task<Shop?> GetByLatitudeAndLongitude(double latitude, double longitude);
        IAsyncEnumerable<Shop> GetByIdsAsync(IEnumerable<long> ids);
        Task<IEnumerable<Shop>> GetAll();
		Task<IEnumerable<Shop>> GetPaginatedShops(int pageNumber, int pageSize);
		Task<IEnumerable<Shop>> GetByCity(string city); 
		Task<IEnumerable<Shop>> GetByName(string name); 
		Task<IEnumerable<Shop>> GetByPostalCode(string postalCode);
		Task<IEnumerable<Shop>> GetByState(string state);
		Task<IEnumerable<Shop>> GetByStreet(string street);
		Task<IEnumerable<Shop>> GetByHouseNumber(string houseNumber);
		Task<IEnumerable<Shop>> GetByQuery(ShopQueryDAL query);
		Task<IEnumerable<Shop>> GetByStringIds(string stringIds);
        Task<IEnumerable<Shop>> GetNearestShopsAsync(double latitude, double longitude, int numberOfShops = 5);
        Task Create(Shop shop);
		void Update(Shop shop);
		Task Delete(long id);
	}
}
