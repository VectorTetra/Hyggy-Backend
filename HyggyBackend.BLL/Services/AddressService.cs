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
		public async Task<bool> CreateAsync(AddressDTO addressDto)
		{
			var address = _mapper.Map<Address>(addressDto);
			if (address == null)
				return false;

			await Database.Addresses.CreateAsync(address);
			if(!await Database.Save())
				return false;

			return true;
		}
		public async Task<bool> Update(AddressDTO addressDto)
		{
			var address = _mapper.Map<Address>(addressDto);

			Database.Addresses.Update(address);
			if(! await Database.Save()) 
				return false;

			return true;
		}
		public async Task<bool> DeleteAsync(long id)
		{
			await Database.Addresses.DeleteAsync(id);

			if(!await Database.Save() ) 
				return false;

			return true;
		}

		public async Task<bool> IsAddressExist(long id)
		{
			var addresses = await Database.Addresses.GetAllAsync();

			return addresses.Any(a => a.Id == id);
		}
	}
}
