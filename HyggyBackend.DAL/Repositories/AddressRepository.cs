using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.DAL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly HyggyContext _context;

        public AddressRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.Addresses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

        public async Task<IEnumerable<Address>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var addresses = new List<Address>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var address in GetByIdsAsync(ids))
            {
                addresses.Add(address);
            }
            return addresses;
        }

        public async Task<IEnumerable<Address>> GetByHouseNumber(string HouseNumber)
        {
            return await _context.Addresses.Where(a => a.HouseNumber != null && a.HouseNumber.Contains(HouseNumber)).ToListAsync();
        }
        public async Task<IEnumerable<Address>> GetByCity(string City)
        {
            return await _context.Addresses.Where(a => a.City != null && a.City.Contains(City)).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetByStreet(string Street)
        {
            return await _context.Addresses.Where(a => a.Street != null && a.Street.Contains(Street)).ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetByState(string State)
        {
            return await _context.Addresses.Where(a => a.State != null && a.State.Contains(State)).ToListAsync();
        }
        public async Task<IEnumerable<Address>> GetByPostalCode(string PostalCode)
        {
            return await _context.Addresses.Where(a => a.PostalCode != null && a.PostalCode.Contains(PostalCode)).ToListAsync();
        }
        public async Task<IEnumerable<Address>> GetByLatitudeAndLongitude(double Latitude, double Longitude)
        {
            return await _context.Addresses.Where(a => (a.Latitude != null && a.Longitude != null && a.Latitude == Latitude && a.Longitude == Longitude)).ToListAsync();
        }
        public async Task<IEnumerable<Address>> GetByQuery(AddressQueryDAL query)
        {
            var collections = new List<IEnumerable<Address>>();

            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(new List<Address> { await GetByIdAsync(id) });
                }
                collections.Add(await GetByHouseNumber(query.QueryAny));
                collections.Add(await GetByStreet(query.QueryAny));
                collections.Add(await GetByCity(query.QueryAny));
                collections.Add(await GetByState(query.QueryAny));
                collections.Add(await GetByPostalCode(query.QueryAny));
            }
            else
            {
                if (query.Id != null)
                {
                    var res = await GetByIdAsync(query.Id.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Address> { res });
                    }
                }
                if (query.HouseNumber != null)
                {
                    collections.Add(await GetByHouseNumber(query.HouseNumber));
                }
                if (query.Street != null)
                {
                    collections.Add(await GetByStreet(query.Street));
                }
                if (query.City != null)
                {
                    collections.Add(await GetByCity(query.City));
                }
                if (query.State != null)
                {
                    collections.Add(await GetByState(query.State));
                }
                if (query.PostalCode != null)
                {
                    collections.Add(await GetByPostalCode(query.PostalCode));
                }
                if (query.Latitude != null && query.Longitude != null)
                {
                    collections.Add(await GetByLatitudeAndLongitude(query.Latitude.Value, query.Longitude.Value));
                }
                if (query.ShopId != null)
                {
                    var res = await GetByShopId(query.ShopId.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Address> { res });
                    }
                }
                if (query.StorageId != null)
                {
                    var res = await GetByStorageId(query.StorageId.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Address> { res });
                    }
                }
                if (query.OrderId != null)
                {
                    var res = await GetByOrderId(query.OrderId.Value);
                    if (res != null)
                    {
                        collections.Add(new List<Address> { res });
                    }
                }
            }

            var result = new List<Address>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Addresses
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

            if (query.Sorting != null)
            {
                // Сортування за вказаними критеріями
                switch (query.Sorting)
                {
                    case "CityAsc":
                        result = result.OrderBy(ware => ware.City).ToList();
                        break;
                    case "CityDesc":
                        result = result.OrderByDescending(ware => ware.City).ToList();
                        break;
                    case "StateAsc":
                        result = result.OrderBy(ware => ware.State).ToList();
                        break;
                    case "StateDesc":
                        result = result.OrderByDescending(ware => ware.State).ToList();
                        break;
                    case "PostalCodeAsc":
                        result = result.OrderBy(ware => ware.PostalCode).ToList();
                        break;
                    case "PostalCodeDesc":
                        result = result.OrderByDescending(ware => ware.PostalCode).ToList();
                        break;
                    case "LatitudeAsc":
                        result = result.OrderBy(ware => ware.Latitude).ToList();
                        break;
                    case "LatitudeDesc":
                        result = result.OrderByDescending(ware => ware.Latitude).ToList();
                        break;
                    case "LongitudeAsc":
                        result = result.OrderBy(ware => ware.Longitude).ToList();
                        break;
                    case "LongitudeDesc":
                        result = result.OrderByDescending(ware => ware.Longitude).ToList();
                        break;
                    case "IdAsc":
                        result = result.OrderBy(ware => ware.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(ware => ware.Id).ToList();
                        break;
                    case "HouseNumberAsc":
                        result = result.OrderBy(ware => ware.HouseNumber).ToList();
                        break;
                    case "HouseNumberDesc":
                        result = result.OrderByDescending(ware => ware.HouseNumber).ToList();
                        break;
                    case "StreetAsc":
                        result = result.OrderBy(ware => ware.Street).ToList();
                        break;
                    case "StreetDesc":
                        result = result.OrderByDescending(ware => ware.Street).ToList();
                        break;
                    case "ShopIdAsc":
                        result = result.OrderBy(ware => ware.Shop?.Id).ToList();
                        break;
                    case "ShopIdDesc":
                        result = result.OrderByDescending(ware => ware.Shop?.Id).ToList();
                        break;
                    case "StorageIdAsc":
                        result = result.OrderBy(ware => ware.Storage?.Id).ToList();
                        break;
                    case "StorageIdDesc":
                        result = result.OrderByDescending(ware => ware.Storage?.Id).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<Address>();
        }


        public async Task<Address?> GetByShopId(long shopId)
        {
            return await _context.Addresses.Where(a => a.Shop != null && a.Shop.Id == shopId).FirstOrDefaultAsync();
        }

        public async Task<Address?> GetByStorageId(long StorageId)
        {
            return await _context.Addresses.Where(a => a.Storage != null && a.Storage.Id == StorageId).FirstOrDefaultAsync();
        }
        public async Task<Address?> GetByOrderId(long OrderId)
        {
            return await _context.Addresses.Where(a => a.Orders.Any(o => o.Id == OrderId)).FirstOrDefaultAsync();
        }
        public void Update(Address updatedAddress)
        {
            _context.Entry(updatedAddress).State = EntityState.Modified;
        }
        public async Task DeleteAsync(long id)
        {
            var address = await GetByIdAsync(id);
            if (address != null)
                _context.Addresses.Remove(address);
        }
    }
}
