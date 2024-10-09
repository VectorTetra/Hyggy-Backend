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
    public class StorageRepository: IStorageRepository
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
            return await _context.Storages.FirstOrDefaultAsync(x => x.WareItems.Any(wi=>wi.Id == WareItemId));
        }
        public async Task<Storage?> GetByStorageEmployeeId(string storageEmployeeId)
        {
            return await _context.Storages.FirstOrDefaultAsync(x => x.StorageEmployees.Any(sem=>sem.Id == storageEmployeeId) );
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
            var storageCollections = new List<IEnumerable<Storage>>();

            if (query.AddressId != null)
            {
                storageCollections.Add(await _context.Storages.Where(x => x.AddressId == query.AddressId).ToListAsync());
            }
            
            if( query.IsGlobal != null)
            {
                if (query.IsGlobal == true)
                {
                    storageCollections.Add(await GetGlobalStorages());
                }
                else
                {
                    storageCollections.Add(await GetNonGlobalStorages());
                }
            }

            if (query.ShopId != null)
            {
                storageCollections.Add(await _context.Storages.Where(x => x.Shop.Id == query.ShopId).ToListAsync());
            }

            if (query.WareItemId != null)
            {
                storageCollections.Add(await _context.Storages.Where(x => x.WareItems.Any(wi => wi.Id == query.WareItemId)).ToListAsync());
            }

            if (query.StorageEmployeeId != null)
            {
                storageCollections.Add(await _context.Storages.Where(x => x.StorageEmployees.Any(sem => sem.Id == query.StorageEmployeeId)).ToListAsync());
            }

            if (query.ShopEmployeeId != null)
            {
                storageCollections.Add(await _context.Storages.Where(x => x.Shop != null && x.Shop.ShopEmployees.Any(sem => sem.Id == query.ShopEmployeeId)).ToListAsync());
            }

            if(query.Id != null)
            {
                storageCollections.Add(new List<Storage> { await GetById(query.Id.Value) });
            }
            return storageCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
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
