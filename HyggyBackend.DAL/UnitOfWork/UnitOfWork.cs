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
        private IShopRepository _shops;
        private ICustomerRepository _customers;
        private ShopEmployeeRepository _shopEmployees;
        private StorageEmployeeRepository _storageEmployees;
        private IProffessionRepository _proffessions;
        private IOrderRepository _orders;
        private IWareCategory1Repository _categories1;
        private IWareCategory2Repository _categories2;
        private IWareCategory3Repository _categories3;


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

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
