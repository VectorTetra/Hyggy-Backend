

using HyggyBackend.BLL.DTO;

namespace HyggyBackend.BLL.Interfaces
{
	public interface IAddressService
	{
		Task<IEnumerable<AddressDTO>> GetAllAsync();
		Task<AddressDTO?> GetByIdAsync(long id);
		Task<bool> IsAddressExist(long id);

		Task<AddressDTO> CreateAsync(AddressDTO address);
		void Update(AddressDTO address);
		Task DeleteAsync(long id);
	}
}
