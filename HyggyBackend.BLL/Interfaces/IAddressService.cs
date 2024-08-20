

using HyggyBackend.BLL.DTO;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IAddressService
	{
		Task<IEnumerable<AddressDTO>> GetAllAsync();
		Task<AddressDTO?> GetByIdAsync(long id);
		Task<bool> IsAddressExist(long id);

		Task<bool> CreateAsync(AddressDTO address);
		Task<bool> Update(AddressDTO address);
		Task<bool> DeleteAsync(long id);
	}
}
