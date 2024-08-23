using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;


namespace HyggyBackend.BLL.Interfaces
{
	public interface IShopService
	{
		Task<ShopDTO?> GetById(long id);
		Task<ShopDTO?> GetByAddressId(long addressId);
		Task<ShopDTO?> GetByOrderId(long orderId);
		Task<ShopDTO?> GetByLatitudeAndLongitude(double latitude, double longitude);

		Task<IEnumerable<ShopDTO>> GetAll();
		Task<IEnumerable<ShopDTO>> GetPaginatedShops(int? page);
		Task<IEnumerable<ShopDTO>> GetByCity(string city);
		Task<IEnumerable<ShopDTO>> GetByPostalCode(string postalCode);
		Task<IEnumerable<ShopDTO>> GetByState(string state);
		Task<IEnumerable<ShopDTO>> GetByQuery(ShopQueryBLL query);

		Task<bool> IsShopExist(long id);

		Task<bool> Create(ShopDTO shopDTO);
		Task<bool> Update(ShopDTO shopDTO);
		Task<bool> Delete(long id);
	}
}
