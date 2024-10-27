

using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

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
		
		public async Task<IEnumerable<ShopDTO>> GetAll()
		{
			var shops = await Database.Shops.GetAll();

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shops);
		}
        public async Task<IEnumerable<ShopDTO>> GetPaginatedShops(int pageNumber, int pageSize)
        {
            var shops = await Database.Shops.GetPaginatedShops(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shops);
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

        public async Task<ShopDTO?> GetByStorageId(long storageId)
        {
            var shop = await Database.Shops.GetByStorageId(storageId);
            return _mapper.Map<ShopDTO>(shop);
        }

        public async Task<IEnumerable<ShopDTO>> GetByHouseNumber(string houseNumber)
        {
            var shop = await Database.Shops.GetByHouseNumber(houseNumber);
            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
        }

        public async Task<IEnumerable<ShopDTO>> GetByStreet(string street)
        {
            var shop = await Database.Shops.GetByStreet(street);
            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
        }
        public async Task<IEnumerable<ShopDTO>> GetByPostalCode(string postalCode)
		{
			var shop = await Database.Shops.GetByPostalCode(postalCode);

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
		}

        public async Task<IEnumerable<ShopDTO>> GetByName(string name)
        {
            var shop = await Database.Shops.GetByName(name);

            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
        }

        public async Task<IEnumerable<ShopDTO>> GetByQuery(ShopQueryBLL query)
		{
            var queryDAL = _mapper.Map<ShopQueryDAL>(query);
            var shop = await Database.Shops.GetByQuery(queryDAL);

            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
        }

		public async Task<IEnumerable<ShopDTO>> GetByState(string state)
		{
			var shop = await Database.Shops.GetByState(state);

			return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
		}
        public async Task<IEnumerable<ShopDTO>> GetNearestShopsAsync(double latitude, double longitude, int numberOfShops = 5)
        {
            var shops = await Database.Shops.GetNearestShopsAsync(latitude, longitude, numberOfShops);
            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shops);
        }

        public async Task<IEnumerable<ShopDTO>> GetByStringIds(string stringIds)
        {
            var shop = await Database.Shops.GetByStringIds(stringIds);

            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDTO>>(shop);
        }
        public async Task<ShopDTO> Create(ShopDTO shopDTO)
		{
			if(shopDTO.AddressId == null)
			{
				throw new ValidationException("Не вказано ідентифікатор адреси для створення магазину!", "");
			}
			var existingShopByAddress = await Database.Shops.GetByAddressId(shopDTO.AddressId.Value);
            if (existingShopByAddress != null)
            {
                throw new ValidationException($"Магазин з Id адреси {shopDTO.AddressId} вже існує!", "");
            }
			var existingAddress = await Database.Addresses.GetByIdAsync(shopDTO.AddressId.Value);
            if (existingAddress == null)
            {
                throw new ValidationException($"Адреса з Id {shopDTO.AddressId} не існує!", "");
            }
            if(shopDTO.Name == null)
            {
                throw new ValidationException("Не вказано назву магазину!", "");
            }
			if(shopDTO.WorkHours == null)
			{
                throw new ValidationException("Не вказано години роботи магазину!", "");
            }
            if (shopDTO.PhotoUrl == null)
            {
                throw new ValidationException("Не вказано фото магазину!", "");
            }
			if(shopDTO.StorageId == null)
			{
				throw new ValidationException("Не вказано ідентифікатор складу для створення магазину!", "");
            }
			var existingStorage = await Database.Storages.GetById(shopDTO.StorageId.Value);
            if (existingStorage == null)
            {
                throw new ValidationException($"Склад з Id {shopDTO.StorageId} не існує!", "");
            }
            var shop = new Shop
			{
                Address = existingAddress,
                Name = shopDTO.Name,
                Orders = new List<Order>(),
                WorkHours = shopDTO.WorkHours,
				PhotoUrl = shopDTO.PhotoUrl,
                Storage = existingStorage,
				ShopEmployees = new List<ShopEmployee>()
            };

            await Database.Shops.Create(shop);
			await Database.Save();

            return _mapper.Map<ShopDTO>(shop);
        }
		public async Task<ShopDTO> Update(ShopDTO shopDTO)
		{
			var existingShop = await Database.Shops.GetById(shopDTO.Id);
			if(existingShop == null)
			{
                throw new ValidationException($"Магазин з id={shopDTO.Id} не знайдено!", "");
            }
            if (shopDTO.AddressId == null)
            {
                throw new ValidationException("Не вказано ідентифікатор адреси для створення магазину!", "");
            }
            var existingShopByAddress = await Database.Shops.GetByAddressId(shopDTO.AddressId.Value);
            if (existingShopByAddress != null && existingShopByAddress.Id != shopDTO.Id)
            {
                throw new ValidationException($"Магазин з Id адреси {shopDTO.AddressId} вже існує!", "");
            }
            var existingAddress = await Database.Addresses.GetByIdAsync(shopDTO.AddressId.Value);
            if (existingAddress == null)
            {
                throw new ValidationException($"Адреса з Id {shopDTO.AddressId} не існує!", "");
            }
            if (shopDTO.WorkHours == null)
            {
                throw new ValidationException("Не вказано години роботи магазину!", "");
            }
            if (shopDTO.Name == null)
            {
                throw new ValidationException("Не вказано назву магазину!", "");
            }
            if (shopDTO.PhotoUrl == null)
            {
                throw new ValidationException("Не вказано фото магазину!", "");
            }
            if (shopDTO.StorageId == null)
            {
                throw new ValidationException("Не вказано ідентифікатор складу для створення магазину!", "");
            }
            var existingStorage = await Database.Storages.GetById(shopDTO.StorageId.Value);
            if (existingStorage == null)
            {
                throw new ValidationException($"Склад з Id {shopDTO.StorageId} не існує!", "");
            }
			existingShop.Orders.Clear();
            existingShop.ShopEmployees.Clear();
            await foreach (var order in Database.Orders.GetByIdsAsync(shopDTO.OrderIds))
            {
                if (order == null)
                {
                    throw new ValidationException("Один з Order не знайдено!", "");
                }
                existingShop.Orders.Add(order);
            }
            await foreach (var order in Database.ShopEmployees.GetByIdsAsync(shopDTO.ShopEmployeeIds))
            {
                if (order == null)
                {
                    throw new ValidationException("Один з Order не знайдено!", "");
                }
                existingShop.ShopEmployees.Add(order);
            }
            existingShop.Address = existingAddress;
            existingShop.WorkHours = shopDTO.WorkHours;
            existingShop.Name = shopDTO.Name;
            existingShop.PhotoUrl = shopDTO.PhotoUrl;
            existingShop.Storage = existingStorage;

            Database.Shops.Update(existingShop);
            await Database.Save();

            return _mapper.Map<ShopDTO>(existingShop);

        }
		public async Task<ShopDTO> Delete(long id)
		{
            var shop = await Database.Shops.GetById(id);
            if (shop == null)
            {
                throw new ValidationException($"Магазин з id={id} не знайдено!", "");
            }
            await Database.Shops.Delete(id);
            await Database.Save();
            return _mapper.Map<ShopDTO>(shop);
        }
	}
}
