using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
	public interface IShopRepository
	{
		Task<Shop?> GetById(long id);
		Task<Shop?> GetByAddressId(long addressId);
		Task<Shop?> GetByOrderId(long orderId);
		Task<Shop?> GetByLatitudeAndLongitude(double latitude, double longitude);

		Task<IEnumerable<Shop>> GetAll();
		Task<IEnumerable<Shop>> GetByCity(string city); 
		Task<IEnumerable<Shop>> GetByPostalCode(string postalCode);
		Task<IEnumerable<Shop>> GetByState(string state);
		Task<IEnumerable<Shop>> GetByQuery(ShopQueryDAL query);

		Task Create(Shop shop);
		void Update(Shop shop);
		Task Delete(long id);
	}
}
