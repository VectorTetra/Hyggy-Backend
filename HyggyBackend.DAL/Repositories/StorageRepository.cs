using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly HyggyContext _context;

        public StorageRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Storage>> GetAll()
        {
            return await _context.Storages.ToListAsync();
        }
        // Отримати всі центральні склади, не пов'язані з магазинами
        public async Task<IEnumerable<Storage>> GetGlobalStorages()
        {
            return await _context.Storages.Where(s => s.Shop.Id == null).ToListAsync();
        }
        // Отримати всі склади, які пов'язані з магазинами
        public async Task<IEnumerable<Storage>> GetNonGlobalStorages()
        {
            return await _context.Storages.Where(s => s.Shop.Id != null).ToListAsync();
        }

        public async Task<IEnumerable<Storage>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<Storage>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async Task<IEnumerable<Storage>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.Storages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<Storage?> GetById(long id)
        {
            return await _context.Storages.FindAsync(id);
        }
        public async Task<Storage?> GetByAddressId(long addressId)
        {
            return await _context.Storages.FirstOrDefaultAsync(x => x.AddressId == addressId);
        }
        public async Task<Storage?> GetByShopId(long ShopId)
        {
            return await _context.Storages.FirstOrDefaultAsync(x => x.Shop.Id == ShopId);
        }
        public async Task<Storage?> GetByWareItemId(long WareItemId)
        {
            return await _context.Storages.FirstOrDefaultAsync(x => x.WareItems.Any(wi => wi.Id == WareItemId));
        }
        public async Task<Storage?> GetByStorageEmployeeId(string storageEmployeeId)
        {
            return await _context.Storages.FirstOrDefaultAsync(x => x.StorageEmployees.Any(sem => sem.Id == storageEmployeeId));
        }
        public async Task<Storage?> GetByShopEmployeeId(string shopEmployeeId)
        {
            return await _context.Storages
                .FirstOrDefaultAsync(x => x.Shop != null && x.Shop.ShopEmployees.Any(sem => sem.Id == shopEmployeeId));
        }

        public async IAsyncEnumerable<Storage> GetByIdsAsync(IEnumerable<long> ids)
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

        public async Task<IEnumerable<Storage>> GetByQuery(StorageQueryDAL query)
        {
            var collections = new List<IEnumerable<Storage>>();

            if (query.AddressId != null)
            {
                collections.Add(await _context.Storages.Where(x => x.AddressId == query.AddressId).ToListAsync());
            }

            if (query.IsGlobal != null)
            {
                if (query.IsGlobal == true)
                {
                    collections.Add(await GetGlobalStorages());
                }
                else
                {
                    collections.Add(await GetNonGlobalStorages());
                }
            }

            if (query.ShopId != null)
            {
                collections.Add(await _context.Storages.Where(x => x.Shop.Id == query.ShopId).ToListAsync());
            }

            if (query.WareItemId != null)
            {
                collections.Add(await _context.Storages.Where(x => x.WareItems.Any(wi => wi.Id == query.WareItemId)).ToListAsync());
            }

            if (query.StorageEmployeeId != null)
            {
                collections.Add(await _context.Storages.Where(x => x.StorageEmployees.Any(sem => sem.Id == query.StorageEmployeeId)).ToListAsync());
            }

            if (query.ShopEmployeeId != null)
            {
                collections.Add(await _context.Storages.Where(x => x.Shop != null && x.Shop.ShopEmployees.Any(sem => sem.Id == query.ShopEmployeeId)).ToListAsync());
            }

            if (query.Id != null)
            {
                collections.Add(new List<Storage> { await GetById(query.Id.Value) });
            }

            if (query.StringIds != null)
            {
                collections.Add(await GetByStringIds(query.StringIds));
            }

            var result = new List<Storage>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Storages
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .ToList();
            }
            else
            {
                result = (List<Storage>)collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
            }


            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "IdAsc":
                        result = result.OrderBy(ware => ware.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(ware => ware.Id).ToList();
                        break;
                    case "AddressIdAsc":
                        result = result.OrderBy(ware => ware.AddressId).ToList();
                        break;
                    case "AddressIdDesc":
                        result = result.OrderByDescending(ware => ware.AddressId).ToList();
                        break;
                    case "ShopIdAsc":
                        result = result.OrderBy(ware => ware.Shop?.Id).ToList();
                        break;
                    case "ShopIdDesc":
                        result = result.OrderByDescending(ware => ware.Shop?.Id).ToList();
                        break;
                    case "ShopNameAsc":
                        result = result.OrderBy(ware => ware.Shop?.Name).ToList();
                        break;
                    case "ShopNameDesc":
                        result = result.OrderByDescending(ware => ware.Shop?.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null)
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            if (!result.Any())
            {
                return new List<Storage>();
            }
            return result;
        }
        public async Task Create(Storage storage)
        {
            await _context.Storages.AddAsync(storage);
        }
        public void Update(Storage storage)
        {
            _context.Entry(storage).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var storage = await _context.Storages.FindAsync(id);
            if (storage != null) { _context.Storages.Remove(storage); }
        }
    }
}
