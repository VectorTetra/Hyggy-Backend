using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HyggyBackend.DAL.Repositories
{
	public class ShopRepository : IShopRepository
	{
		private readonly HyggyContext _context;
        public ShopRepository(HyggyContext context)
        {
			_context = context;
        }
		public async Task<Shop?> GetById(long id)
		{
			return await _context.Shops.Where(s => s.Id == id).FirstOrDefaultAsync(); 
		}
		public async Task<Shop?> GetByAddressId(long addressId)
		{
			return await _context.Shops.Where(s => s.Address.Id == addressId).FirstOrDefaultAsync();
		}
		public async Task<Shop?> GetByOrderId(long orderId)
		{
			return await _context.Shops.Where(s => s.Orders.Any(o => o.Id == orderId)).FirstOrDefaultAsync();
		}
		public async Task<Shop?> GetByLatitudeAndLongitude(double latitude, double longitude)
		{
			return await _context.Shops.Where(s => s.Address.Latitude == latitude && s.Address.Longitude == longitude).FirstOrDefaultAsync();
		}
		public async Task<IEnumerable<Shop>> GetAll()
		{
			return await _context.Shops.ToListAsync();
		}
		public async Task<IEnumerable<Shop>> GetPaginatedShops(int? pageNumber)
		{
			const int pageSize = 10;
			var shops = await _context.Shops.ToListAsync();
			var paginatedShops = shops.Skip((pageNumber ?? 0) * pageSize)
				.Take(pageSize).ToList();

			return paginatedShops;
		}
		public async Task<IEnumerable<Shop>> GetByCity(string city)
		{
			return await _context.Shops.Where(s => s.Address.City == city).ToListAsync();
		}
		public async Task<IEnumerable<Shop>> GetByPostalCode(string postalCode)
		{
			return await _context.Shops.Where(s => s.Address.PostalCode == postalCode).ToListAsync();
		}
		public async Task<IEnumerable<Shop>> GetByState(string state)
		{
			return await _context.Shops.Where(s => s.Address.State == state).ToListAsync();
		}
		public async Task<IEnumerable<Shop>> GetByQuery(ShopQueryDAL query)
		{
			var shopCollections = new List<IEnumerable<Shop>>();

			if (query.City != null) 
				shopCollections.Add(await GetByCity(query.City));

			if(query.PostalCode != null)
				shopCollections.Add(await GetByPostalCode(query.PostalCode));

			if(query.State != null)
				shopCollections.Add(await GetByState(query.State));
			 
			return shopCollections.Aggregate((prev, next) => prev.Intersect(next));
		}
		public async Task Create(Shop shop)
		{
			await _context.Shops.AddAsync(shop);
		}
		public void Update(Shop shop)
		{
			_context.Shops.Update(shop);
		}
		public async Task Delete(long id)
		{
			var shop = await GetById(id);
			if(shop != null)
				_context.Shops.Remove(shop);
		}
	}
}
