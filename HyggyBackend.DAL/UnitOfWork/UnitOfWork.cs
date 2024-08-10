using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Repositories;

namespace HyggyBackend.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HyggyContext _context;
        private IWareRepository _wares;
        private IShopRepository _shops;
        //private IEmailRepository _emails;
        //private IPhoneRepository _phones;
        //private IPersonRepository _persons;
        //private ICountriesRepository _countries;
        //private ISettlementsRepository _settlements;
        //private ITourStateRepository _statuses;
        //private ITourNameRepository _tourNames;
        //private ITourRepository _tours;
        //private IPositionRepository _positions;
        //private IReviewRepository _reviews;
        //private IReviewImageRepository _reviewImages;
        //private ITransportTypeRepository _transportTypes;
        //private IBookingRepository _bookings;
        //private IBookingDataRepository _bookingDatas;
        //private IBedConfigurationRepository _bedConfigurations;
        //private IBookingChildrenRepository _bookingChildrens;
        //private IHotelRepository _hotels;
        //private IHotelConfigurationRepository _hotelConfigurations;
        //private IHotelServiceTypeRepository _hotelServiceTypes;
        //private IHotelServiceRepository _hotelServices;
        //private IHotelImageRepository _hotelImages;
        //private ITourImageRepository _tourImages;
        //private ITouragencyAccountRepository _account;
        //private ITouragencyAccountRoleRepository _role;
        //private ITouragencyEmployeeRepository _employee;
        //private IContinentRepository _continents;
        //private INewsRepository _news;


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
        //public IContinentRepository Continents
        //{
        //    get
        //    {
        //        if (_continents == null)
        //            _continents = new ContinentRepository(_context);
        //        return _continents;
        //    }
        //}
        //public ITouragencyEmployeeRepository TouragencyEmployees
        //{
        //    get
        //    {
        //        if (_employee == null)
        //            _employee = new TouragencyEmployeeRepository(_context);
        //        return _employee;
        //    }
        //}
        //public ITouragencyAccountRoleRepository TouragencyAccountRoles
        //{
        //    get
        //    {
        //        if (_role == null)
        //            _role = new TouragencyAccountRoleRepository(_context);
        //        return _role;
        //    }
        //}
        //public ITouragencyAccountRepository TouragencyAccounts
        //{
        //    get
        //    {
        //        if (_account == null)
        //            _account = new TouragencyAccountRepository(_context);
        //        return _account;
        //    }
        //}
        //public ITourImageRepository TourImages
        //{
        //    get
        //    {
        //        if (_tourImages == null)
        //            _tourImages = new TourImageRepository(_context);
        //        return _tourImages;
        //    }
        //}
        //public IHotelImageRepository HotelImages
        //{
        //    get
        //    {
        //        if (_hotelImages == null)
        //            _hotelImages = new HotelImageRepository(_context);
        //        return _hotelImages;
        //    }
        //}

        //public IHotelServiceRepository HotelServices
        //{
        //    get
        //    {
        //        if (_hotelServices == null)
        //            _hotelServices = new HotelServiceRepository(_context);
        //        return _hotelServices;
        //    }
        //}
        //public IHotelServiceTypeRepository HotelServiceTypes
        //{
        //    get
        //    {
        //        if (_hotelServiceTypes == null)
        //            _hotelServiceTypes = new HotelServiceTypeRepository(_context);
        //        return _hotelServiceTypes;
        //    }
        //}
        //public IHotelConfigurationRepository HotelConfigurations
        //{
        //    get
        //    {
        //        if (_hotelConfigurations == null)
        //            _hotelConfigurations = new HotelConfigurationRepository(_context);
        //        return _hotelConfigurations;
        //    }
        //}

        //public IHotelRepository Hotels
        //{
        //    get
        //    {
        //        if (_hotels == null)
        //            _hotels = new HotelRepository(_context);
        //        return _hotels;
        //    }
        //}
        //public IBookingChildrenRepository BookingChildrens
        //{
        //    get
        //    {
        //        if (_bookingChildrens == null)
        //            _bookingChildrens = new BookingChildrenRepository(_context);
        //        return _bookingChildrens;
        //    }
        //}
        //public IBookingDataRepository BookingDatas
        //{
        //    get
        //    {
        //        if (_bookingDatas == null)
        //            _bookingDatas = new BookingDataRepository(_context);
        //        return _bookingDatas;
        //    }
        //}
        //public IBookingRepository Bookings
        //{
        //    get
        //    {
        //        if (_bookings == null)
        //            _bookings = new BookingRepository(_context);
        //        return _bookings;
        //    }
        //}
        //public IBedConfigurationRepository BedConfigurations
        //{
        //    get
        //    {
        //        if (_bedConfigurations == null)
        //            _bedConfigurations = new BedConfigurationRepository(_context);
        //        return _bedConfigurations;
        //    }
        //}
        //public ITransportTypeRepository TransportTypes
        //{
        //    get
        //    {
        //        if (_transportTypes == null)
        //            _transportTypes = new TransportTypeRepository(_context);
        //        return _transportTypes;
        //    }
        //}
        //public IReviewImageRepository ReviewImages
        //{
        //    get
        //    {
        //        if (_reviewImages == null)
        //            _reviewImages = new ReviewImageRepository(_context);
        //        return _reviewImages;
        //    }
        //}
        //public IPositionRepository Positions
        //{
        //    get
        //    {
        //        if (_positions == null)
        //            _positions = new PositionRepository(_context);
        //        return _positions;
        //    }
        //}
        //public ITourNameRepository TourNames
        //{
        //    get
        //    {
        //        if (_tourNames == null)
        //            _tourNames = new TourNameRepository(_context);
        //        return _tourNames;
        //    }
        //}
        //public ITourRepository Tours
        //{
        //    get
        //    {
        //        if (_tours == null)
        //            _tours = new TourRepository(_context);
        //        return _tours;
        //    }
        //}
        //public IPersonRepository Persons
        //{
        //    get
        //    {
        //        if (_persons == null)
        //            _persons = new PersonRepository(_context);
        //        return _persons;
        //    }
        //}
        //public IClientRepository Clients
        //{
        //    get
        //    {
        //        if (_clients == null)
        //            _clients = new ClientRepository(_context);
        //        return _clients;
        //    }
        //}
        //public IEmailRepository Emails
        //{
        //    get
        //    {
        //        if (_emails == null)
        //            _emails = new EmailRepository(_context);
        //        return _emails;
        //    }
        //}
        //public IPhoneRepository Phones
        //{
        //    get
        //    {
        //        if (_phones == null)
        //            _phones = new PhoneRepository(_context);
        //        return _phones;
        //    }
        //}
        //public ICountriesRepository Countries
        //{
        //    get
        //    {
        //        if (_countries == null)
        //            _countries = new CountriesRepository(_context);
        //        return _countries;
        //    }
        //}
        //public ISettlementsRepository Settlements
        //{
        //    get
        //    {
        //        if (_settlements == null)
        //            _settlements = new SettlementsRepository(_context);
        //        return _settlements;
        //    }
        //}
        //public ITourStateRepository TourStates
        //{
        //    get
        //    {
        //        if (_statuses == null)
        //            _statuses = new TourStateRepository(_context);
        //        return _statuses;
        //    }
        //}
        //public IReviewRepository Reviews
        //{
        //    get
        //    {
        //        if (_reviews == null)
        //            _reviews = new ReviewRepository(_context);
        //        return _reviews;
        //    }
        //}
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
