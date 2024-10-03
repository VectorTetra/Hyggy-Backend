using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IStorageService
    {
        Task<IEnumerable<StorageDTO>> GetAll();
        // Отримати всі центральні склади, не пов'язані з магазинами
        Task<IEnumerable<StorageDTO>> GetGlobalStorages();
        // Отримати всі склади, які пов'язані з магазинами
        Task<IEnumerable<StorageDTO>> GetNonGlobalStorages();

        Task<IEnumerable<StorageDTO>> GetByQuery(StorageQueryBLL query);
        Task<StorageDTO?> GetById(long id);
        Task<StorageDTO?> GetByAddressId(long addressId);
        Task<StorageDTO?> GetByShopId(long ShopId);
        Task<StorageDTO?> GetByWareItemId(long WareItemId);
        Task<StorageDTO?> GetByStorageEmployeeId(string storageEmployeeId);
        Task<StorageDTO?> GetByShopEmployeeId(string shopEmployeeId);

        Task<StorageDTO> Create(StorageDTO storage);
        Task<StorageDTO> Update(StorageDTO storage);
        Task<StorageDTO> Delete(long id);
    }
}
