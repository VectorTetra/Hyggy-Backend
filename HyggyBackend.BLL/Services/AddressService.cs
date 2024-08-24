using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;

namespace HyggyBackend.BLL.Services
{
	public class AddressService : IAddressService
	{
		IUnitOfWork Database { get; set; }
		private IMapper _mapper;
		public AddressService(IUnitOfWork database, IMapper mapper)
		{
			Database = database;
			_mapper = mapper;
		}

		public async Task<IEnumerable<AddressDTO>> GetAllAsync()
		{
			var addresses = await Database.Addresses.GetAllAsync();
			return _mapper.Map<IEnumerable<AddressDTO>>(addresses);
		}
		public async Task<AddressDTO?> GetByIdAsync(long id)
		{
			var address = await Database.Addresses.GetByIdAsync(id);
			return _mapper.Map<AddressDTO>(address);
		}
		public async Task<AddressDTO> CreateAsync(AddressDTO addressDto)
		{
			var address = _mapper.Map<Address>(addressDto);
			await Database.Addresses.CreateAsync(address);
			await Database.Save();

			return addressDto;
		}
		public void Update(AddressDTO addressDto)
		{
			var address = _mapper.Map<Address>(addressDto);

			Database.Addresses.Update(address);
            Database.Save();

		}
		public async Task DeleteAsync(long id)
		{
			await Database.Addresses.DeleteAsync(id);
			await Database.Save();

		}

		public async Task<bool> IsAddressExist(long id)
		{
			var addresses = await Database.Addresses.GetAllAsync();

			return addresses.Any(a => a.Id == id);
		}
	}
}
