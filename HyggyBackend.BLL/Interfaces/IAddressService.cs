

using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IAddressService
	{
        Task<IEnumerable<AddressDTO>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<AddressDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<AddressDTO>> GetByHouseNumber(string HouseNumber);
        Task<IEnumerable<AddressDTO>> GetByCity(string City);
        Task<IEnumerable<AddressDTO>> GetByState(string State);
        Task<IEnumerable<AddressDTO>> GetByStreet(string Street);
        Task<IEnumerable<AddressDTO>> GetByPostalCode(string PostalCode);
        Task<IEnumerable<AddressDTO>> GetByLatitudeAndLongitude(double Latitude, double Longitude);
        Task<IEnumerable<AddressDTO>> GetByQuery(AddressQueryBLL queryBLL);
        Task<AddressDTO?> GetByShopId(long ShopId);
        Task<AddressDTO?> GetByStorageId(long StorageId);
        Task<AddressDTO?> GetByOrderId(long OrderId);
        Task<AddressDTO?> GetByIdAsync(long id);
        Task<AddressDTO?> CreateAsync(AddressDTO AddressDTO);
        Task<AddressDTO?> Update(AddressDTO AddressDTO);
        Task<AddressDTO?> DeleteAsync(long id);
    }
}
