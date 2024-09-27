using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Storage>> GetAll();
        // Отримати всі центральні склади, не пов'язані з магазинами
        Task<IEnumerable<Storage>> GetGlobalStorages();
        // Отримати всі склади, які пов'язані з магазинами
        Task<IEnumerable<Storage>> GetNonGlobalStorages();
        Task<IEnumerable<Storage>> GetByQuery(StorageQueryDAL query);
        Task<Storage?> GetById(long id);
        Task<Storage?> GetByAddressId(long addressId);
        Task<Storage?> GetByShopId(long ShopId);
        Task<Storage?> GetByWareItemId(long WareItemId);
        Task<Storage?> GetByStorageEmployeeId(string storageEmployeeId);
        Task<Storage?> GetByShopEmployeeId(string shopEmployeeId);

        Task Create(Storage storage);
        void Update(Storage storage);
        Task Delete(long id);
    }
}
