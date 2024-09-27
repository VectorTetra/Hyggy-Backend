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
    public class StorageService : IStorageService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public StorageService(IUnitOfWork database, IMapper mapper)
        {
            Database = database;
            _mapper = mapper;
        }
        public async Task<StorageDTO> Create(StorageDTO storage)
        {
            if(storage.AddressId == null)
            {
                throw new ValidationException($"AddressId не вказано! Id: {storage.AddressId}","");
            }
            var existedAddress = await Database.Addresses.GetByIdAsync(storage.AddressId.Value);
            if (existedAddress == null)
            {
                throw new ValidationException($"Адреса з id {storage.AddressId} не знайдена!", "");
            }

            var storageDAL = new Storage
            {
                Id = storage.Id,
                AddressId = storage.AddressId,
                ShopId = storage.ShopId
            };

            await Database.Storages.Create(storageDAL);
            await Database.Save();

            return _mapper.Map<StorageDTO>(storageDAL);

        }

        public async Task<StorageDTO> Delete(long id)
        {
            var storage = await Database.Storages.GetById(id);
            if (storage == null)
            {
                throw new ValidationException($"Склад з id {id} не знайдено!", "");
            }

            await Database.Storages.Delete(id);
            await Database.Save();

            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<IEnumerable<StorageDTO>> GetAll()
        {
            var storages = await Database.Storages.GetAll();

            return _mapper.Map<IEnumerable<Storage>, IEnumerable<StorageDTO>>(storages);
        }

        public async Task<IEnumerable<StorageDTO>> GetGlobalStorages()
        {
            var storages = await Database.Storages.GetGlobalStorages();

            return _mapper.Map<IEnumerable<Storage>, IEnumerable<StorageDTO>>(storages);
        }

        public async Task<StorageDTO?> GetById(long id)
        {
            var storage = await Database.Storages.GetById(id);

            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<StorageDTO?> GetByAddressId(long addressId)
        {
            var storage = await Database.Storages.GetByAddressId(addressId);

            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<StorageDTO?> GetByShopEmployeeId(string shopEmployeeId)
        {
            var storage = await Database.Storages.GetByShopEmployeeId(shopEmployeeId);
            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<StorageDTO?> GetByShopId(long ShopId)
        {
            var storage = await Database.Storages.GetByShopId(ShopId);

            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<StorageDTO?> GetByStorageEmployeeId(string storageEmployeeId)
        {
            var storage = await Database.Storages.GetByStorageEmployeeId(storageEmployeeId);

            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<StorageDTO?> GetByWareItemId(long WareItemId)
        {
            var storage = await Database.Storages.GetByWareItemId(WareItemId);

            return _mapper.Map<StorageDTO>(storage);
        }

        public async Task<IEnumerable<StorageDTO>> GetNonGlobalStorages()
        {
            var storages = await Database.Storages.GetNonGlobalStorages();

            return _mapper.Map<IEnumerable<Storage>, IEnumerable<StorageDTO>>(storages);
        }

        public async Task<IEnumerable<StorageDTO>> GetByQuery(StorageQueryBLL query)
        {
            var storageQueryDAL = _mapper.Map<StorageQueryDAL>(query);
            var storages = await Database.Storages.GetByQuery(storageQueryDAL);

            return _mapper.Map<IEnumerable<Storage>, IEnumerable<StorageDTO>>(storages);
        }

        public async Task<StorageDTO> Update(StorageDTO storage)
        {
            if (storage.AddressId == null)
            {
                throw new ValidationException($"AddressId не вказано! Id: {storage.AddressId}", "");
            }
            var existedAddress = await Database.Addresses.GetByIdAsync(storage.AddressId.Value);
            if (existedAddress == null)
            {
                throw new ValidationException($"Адреса з id {storage.AddressId} не знайдена!", "");
            }
            if(storage.ShopId != null)
            {
                var existedShop = await Database.Shops.GetById(storage.ShopId.Value);
                if (existedShop == null)
                {
                    throw new ValidationException($"Магазин з id {storage.ShopId} не знайдено!", "");
                }
            }

            var storageDAL = new Storage
            {
                Id = storage.Id,
                AddressId = storage.AddressId,
                ShopId = storage.ShopId
            };

            Database.Storages.Update(storageDAL);
            await Database.Save();

            return _mapper.Map<StorageDTO>(storageDAL);
        }
    }
}
