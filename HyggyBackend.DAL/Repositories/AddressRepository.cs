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
		public void Update(Address address)
		{
			_context.Addresses.Update(address);
		}
		public async Task DeleteAsync(long id)
		{
			var address = await GetByIdAsync(id);
			if(address != null) 
				_context.Addresses.Remove(address);
		}
	}
}
