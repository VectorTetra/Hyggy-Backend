using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;


namespace HyggyBackend.BLL.Interfaces
{
	public interface IShopService
	{
        Task<ShopDTO?> GetById(long id);
        Task<ShopDTO?> GetByAddressId(long addressId);
        Task<ShopDTO?> GetByOrderId(long orderId);
        Task<ShopDTO?> GetByStorageId(long storageId);
        Task<ShopDTO?> GetByLatitudeAndLongitude(double latitude, double longitude);
        Task<IEnumerable<ShopDTO>> GetAll();
        Task<IEnumerable<ShopDTO>> GetPaginatedShops(int pageNumber, int pageSize);
        Task<IEnumerable<ShopDTO>> GetByCity(string city);
        Task<IEnumerable<ShopDTO>> GetByName(string name);
        Task<IEnumerable<ShopDTO>> GetByPostalCode(string postalCode);
        Task<IEnumerable<ShopDTO>> GetByState(string state);
        Task<IEnumerable<ShopDTO>> GetByStreet(string street);
        Task<IEnumerable<ShopDTO>> GetByHouseNumber(string houseNumber);
        Task<IEnumerable<ShopDTO>> GetByQuery(ShopQueryBLL query);
        Task<IEnumerable<ShopDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<ShopDTO>> GetNearestShopsAsync(double latitude, double longitude, int numberOfShops = 5);
        Task<ShopDTO> Create(ShopDTO shopDTO);
        Task<ShopDTO> Update(ShopDTO shopDTO);
		Task<ShopDTO> Delete(long id);
	}
}
