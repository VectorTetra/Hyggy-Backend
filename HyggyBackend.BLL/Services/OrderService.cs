using HyggyBackend.BLL.Interfaces;
using AutoMapper;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using HyggyBackend.BLL.Infrastructure;

namespace HyggyBackend.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database;
        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Order, OrderDTO>()
            .ForMember("Id", opt => opt.MapFrom(src => src.Id))
            .ForPath(dto => dto.DeliveryAddress, opt => opt.MapFrom(src =>
                new AddressDTO
                {
                    Id = src.DeliveryAddress.Id,
                    City = src.DeliveryAddress.City,
                    Street = src.DeliveryAddress.Street,
                    HouseNumber = src.DeliveryAddress.HouseNumber,
                    State = src.DeliveryAddress.State,
                    PostalCode = src.DeliveryAddress.PostalCode,
                    Latitude = src.DeliveryAddress.Latitude,
                    Longitude = src.DeliveryAddress.Longitude,
                    Storage = src.DeliveryAddress.Storage == null ? null : new StorageDTO
                    {
                        Id = src.DeliveryAddress.Storage.Id,
                        Shop = src.DeliveryAddress.Storage.Shop == null ? null : new ShopDTO
                        {
                            Id = src.DeliveryAddress.Storage.Shop.Id,
                            PhotoUrl = src.DeliveryAddress.Storage.Shop.PhotoUrl,
                            WorkHours = src.DeliveryAddress.Storage.Shop.WorkHours
                        }
                    }
                }))
            .ForMember("OrderDate", opt => opt.MapFrom(src => src.OrderDate))
            .ForMember("Phone", opt => opt.MapFrom(src => src.Phone))
            .ForMember("Comment", opt => opt.MapFrom(src => src.Comment))
            .ForPath(dto => dto.Status, opt => opt.MapFrom(src =>
                new OrderStatusDTO
                {
                    Id = src.Status.Id,
                    Name = src.Status.Name,
                    Description = src.Status.Description
                }))
            .ForPath(dto => dto.OrderItems, opt => opt.MapFrom(src =>
                src.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    Count = oi.Count,
                    Ware = new WareDTO
                    {
                        Id = oi.Ware.Id,
                        Name = oi.Ware.Name,
                        Description = oi.Ware.Description,
                        Price = oi.Ware.Price,
                        Discount = oi.Ware.Discount,
                        IsDeliveryAvailable = oi.Ware.IsDeliveryAvailable,
                        Category3 = new WareCategory3DTO
                        {
                            Id = oi.Ware.WareCategory3.Id,
                            Name = oi.Ware.WareCategory3.Name,
                            WareCategory2 = new WareCategory2DTO
                            {
                                Id = oi.Ware.WareCategory3.WareCategory2.Id,
                                Name = oi.Ware.WareCategory3.WareCategory2.Name,
                                WareCategory1 = new WareCategory1DTO
                                {
                                    Id = oi.Ware.WareCategory3.WareCategory2.WareCategory1.Id,
                                    Name = oi.Ware.WareCategory3.WareCategory2.WareCategory1.Name
                                }
                            }
                        },
                        Status = new WareStatusDTO
                        {
                            Id = oi.Ware.Status.Id,
                            Name = oi.Ware.Status.Name,
                            Description = oi.Ware.Status.Description
                        },
                        Images = oi.Ware.Images.Select(i => new WareImageDTO
                        {
                            Id = i.Id,
                            Path = i.Path
                        }).ToList()
                    },
                    PriceHistory = new WarePriceHistoryDTO
                    {
                        Id = oi.PriceHistory.Id,
                        Price = oi.PriceHistory.Price,
                        EffectiveDate = oi.PriceHistory.EffectiveDate
                    }
                }).ToList()));


            cfg.CreateMap<OrderDTO, Order>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForPath(dest => dest.DeliveryAddress, opt => opt.MapFrom(src =>
            new Address
            {
                Id = src.DeliveryAddress.Id,
                City = src.DeliveryAddress.City,
                Street = src.DeliveryAddress.Street,
                HouseNumber = src.DeliveryAddress.HouseNumber,
                State = src.DeliveryAddress.State,
                PostalCode = src.DeliveryAddress.PostalCode,
                Latitude = src.DeliveryAddress.Latitude,
                Longitude = src.DeliveryAddress.Longitude,
                Storage = src.DeliveryAddress.Storage == null ? null : new Storage
                {
                    Id = src.DeliveryAddress.Storage.Id,
                    Shop = src.DeliveryAddress.Storage.Shop == null ? null : new Shop
                    {
                        Id = src.DeliveryAddress.Storage.Shop.Id,
                        PhotoUrl = src.DeliveryAddress.Storage.Shop.PhotoUrl,
                        WorkHours = src.DeliveryAddress.Storage.Shop.WorkHours
                    }
                }
            }))
        .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
        .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
        .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
        .ForPath(dest => dest.Status, opt => opt.MapFrom(src =>
            new OrderStatus
            {
                Id = src.Status.Id,
                Name = src.Status.Name,
                Description = src.Status.Description
            }))
        .ForPath(dest => dest.OrderItems, opt => opt.MapFrom(src =>
            src.OrderItems.Select(dto => new OrderItem
            {
                Id = dto.Id,
                Count = dto.Count,
                Ware = new Ware
                {
                    Id = dto.Ware.Id,
                    Name = dto.Ware.Name,
                    Description = dto.Ware.Description,
                    Price = dto.Ware.Price,
                    Discount = dto.Ware.Discount,
                    IsDeliveryAvailable = dto.Ware.IsDeliveryAvailable,
                    WareCategory3 = new WareCategory3
                    {
                        Id = dto.Ware.Category3.Id,
                        Name = dto.Ware.Category3.Name,
                        WareCategory2 = new WareCategory2
                        {
                            Id = dto.Ware.Category3.WareCategory2.Id,
                            Name = dto.Ware.Category3.WareCategory2.Name,
                            WareCategory1 = new WareCategory1
                            {
                                Id = dto.Ware.Category3.WareCategory2.WareCategory1.Id,
                                Name = dto.Ware.Category3.WareCategory2.WareCategory1.Name
                            }
                        }
                    },
                    Status = new WareStatus
                    {
                        Id = dto.Ware.Status.Id,
                        Name = dto.Ware.Status.Name,
                        Description = dto.Ware.Status.Description
                    },
                    Images = dto.Ware.Images.Select(i => new WareImage
                    {
                        Id = i.Id,
                        Path = i.Path
                    }).ToList()
                },
                PriceHistory = new WarePriceHistory
                {
                    Id = dto.PriceHistory.Id,
                    Price = dto.PriceHistory.Price.Value,
                    EffectiveDate = dto.PriceHistory.EffectiveDate.Value
                }
            }).ToList()));

        });

        MapperConfiguration OrderQueryBLL_OrderQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<OrderQueryBLL, OrderQueryDAL>());

        public async Task<OrderDTO?> GetById(long id)
        {
            var order = await Database.Orders.GetById(id);
            if (order == null)
            {
                return null;
            }
            var mapper = new Mapper(config);
            return mapper.Map<OrderDTO>(order);
        }
        public async Task<IEnumerable<OrderDTO>> GetByAddressId(long addressId)
        {
            var orders = await Database.Orders.GetByAddressId(addressId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStreet(string streetSubstring)
        {
            var orders = await Database.Orders.GetByStreet(streetSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByHouseNumber(string houseNumber)
        {
            var orders = await Database.Orders.GetByHouseNumber(houseNumber);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByCity(string city)
        {
            var orders = await Database.Orders.GetByCity(city);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByPostalCode(string postalCode)
        {
            var orders = await Database.Orders.GetByPostalCode(postalCode);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByState(string state)
        {
            var orders = await Database.Orders.GetByState(state);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByLatitudeAndLongitude(double latitude, double longitude)
        {
            var orders = await Database.Orders.GetByLatitudeAndLongitude(latitude, longitude);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByOrderDateRange(DateTime minOrderDate, DateTime maxOrderDate)
        {
            var orders = await Database.Orders.GetByOrderDateRange(minOrderDate, maxOrderDate);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByPhoneSubstring(string phoneSubstring)
        {
            var orders = await Database.Orders.GetByPhoneSubstring(phoneSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByCommentSubstring(string commentSubstring)
        {
            var orders = await Database.Orders.GetByCommentSubstring(commentSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStatusId(long statusId)
        {
            var orders = await Database.Orders.GetByStatusId(statusId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            var orders = await Database.Orders.GetByStatusNameSubstring(statusNameSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            var orders = await Database.Orders.GetByStatusDescriptionSubstring(statusDescriptionSubstring);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByOrderItemId(long orderItemId)
        {
            var orders = await Database.Orders.GetByOrderItemId(orderItemId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByWareId(long wareId)
        {
            var orders = await Database.Orders.GetByWareId(wareId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByWarePriceHistoryId(long warePriceHistoryId)
        {
            var orders = await Database.Orders.GetByWarePriceHistoryId(warePriceHistoryId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByCustomerId(long customerId)
        {
            var orders = await Database.Orders.GetByCustomerId(customerId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByShopId(long shopId)
        {
            var orders = await Database.Orders.GetByShopId(shopId);
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByQuery(OrderQueryBLL query)
        {
            var mapper = new Mapper(OrderQueryBLL_OrderQueryDALMapConfig);
            var queryDAL = mapper.Map<OrderQueryDAL>(query);
            var orders = await Database.Orders.GetByQuery(queryDAL);
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
            // Перевірка наявності статусу замовлення
            if (orderDTO.Status == null)
            {
                throw new ValidationException("Статус замовлення не може бути пустим!", orderDTO.Status.ToString());
            }

            // Перевірка наявності адреси доставки
            if (orderDTO.DeliveryAddress == null)
            {
                throw new ValidationException("Адреса доставки не може бути пустою!","");
            }

            // Перевірка на наявність обов'язкових полів адреси
            if (string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.Street) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.HouseNumber) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.City) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.State) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.PostalCode))
            {
                throw new ValidationException("Всі поля адреси доставки є обов'язковими!","");
            }

            // Перевірка наявності списку товарів у замовленні
            if (orderDTO.OrderItems == null || !orderDTO.OrderItems.Any())
            {
                throw new ValidationException("Замовлення повинно містити хоча б один товар!", "");
            }

            // Створення маппера
            var mapper = new Mapper(config);
            var orderDAL = mapper.Map<OrderDTO, Order>(orderDTO);

            // Створення замовлення
            await Database.Orders.Create(orderDAL);
            await Database.Save();

            // Присвоєння ID новоствореного замовлення DTO
            orderDTO.Id = orderDAL.Id;
            return orderDTO;
        }

        public async Task<OrderDTO> Update(OrderDTO orderDTO)
        {
            // Перевірка, чи існує замовлення в базі
            var existingOrder = await Database.Orders.GetById(orderDTO.Id);
            if (existingOrder == null)
            {
                throw new ValidationException("Замовлення з таким ID не знайдено!", orderDTO.Id.ToString());
            }

            // Перевірка наявності статусу замовлення
            if (orderDTO.Status == null)
            {
                throw new ValidationException("Статус замовлення не може бути пустим!", orderDTO.Status.ToString());
            }

            // Перевірка на наявність обов'язкових полів адреси
            if (orderDTO.DeliveryAddress == null ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.Street) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.HouseNumber) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.City) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.State) ||
                string.IsNullOrWhiteSpace(orderDTO.DeliveryAddress.PostalCode))
            {
                throw new ValidationException("Всі поля адреси доставки є обов'язковими!", "");
            }

            // Перевірка наявності списку товарів у замовленні
            if (orderDTO.OrderItems == null || !orderDTO.OrderItems.Any())
            {
                throw new ValidationException("Замовлення повинно містити хоча б один товар!","");
            }

            // Маппінг нових значень на існуюче замовлення
            var mapper = new Mapper(config);
            var updatedOrder = mapper.Map(orderDTO, existingOrder);

            // Оновлення замовлення в базі
            Database.Orders.Update(updatedOrder);
            await Database.Save();

            var returnedDTO = await GetById(orderDTO.Id);

            return returnedDTO;
        }

        public async Task<OrderDTO> Delete(long id)
        {
            // Перевірка, чи існує замовлення в базі
            var existingOrder = await Database.Orders.GetById(id);
            if (existingOrder == null)
            {
                throw new ValidationException("Замовлення з таким ID не знайдено!", id.ToString());
            }

            // Видалення замовлення з бази
            await Database.Orders.Delete(id);
            await Database.Save();

            // Маппінг видаленого замовлення на DTO
            var mapper = new Mapper(config);
            return mapper.Map<OrderDTO>(existingOrder);
        }
    }
}
