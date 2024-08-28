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
        IShopRepository Shops { get; }
        IProffessionRepository Proffessions { get; }
        IEmployeeRepository<StorageEmployee> StorageEmployees { get; }
        IEmployeeRepository<ShopEmployee> ShopEmployees { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        IAddressRepository Addresses { get; }

        Task Save();
    }
}
