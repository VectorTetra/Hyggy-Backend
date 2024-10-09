using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
	public class AddressRepository : IAddressRepository
	{
		private readonly HyggyContext _context;

		public AddressRepository(HyggyContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Address>> GetAllAsync()
		{
			return await _context.Addresses.ToListAsync();
		}
		public async Task<Address?> GetByIdAsync(long id)
		{
			return await _context.Addresses.Where(a => a.Id == id).FirstOrDefaultAsync();
		}
		public async Task CreateAsync(Address address)
		{
			await _context.Addresses.AddAsync(address);
		}

        public async IAsyncEnumerable<Address> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var address = await GetByIdAsync(id);  // Виклик методу репозиторію
                if (address != null)
                {
                    yield return address;
                }
            }
        }
        public void Update(Address updatedAddress)
		{
			Address? address = _context.Addresses.Where(a => a.Id == updatedAddress.Id).FirstOrDefault();
			address.Street = updatedAddress.Street;
			address.City = updatedAddress.City;
			address.State = updatedAddress.State;
			address.PostalCode = updatedAddress.PostalCode;
			address.HouseNumber = updatedAddress.HouseNumber;
			address.Latitude = updatedAddress.Latitude;
			address.Longitude = updatedAddress.Longitude;
		}
		public async Task DeleteAsync(long id)
		{
			var address = await GetByIdAsync(id);
			if(address != null) 
				_context.Addresses.Remove(address);
		}
	}
}
