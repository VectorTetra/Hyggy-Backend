using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Repositories;
using HyggyBackend.DAL.Repositories.Employes;
using Microsoft.AspNetCore.Identity;

namespace HyggyBackend.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HyggyContext _context;
        private IWareRepository _wares;
        private IWareItemRepository _wareItems;

        private IWarePriceHistoryRepository _warePriceHistories;
        private IShopRepository _shops;
        private ICustomerRepository _customers;
        private ShopEmployeeRepository _shopEmployees;
        private StorageEmployeeRepository _storageEmployees;
        private IProffessionRepository _proffessions;
        private IOrderRepository _orders;
        private IWareCategory1Repository _categories1;
        private IWareCategory2Repository _categories2;
        private IWareCategory3Repository _categories3;
        private IWareStatusRepository _wareStatuses;
        private IWareImageRepository _wareImages;


        private IOrderItemRepository _orderItems;
        private IOrderStatusRepository _orderStatuses;
        private IAddressRepository _addresses;
        private IMainStorageRepository _globalStorage;
        public UnitOfWork(HyggyContext context)
        {
            _context = context;
        }

        public IWareItemRepository WareItems
        {
            get
            {
                if (_wareItems == null)
                    _wareItems = new WareItemRepository(_context);
                return _wareItems;
            }
        }
        public IAddressRepository Addresses
		{
			get
			{
				if (_addresses == null)
					_addresses = new AddressRepository(_context);
				return _addresses;
			}
		}
		public IWareRepository Wares
        {
            get
            {
                if (_wares == null)
                    _wares = new WareRepository(_context);
                return _wares;
            }
        }
        public IWarePriceHistoryRepository WarePriceHistories
        {
            get
            {
                if (_warePriceHistories == null)
                    _warePriceHistories = new WarePriceHistoryRepository(_context);
                return _warePriceHistories;
            }
        }

        public IShopRepository Shops
        {
            get
            {
                if (_shops == null)
                    _shops = new ShopRepository(_context);
                return _shops;
            }
        }
        public IMainStorageRepository MainStorages
        {
            get
            {
                if(_globalStorage == null)
                    _globalStorage = new MainStorageRepository(_context);
                return _globalStorage;
            }
        }
        public IEmployeeRepository<StorageEmployee> StorageEmployees
        {
            get
            {
                if (_storageEmployees == null)
                    _storageEmployees = new StorageEmployeeRepository(_context);
                return _storageEmployees;
            }
        }
        public IEmployeeRepository<ShopEmployee> ShopEmployees
        {
			get
			{
				if (_shopEmployees == null)
                    _shopEmployees = new ShopEmployeeRepository(_context);
                return _shopEmployees;
            }
        }
        public IProffessionRepository Proffessions
        {
            get
            {
                if (_proffessions == null)
                    _proffessions = new ProffessionRepository(_context);
                return _proffessions;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrderRepository(_context);
                return _orders;
            }
        }
        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);
                return _customers;
            }
        }

        public IWareCategory1Repository Categories1
        {
            get
            {
                if (_categories1 == null)
                    _categories1 = new WareCategory1Repository(_context);
                return _categories1;
            }
        }

        public IWareCategory2Repository Categories2
        {
            get
            {
                if (_categories2 == null)
                    _categories2 = new WareCategory2Repository(_context);
                return _categories2;
            }
        }

        public IWareCategory3Repository Categories3
        {
            get
            {
                if (_categories3 == null)
                    _categories3 = new WareCategory3Repository(_context);
                return _categories3;
            }
        }

        public IWareStatusRepository WareStatuses
        {
            get
            {
                if (_wareStatuses == null)
                    _wareStatuses = new WareStatusRepository(_context);
                return _wareStatuses;
            }
        }

        public IWareImageRepository WareImages
        {
            get
            {
                if (_wareImages == null)
                    _wareImages = new WareImageRepository(_context);
                return _wareImages;
            }
        }
        public IOrderItemRepository OrderItems
        {
            get
            {
                if (_orderItems == null)
                    _orderItems = new OrderItemRepository(_context);
                return _orderItems;
            }
        }
        public IOrderStatusRepository OrderStatuses
        {
            get
            {
                if (_orderStatuses == null)
                    _orderStatuses = new OrderStatusRepository(_context);
                return _orderStatuses;
            }
        }

        public async Task Save()
        {
           var saved = await _context.SaveChangesAsync();
        }
    }
}
