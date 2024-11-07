using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

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
        public async Task<IEnumerable<AddressDTO>> GetPaged(int pageNumber, int pageSize)
        {
            var addresses = await Database.Addresses.GetPaged(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByStringIds(string stringIds)
        {
            var addresses = await Database.Addresses.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByHouseNumber(string HouseNumber)
        {
            var addresses = await Database.Addresses.GetByHouseNumber(HouseNumber);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByCity(string City)
        {
            var addresses = await Database.Addresses.GetByCity(City);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByStreet(string Street)
        {
            var addresses = await Database.Addresses.GetByStreet(Street);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByState(string State)
        {
            var addresses = await Database.Addresses.GetByState(State);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByPostalCode(string PostalCode)
        {
            var addresses = await Database.Addresses.GetByPostalCode(PostalCode);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByLatitudeAndLongitude(double Latitude, double Longitude)
        {
            var addresses = await Database.Addresses.GetByLatitudeAndLongitude(Latitude, Longitude);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<IEnumerable<AddressDTO>> GetByQuery(AddressQueryBLL queryBLL)
        {
            var queryDAL = _mapper.Map<AddressQueryBLL, AddressQueryDAL>(queryBLL);
            var addresses = await Database.Addresses.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressDTO>>(addresses);
        }
        public async Task<AddressDTO?> GetByShopId(long ShopId)
        {
            var address = await Database.Addresses.GetByShopId(ShopId);
            return _mapper.Map<Address, AddressDTO>(address);
        }
        public async Task<AddressDTO?> GetByStorageId(long StorageId)
        {
            var address = await Database.Addresses.GetByStorageId(StorageId);
            return _mapper.Map<Address, AddressDTO>(address);
        }
        public async Task<AddressDTO?> GetByOrderId(long OrderId)
        {
            var address = await Database.Addresses.GetByOrderId(OrderId);
            return _mapper.Map<Address, AddressDTO>(address);
        }
        public async Task<AddressDTO?> GetByIdAsync(long id)
        {
            var address = await Database.Addresses.GetByIdAsync(id);
            return _mapper.Map<Address, AddressDTO>(address);
        }
        public async Task<AddressDTO?> CreateAsync(AddressDTO AddressDTO)
        {
            var checkLatitude = AddressDTO.Latitude ?? throw new ValidationException("В адресі повинна бути вказана широта!", "");
            var checkLongitude = AddressDTO.Longitude ?? throw new ValidationException("В адресі повинна бути вказана довгота!", "");
            var checkHouseNumber = AddressDTO.HouseNumber ?? throw new ValidationException("В адресі повинен бути вказаний номер будинку!", "");
            var checkCity = AddressDTO.City ?? throw new ValidationException("В адресі повинно бути вказано місто!", "");
            var checkStreet = AddressDTO.Street ?? throw new ValidationException("В адресі повинно бути вказано вулицю!", "");
            var checkState = AddressDTO.State ?? throw new ValidationException("В адресі повинно бути вказано область!", "");
            var checkPostalCode = AddressDTO.PostalCode ?? throw new ValidationException("В адресі повинно бути вказано поштовий індекс!", "");
            if (checkLatitude < -90 || checkLatitude > 90)
            {
                throw new ValidationException("Широта повинна бути в межах від -90 до 90!", "");
            }

            if (checkLongitude < -180 || checkLongitude > 180)
            {
                throw new ValidationException("Довгота повинна бути в межах від -180 до 180!", "");
            }

            // Перевірка на існування адреси за координатами
            var existedByLatLong = await GetByLatitudeAndLongitude(checkLatitude, checkLongitude);
            if (existedByLatLong.Any())
            {
                throw new ValidationException($"Адреса з такими координатами: {AddressDTO.Latitude.Value} , {AddressDTO.Longitude.Value} вже існує!", "");
            }

            var addressCollections = new List<IEnumerable<AddressDTO>>
            {
                await GetByCity(AddressDTO.City),
                await GetByState(AddressDTO.State),
                await GetByStreet(AddressDTO.Street),
                await GetByPostalCode(AddressDTO.PostalCode),
                await GetByHouseNumber(AddressDTO.HouseNumber)
            };

            var existedAddresses = addressCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
            if (existedAddresses.Any())
            {
                throw new ValidationException($"Адреса з такими параметрами вже існує!", "");
            }

            var address = new Address
            {
                HouseNumber = AddressDTO.HouseNumber,
                City = AddressDTO.City,
                State = AddressDTO.State,
                Street = AddressDTO.Street,
                PostalCode = AddressDTO.PostalCode,
                Latitude = AddressDTO.Latitude,
                Longitude = AddressDTO.Longitude,
                Orders = new List<Order>()
            };

            await Database.Addresses.CreateAsync(address);
            await Database.Save();
            return _mapper.Map<Address, AddressDTO>(address);

        }
        public async Task<AddressDTO?> Update(AddressDTO AddressDTO) 
        {
            var address = await Database.Addresses.GetByIdAsync(AddressDTO.Id);
            if (address == null)
            {
                throw new ValidationException("Адреса не знайдена!", "");
            }
            Storage checkStorage = null;
            if (AddressDTO.StorageId.HasValue)
            {
                checkStorage = await Database.Storages.GetById(AddressDTO.StorageId.Value);

                if (checkStorage == null)
                {
                    throw new ValidationException($"Складу з таким Id : {AddressDTO.StorageId.Value} не існує!", "");
                }
            }
            Shop? checkShop = null;
            if (AddressDTO.ShopId.HasValue)
            {
                checkShop = await Database.Shops.GetById(AddressDTO.ShopId.Value);

                if (checkShop == null)
                {
                    throw new ValidationException($"Магазину з таким Id : {AddressDTO.ShopId.Value} не існує!", "");
                }
            }
            var checkLatitude = AddressDTO.Latitude ?? throw new ValidationException("В адресі повинна бути вказана широта!", "");
            var checkLongitude = AddressDTO.Longitude ?? throw new ValidationException("В адресі повинна бути вказана довгота!", "");
            var checkHouseNumber = AddressDTO.HouseNumber ?? throw new ValidationException("В адресі повинен бути вказаний номер будинку!", "");
            var checkCity = AddressDTO.City ?? throw new ValidationException("В адресі повинно бути вказано місто!", "");
            var checkStreet = AddressDTO.Street ?? throw new ValidationException("В адресі повинно бути вказано вулицю!", "");
            var checkState = AddressDTO.State ?? throw new ValidationException("В адресі повинно бути вказано область!", "");
            var checkPostalCode = AddressDTO.PostalCode ?? throw new ValidationException("В адресі повинно бути вказано поштовий індекс!", "");
            if (checkLatitude < -90 || checkLatitude > 90)
            {
                throw new ValidationException("Широта повинна бути в межах від -90 до 90!", "");
            }

            if (checkLongitude < -180 || checkLongitude > 180)
            {
                throw new ValidationException("Довгота повинна бути в межах від -180 до 180!", "");
            }

            //// Перевірка на існування адреси за координатами
            //var existedByLatLong = await GetByLatitudeAndLongitude(checkLatitude, checkLongitude);
            //if (existedByLatLong.Any(x=>x.Id != AddressDTO.Id))
            //{
            //    throw new ValidationException($"Адреса з такими координатами: {AddressDTO.Latitude.Value} , {AddressDTO.Longitude.Value} вже існує!", "");
            //}

            var addressCollections = new List<IEnumerable<AddressDTO>>
            {
                await GetByCity(AddressDTO.City),
                await GetByState(AddressDTO.State),
                await GetByStreet(AddressDTO.Street),
                await GetByPostalCode(AddressDTO.PostalCode),
                await GetByHouseNumber(AddressDTO.HouseNumber)
            };

            var existedAddresses = addressCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
            if (existedAddresses.Any(x => x.Id != AddressDTO.Id))
            {
                throw new ValidationException($"Адреса з такими параметрами вже існує!", "");
            }

            address.HouseNumber = AddressDTO.HouseNumber;
            address.City = AddressDTO.City;
            address.State = AddressDTO.State;
            address.Street = AddressDTO.Street;
            address.PostalCode = AddressDTO.PostalCode;
            address.Latitude = AddressDTO.Latitude;
            address.Longitude = AddressDTO.Longitude;
            address.Shop = checkShop;
            address.Storage = checkStorage;
            address.Orders.Clear();

            if (AddressDTO.OrderIds != null)
            {
                if (!AddressDTO.OrderIds.Any())
                {
                    address.Orders.Clear();  // Очищаємо колекцію, якщо масив порожній
                }
                else
                {
                    address.Orders.Clear();  // Очищаємо старі замовлення
                    await foreach (var order in Database.Orders.GetByIdsAsync(AddressDTO.OrderIds))
                    {
                        if (order == null)
                        {
                            throw new ValidationException($"Одне з замовлень не знайдено!", "");
                        }
                        address.Orders.Add(order);  // Додаємо нові замовлення
                    }
                }
            }

            Database.Addresses.Update(address);
            await Database.Save();
            return _mapper.Map<Address, AddressDTO>(address);
        }
        public async Task<AddressDTO?> DeleteAsync(long id) 
        {
            var address = await Database.Addresses.GetByIdAsync(id);
            if (address == null)
            {
                throw new ValidationException("Адреса не знайдена!", "");
            }
            await Database.Addresses.DeleteAsync(id);
            await Database.Save();
            return _mapper.Map<Address, AddressDTO>(address);
        }
    }
}
