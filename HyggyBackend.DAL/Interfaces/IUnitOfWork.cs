using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Repositories;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IUnitOfWork
    {
       
        IWareRepository Wares { get; }

        IWareItemRepository WareItems { get; }
        ICustomerRepository Customers { get; }
        IWarePriceHistoryRepository WarePriceHistories { get; }
        IShopRepository Shops { get; }
        IProffessionRepository Proffessions { get; }
        IEmployeeRepository<StorageEmployee> StorageEmployees { get; }
        IEmployeeRepository<ShopEmployee> ShopEmployees { get; }
        IOrderRepository Orders { get; }
        IWareCategory1Repository Categories1 { get; }
        IWareCategory2Repository Categories2 { get; }
        IWareCategory3Repository Categories3 { get; }
        IWareStatusRepository WareStatuses { get; }
        IWareImageRepository WareImages { get; }
        IOrderItemRepository OrderItems { get; }
        IOrderStatusRepository OrderStatuses { get; }
        IAddressRepository Addresses { get; }
        IMainStorageRepository MainStorages { get; }
        Task Save();
    }
}
