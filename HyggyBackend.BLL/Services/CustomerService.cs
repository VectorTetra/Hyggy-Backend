using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        IUnitOfWork Database;
        public CustomerService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerDTO>()
            .ForPath(dst => dst.Orders, opt => opt.MapFrom(src => src.Orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                DeliveryAddress = new AddressDTO
                {
                    Id = o.DeliveryAddress.Id,
                    City = o.DeliveryAddress.City,
                    Street = o.DeliveryAddress.Street,
                    HouseNumber = o.DeliveryAddress.HouseNumber,
                    State = o.DeliveryAddress.State,
                    PostalCode = o.DeliveryAddress.PostalCode,
                    Latitude = o.DeliveryAddress.Latitude,
                    Longitude = o.DeliveryAddress.Longitude
                },
                OrderDate = o.OrderDate,
                Phone = o.Phone,
                Comment = o.Comment,
                Status = new OrderStatusDTO
                {
                    Id = o.Status.Id,
                    Name = o.Status.Name,
                    Description = o.Status.Description
                },
                Shop = new ShopDTO
                {
                    Id = o.Shop.Id,
                    PhotoUrl = o.Shop.PhotoUrl,
                    WorkHours = o.Shop.WorkHours,
                    Address = new AddressDTO
                    {
                        Id = o.Shop.Address.Id,
                        City = o.Shop.Address.City,
                        Street = o.Shop.Address.Street,
                        HouseNumber = o.Shop.Address.HouseNumber,
                        State = o.Shop.Address.State,
                        PostalCode = o.Shop.Address.PostalCode,
                        Latitude = o.Shop.Address.Latitude,
                        Longitude = o.Shop.Address.Longitude
                    }
                },
                Customer = new CustomerDTO
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Surname = o.Customer.Surname,
                    Email = o.Customer.Email
                },
                OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    Count = oi.Count,
                    Ware = new WareDTO
                    {
                        Id = oi.Ware.Id,
                        Name = oi.Ware.Name,
                        Description = oi.Ware.Description,
                        Price = oi.Ware.Price,
                        WareCategory3 = new WareCategory3DTO
                        {
                            Id = oi.Ware.WareCategory3.Id,
                            JSONStructureFilePath = oi.Ware.WareCategory3.JSONStructureFilePath,
                            Name = oi.Ware.WareCategory3.Name,
                            WareCategory2 = new WareCategory2DTO
                            {
                                Id = oi.Ware.WareCategory3.WareCategory2.Id,
                                JSONStructureFilePath = oi.Ware.WareCategory3.WareCategory2.JSONStructureFilePath,
                                Name = oi.Ware.WareCategory3.WareCategory2.Name,
                                WareCategory1 = new WareCategory1DTO
                                {
                                    Id = oi.Ware.WareCategory3.WareCategory2.WareCategory1.Id,
                                    JSONStructureFilePath = oi.Ware.WareCategory3.WareCategory2.WareCategory1.JSONStructureFilePath,
                                    Name = oi.Ware.WareCategory3.WareCategory2.WareCategory1.Name
                                }
                            }
                        }
                    },
                    PriceHistory = new WarePriceHistoryDTO
                    {
                        Id = oi.PriceHistory.Id,
                        Price = oi.PriceHistory.Price,
                        EffectiveDate = oi.PriceHistory.EffectiveDate
                    }

                }).ToList()
            })));

            cfg.CreateMap<CustomerDTO, Customer>()
                .ForPath(dst => dst.Orders, opt => opt.MapFrom(src => src.Orders.Select(o => new Order
                {
                    Id = o.Id,
                    DeliveryAddress = new Address
                    {
                        Id = o.DeliveryAddress.Id,
                        City = o.DeliveryAddress.City,
                        Street = o.DeliveryAddress.Street,
                        HouseNumber = o.DeliveryAddress.HouseNumber,
                        State = o.DeliveryAddress.State,
                        PostalCode = o.DeliveryAddress.PostalCode,
                        Latitude = o.DeliveryAddress.Latitude,
                        Longitude = o.DeliveryAddress.Longitude
                    },
                    OrderDate = o.OrderDate,
                    Phone = o.Phone,
                    Comment = o.Comment,
                    Status = new OrderStatus
                    {
                        Id = o.Status.Id,
                        Name = o.Status.Name,
                        Description = o.Status.Description
                    },
                    Shop = new Shop
                    {
                        Id = o.Shop.Id,
                        PhotoUrl = o.Shop.PhotoUrl,
                        WorkHours = o.Shop.WorkHours,
                        Address = new Address
                        {
                            Id = o.Shop.Address.Id,
                            City = o.Shop.Address.City,
                            Street = o.Shop.Address.Street,
                            HouseNumber = o.Shop.Address.HouseNumber,
                            State = o.Shop.Address.State,
                            PostalCode = o.Shop.Address.PostalCode,
                            Latitude = o.Shop.Address.Latitude,
                            Longitude = o.Shop.Address.Longitude
                        }
                    },
                    Customer = new Customer
                    {
                        Id = o.Customer.Id,
                        Name = o.Customer.Name,
                        Surname = o.Customer.Surname,
                        Email = o.Customer.Email
                    },
                    OrderItems = o.OrderItems.Select(oi => new OrderItem
                    {
                        Id = oi.Id,
                        Count = oi.Count,
                        Ware = new Ware
                        {
                            Id = oi.Ware.Id,
                            Name = oi.Ware.Name,
                            Description = oi.Ware.Description,
                            Price = oi.Ware.Price,
                            WareCategory3 = new WareCategory3
                            {
                                Id = oi.Ware.WareCategory3.Id,
                                JSONStructureFilePath = oi.Ware.WareCategory3.JSONStructureFilePath,
                                Name = oi.Ware.WareCategory3.Name,
                                WareCategory2 = new WareCategory2
                                {
                                    Id = oi.Ware.WareCategory3.WareCategory2.Id,
                                    JSONStructureFilePath = oi.Ware.WareCategory3.WareCategory2.JSONStructureFilePath,
                                    Name = oi.Ware.WareCategory3.WareCategory2.Name,
                                    WareCategory1 = new WareCategory1
                                    {
                                        Id = oi.Ware.WareCategory3.WareCategory2.WareCategory1.Id,
                                        JSONStructureFilePath = oi.Ware.WareCategory3.WareCategory2.WareCategory1.JSONStructureFilePath,
                                        Name = oi.Ware.WareCategory3.WareCategory2.WareCategory1.Name
                                    }
                                }
                            }
                        },
                        PriceHistory = new WarePriceHistory
                        {
                            Id = oi.PriceHistory.Id,
                            Price = oi.PriceHistory.Price.Value,
                            EffectiveDate = oi.PriceHistory.EffectiveDate.Value
                        }
                    }).ToList()
                })));

        });

        MapperConfiguration CustomerQueryBLL_CustomerQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<CustomerQueryBLL, CustomerQueryDAL>());

        public async Task<IEnumerable<CustomerDTO>> GetPagedCustomers(int pageNumber, int pageSize)
        {

            var customers = await Database.Customers.GetPagedCustomers(pageNumber, pageSize);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);

        }
        public async Task<IEnumerable<CustomerDTO>> GetByOrderId(long orderId) 
        {
            var customers = await Database.Customers.GetByOrderId(orderId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByNameSubstring(string nameSubstring) 
        {
            var customers = await Database.Customers.GetByNameSubstring(nameSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetBySurnameSubstring(string surnameSubstring) 
        {
            var customers = await Database.Customers.GetBySurnameSubstring(surnameSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByEmailSubstring(string emailSubstring)
        {
            var customers = await Database.Customers.GetByEmailSubstring(emailSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByPhoneSubstring(string phoneSubstring) 
        {
            var customers = await Database.Customers.GetByPhoneSubstring(phoneSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<CustomerDTO?> GetByIdAsync(long id)
        { 
            var customer = await Database.Customers.GetByIdAsync(id);
            var mapper = new Mapper(config);
            return mapper.Map<Customer, CustomerDTO>(customer);
        }
        public async Task<IEnumerable<CustomerDTO>> GetByQuery(CustomerQueryBLL query)
        {
            var mapper = new Mapper(CustomerQueryBLL_CustomerQueryDALMapConfig);
            var queryDAL = mapper.Map<CustomerQueryBLL, CustomerQueryDAL>(query);
            var customers = await Database.Customers.GetByQuery(queryDAL);
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> CreateAsync(CustomerDTO item) 
        {
            var mapper = new Mapper(config);
            var customer = mapper.Map<CustomerDTO, Customer>(item);
            await Database.Customers.CreateAsync(customer);
            await Database.Save();

            item.Id = customer.Id;
            return item;
        }
        public async Task<CustomerDTO> Update(CustomerDTO item) 
        {
            var mapper = new Mapper(config);
            var customer = mapper.Map<CustomerDTO, Customer>(item);
            Database.Customers.Update(customer);
            await Database.Save();

            var returnedDTO = await GetByIdAsync(customer.Id);
            return returnedDTO;
        }
        public async Task DeleteAsync(long id) 
        {
            await Database.Customers.DeleteAsync(id);
        }

    }
}
