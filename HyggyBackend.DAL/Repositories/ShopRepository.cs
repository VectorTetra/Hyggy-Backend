using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;

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
        public async Task<Shop?> GetByStorageId(long storageId)
        {
            return await _context.Shops.Where(s => s.Storage.Id == storageId).FirstOrDefaultAsync();
        }
        public async Task<Shop?> GetByLatitudeAndLongitude(double latitude, double longitude)
        {
            return await _context.Shops.Where(s => s.Address.Latitude == latitude && s.Address.Longitude == longitude).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Shop>> GetAll()
        {
            return await _context.Shops.ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetPaginatedShops(int pageNumber, int pageSize)
        {

            var shops = await _context.Shops.ToListAsync();
            var paginatedShops = shops.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return paginatedShops;
        }
        public async Task<IEnumerable<Shop>> GetByCity(string city)
        {
            return await _context.Shops.Where(s => s.Address.City != null && s.Address.City.Contains(city)).ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetByStreet(string Street)
        {
            return await _context.Shops.Where(s => s.Address.Street != null && s.Address.Street.Contains(Street)).ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetByName(string name)
        {
            return await _context.Shops.Where(s => s.Name != null && s.Name.Contains(name)).ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetByPostalCode(string postalCode)
        {
            return await _context.Shops.Where(s => s.Address.PostalCode == postalCode).ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetByState(string state)
        {
            return await _context.Shops.Where(s => s.Address.State != null && s.Address.State.Contains(state)).ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetByHouseNumber(string HouseNumber)
        {
            return await _context.Shops.Where(s => s.Address.HouseNumber != null && s.Address.HouseNumber.Contains(HouseNumber)).ToListAsync();
        }
        public async Task<IEnumerable<Shop>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var Shopes = new List<Shop>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var Shop in GetByIdsAsync(ids))
            {
                Shopes.Add(Shop);
            }
            return Shopes;
        }
        public async Task<IEnumerable<Shop>> GetByQuery(ShopQueryDAL query)
        {
            var collections = new List<IEnumerable<Shop>>();

            // Якщо вказано QueryAny, виконуємо запити за різними критеріями
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<Shop> { await GetById(id) });
                }
                collections.Add(await GetByCity(query.QueryAny));
                collections.Add(await GetByPostalCode(query.QueryAny));
                collections.Add(await GetByState(query.QueryAny));
                collections.Add(await GetByStreet(query.QueryAny));
                collections.Add(await GetByHouseNumber(query.QueryAny));
                collections.Add(await GetByName(query.QueryAny));
            }
            else
            {
                if (query.Id != null)
                {
                    var res = await GetById(query.Id.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Shop> { res });
                    }
                }
                if (query.City != null)
                    collections.Add(await GetByCity(query.City));
                if (query.PostalCode != null)
                    collections.Add(await GetByPostalCode(query.PostalCode));
                if (query.State != null)
                    collections.Add(await GetByState(query.State));
                if (query.Street != null)
                    collections.Add(await GetByStreet(query.Street));
                if (query.HouseNumber != null)
                    collections.Add(await GetByHouseNumber(query.HouseNumber));
                if (query.Latitude != null && query.Longitude != null)
                {
                    var res = await GetByLatitudeAndLongitude(query.Latitude.Value, query.Longitude.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Shop> { res });
                    }
                }
                if (query.Name != null)
                    collections.Add(await GetByName(query.Name));
                if (query.OrderId != null)
                {
                    var res = await GetByOrderId(query.OrderId.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Shop> { res });
                    }
                }
                if (query.StorageId != null)
                {
                    var res = await GetByStorageId(query.StorageId.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Shop> { res });
                    }
                }
                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<Shop>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Shops
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Використовуємо Union для об'єднання результатів
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                // Використовуємо Intersect для знаходження записів, які задовольняють всі умови
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "CityAsc":
                        result = result.OrderBy(ware => ware.Address.City).ToList();
                        break;
                    case "CityDesc":
                        result = result.OrderByDescending(ware => ware.Address.City).ToList();
                        break;
                    case "StateAsc":
                        result = result.OrderBy(ware => ware.Address.State).ToList();
                        break;
                    case "StateDesc":
                        result = result.OrderByDescending(ware => ware.Address.State).ToList();
                        break;
                    case "PostalCodeAsc":
                        result = result.OrderBy(ware => ware.Address.PostalCode).ToList();
                        break;
                    case "PostalCodeDesc":
                        result = result.OrderByDescending(ware => ware.Address.PostalCode).ToList();
                        break;
                    case "LatitudeAsc":
                        result = result.OrderBy(ware => ware.Address.Latitude).ToList();
                        break;
                    case "LatitudeDesc":
                        result = result.OrderByDescending(ware => ware.Address.Latitude).ToList();
                        break;
                    case "LongitudeAsc":
                        result = result.OrderBy(ware => ware.Address.Longitude).ToList();
                        break;
                    case "LongitudeDesc":
                        result = result.OrderByDescending(ware => ware.Address.Longitude).ToList();
                        break;
                    case "NameAsc":
                        result = result.OrderBy(ware => ware.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(ware => ware.Name).ToList();
                        break;
                    case "HouseNumberAsc":
                        result = result.OrderBy(ware => ware.Address.HouseNumber).ToList();
                        break;
                    case "HouseNumberDesc":
                        result = result.OrderByDescending(ware => ware.Address.HouseNumber).ToList();
                        break;
                    case "StreetAsc":
                        result = result.OrderBy(ware => ware.Address.Street).ToList();
                        break;
                    case "StreetDesc":
                        result = result.OrderByDescending(ware => ware.Address.Street).ToList();
                        break;
                    case "IdAsc":
                        result = result.OrderBy(ware => ware.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(ware => ware.Id).ToList();
                        break;
                    case "StorageIdAsc":
                        result = result.OrderBy(ware => ware.Storage.Id).ToList();
                        break;
                    case "StorageIdDesc":
                        result = result.OrderByDescending(ware => ware.Storage.Id).ToList();
                        break;
                    case "OrderIdAsc":
                        result = result.OrderBy(ware => ware.Orders.First().Id).ToList();
                        break;
                    case "OrderIdDesc":
                        result = result.OrderByDescending(ware => ware.Orders.First().Id).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<Shop>();
        }

        public async Task<IEnumerable<Shop>> GetNearestShopsAsync(double latitude, double longitude, int numberOfShops = 5)
        {
            // Константа для переведення градусів в радіани
            double degreesToRadians = Math.PI / 180.0;

            // Радіус Землі в кілометрах
            double earthRadiusKm = 6371.0;

            var nearestShops = await _context.Addresses
                .Where(a => a.Latitude.HasValue && a.Longitude.HasValue && a.Shop != null)
                .Select(a => new
                {
                    Shop = a.Shop, // Отримуємо магазин напряму
                    Distance = earthRadiusKm * 2 * Math.Asin(
                        Math.Sqrt(
                            Math.Pow(Math.Sin((a.Latitude.Value - latitude) * degreesToRadians / 2), 2) +
                            Math.Cos(latitude * degreesToRadians) * Math.Cos(a.Latitude.Value * degreesToRadians) *
                            Math.Pow(Math.Sin((a.Longitude.Value - longitude) * degreesToRadians / 2), 2)
                        )
                    )
                })
                .OrderBy(x => x.Distance) // Сортування за відстанню
                .Take(numberOfShops) // Повертаємо лише задану кількість магазинів
                .Select(x => x.Shop) // Отримуємо лише магазини
                .ToListAsync();

            return nearestShops;
        }
        public async IAsyncEnumerable<Shop> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var item = await GetById(id);  // Виклик методу репозиторію
                if (item != null)
                {
                    yield return item;
                }
            }
        }
        public async Task Create(Shop shop)
        {
            await _context.Shops.AddAsync(shop);
        }
        public void Update(Shop updatedShop)
        {
            _context.Entry(updatedShop).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var shop = await GetById(id);
            if (shop != null)
                _context.Shops.Remove(shop);
        }
    }
}
