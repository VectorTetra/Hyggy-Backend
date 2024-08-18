﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities.Employes;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Repositories;
using HyggyBackend.DAL.Repositories.Employes;

namespace HyggyBackend.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HyggyContext _context;
        private IWareRepository _wares;
        private IOrderRepository _orders;
        


        public UnitOfWork(HyggyContext context)
        {
            _context = context;
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
                if(_shops == null)
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
            await _context.SaveChangesAsync();
        }
    }
}
