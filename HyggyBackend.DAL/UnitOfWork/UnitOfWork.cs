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
        private IShopRepository _shops;
        private ShopEmployeeRepository _shopEmployees;
        private StorageEmployeeRepository _storageEmployees;
        private IProffessionRepository _proffessions;
        private IOrderRepository _orders;
        private IAddressRepository _addresses;
        public UnitOfWork(HyggyContext context)
        {
            _context = context;
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
        public IShopRepository Shops
        {
            get
            {
                if (_shops == null)
                    _shops = new ShopRepository(_context);
                return _shops;
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


        public async Task Save()
        {
           var saved = await _context.SaveChangesAsync();
        }
    }
}
