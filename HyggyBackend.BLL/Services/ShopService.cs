

using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;

namespace HyggyBackend.BLL.Services
{
	public class ShopService : IShopService
	{
		IUnitOfWork Database { get; set; }
		private readonly IMapper _mapper;

		public ShopService(IUnitOfWork database, IMapper mapper)
		{
			Database = database;
			_mapper = mapper;
		}
		public Task<IEnumerable<ShopDTO>> GetAll()
		{
			throw new NotImplementedException();
		}
		public async Task<IEnumerable<ShopDTO>> GetPaginatedShops(int? page)
		{
			var paginatedShops = await Database.Shops.GetPaginatedShops(page);

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(paginatedShops);
		}
		public async Task<ShopDTO?> GetByAddressId(long addressId)
		{
			var shop = await Database.Shops.GetByAddressId(addressId);

			return _mapper.Map<ShopDTO>(shop);
		}

		public async Task<IEnumerable<ShopDTO>> GetByCity(string city)
		{
			var shop = await Database.Shops.GetByCity(city);

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
		}

		public async Task<ShopDTO?> GetById(long id)
		{
			var shop = await Database.Shops.GetById(id);

			return _mapper.Map<ShopDTO>(shop);
		}

		public async Task<ShopDTO?> GetByLatitudeAndLongitude(double latitude, double longitude)
		{
			var shop = await Database.Shops.GetByLatitudeAndLongitude(latitude, longitude);

			return _mapper.Map<ShopDTO>(shop);
		}

		public async Task<ShopDTO?> GetByOrderId(long orderId)
		{
			var shop = await Database.Shops.GetByOrderId(orderId);

			return _mapper.Map<ShopDTO>(shop);
		}

		public async Task<IEnumerable<ShopDTO>> GetByPostalCode(string postalCode)
		{
			var shop = await Database.Shops.GetByPostalCode(postalCode);

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
		}

		public Task<IEnumerable<ShopDTO>> GetByQuery(ShopQueryBLL query)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ShopDTO>> GetByState(string state)
		{
			var shop = await Database.Shops.GetByState(state);

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
		}
		public async Task<bool> Create(ShopDTO shopDTO)
		{
			var shop = _mapper.Map<Shop>(shopDTO);
			if(shop == null)
				return false;

			await Database.Shops.Create(shop);
			if(! await Database.Save()) 
				return false;

			return true;
		}
		public void Update(ShopDTO shopDTO)
		{
			var shop = _mapper.Map<Shop>(shopDTO);

			Database.Shops.Update(shop);
		}
		public async Task Delete(long id)
		{
			await Database.Shops.Delete(id);
		}

		public async Task<bool> IsShopExist(long id)
		{
			var shops = await Database.Shops.GetAll();
			
			return shops.Any(shop => shop.Id == id);
		}
	}
}
