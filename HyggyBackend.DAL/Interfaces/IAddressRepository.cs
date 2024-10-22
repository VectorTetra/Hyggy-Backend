using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IAddressRepository
	{
		Task<IEnumerable<Address>> GetPaged(int pageNumber, int pageSize);
		Task<IEnumerable<Address>> GetByStringIds(string stringIds);
		Task<IEnumerable<Address>> GetByHouseNumber(string HouseNumber);
        Task<IEnumerable<Address>> GetByCity(string City);
        Task<IEnumerable<Address>> GetByState(string State);
        Task<IEnumerable<Address>> GetByStreet(string Street);
        Task<IEnumerable<Address>> GetByPostalCode(string PostalCode);
        Task<IEnumerable<Address>> GetByLatitudeAndLongitude(double Latitude, double Longitude);
        Task<IEnumerable<Address>> GetByQuery(AddressQueryDAL queryDAL);
        Task<Address?> GetByShopId(long ShopId);
        Task<Address?> GetByStorageId(long StorageId);
        Task<Address?> GetByOrderId(long OrderId);
        IAsyncEnumerable<Address> GetByIdsAsync(IEnumerable<long> ids);
        Task<Address?> GetByIdAsync(long id);
        Task CreateAsync(Address address);
		void Update(Address address);
		Task DeleteAsync(long id);
	}
}
