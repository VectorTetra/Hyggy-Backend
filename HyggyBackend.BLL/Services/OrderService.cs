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
            .ForPath(dto=>dto.Id, opt => opt.MapFrom(src => src.Id))
            .ForPath(dto => dto.DeliveryAddressId, opt => opt.MapFrom(src => src.DeliveryAddress.Id))
            .ForPath(dto => dto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForPath(dto => dto.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForPath(dto => dto.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForPath(dto => dto.StatusId, opt => opt.MapFrom(src => src.Status.Id))
            .ForPath(dto => dto.ShopId, opt => opt.MapFrom(src => src.ShopId))
            .ForPath(dto => dto.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForPath(dto => dto.OrderItemIds, opt => opt.MapFrom(src =>
                src.OrderItems.Select(oi => oi.Id)));
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
        public async Task<IEnumerable<OrderDTO>> GetByCustomerId(string customerId)
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
            if(orderDTO.StatusId == null)
            {
                throw new ValidationException($"Не вказано Id статусу замовлення! Id:{orderDTO.StatusId}", "");
            }
            var ExistedStatus = await Database.OrderStatuses.GetById(orderDTO.StatusId.Value);
            if (ExistedStatus == null)
            {
                throw new ValidationException($"Статус замовлення не знайдено! StatusId: {orderDTO.StatusId}", "" );
            }
            // Перевірка наявності ShopId
            if (orderDTO.ShopId == null)
            {
                throw new ValidationException($"Id магазину не може бути пустим! Id:{orderDTO.ShopId}", "");
            }
            var ExistedShop = await Database.Shops.GetById(orderDTO.ShopId.Value);
            if (ExistedShop == null)
            {
                throw new ValidationException($"Магазин з таким ID не знайдено! ShopId: {orderDTO.ShopId}", "");
            }
            // Перевірка наявності CustomerId
            if (orderDTO.CustomerId == null)
            {
                throw new ValidationException($"Id клієнта не може бути пустим! Id:{orderDTO.CustomerId}", "");
            }
            var ExistedCustomer = await Database.Customers.GetByIdAsync(orderDTO.CustomerId);
            if (ExistedCustomer == null)
            {
                throw new ValidationException($"Клієнт з таким ID не знайдено! CustomerId: {orderDTO.CustomerId}", "");
            }
            // Перевірка наявності дати замовлення
            if (orderDTO.OrderDate == null)
            {
                throw new ValidationException($"Дата замовлення не може бути пустою! Date:{orderDTO.OrderDate}", "");
            }
            // Перевірка наявності телефону
            if (string.IsNullOrWhiteSpace(orderDTO.Phone))
            {
                throw new ValidationException($"Телефон не може бути пустим! Phone:{orderDTO.Phone}", "");
            }

            // Перевірка наявності DeliveryAddressId
            if (orderDTO.DeliveryAddressId == null)
            {
                throw new ValidationException($"Id адреси доставки не може бути пустим! Id:{orderDTO.DeliveryAddressId}", "");
            }

            // Отримання адреси з бази даних за її Id
            var deliveryAddress = await Database.Addresses.GetByIdAsync(orderDTO.DeliveryAddressId.Value);

            // Перевірка, чи існує адреса
            if (deliveryAddress == null)
            {
                throw new ValidationException("Адреса доставки не знайдена!", "");
            }

            // Перевірка на наявність обов'язкових полів адреси
            if (string.IsNullOrWhiteSpace(deliveryAddress.Street) ||
                string.IsNullOrWhiteSpace(deliveryAddress.HouseNumber) ||
                string.IsNullOrWhiteSpace(deliveryAddress.City) ||
                string.IsNullOrWhiteSpace(deliveryAddress.State) ||
                string.IsNullOrWhiteSpace(deliveryAddress.PostalCode))
            {
                throw new ValidationException("Всі поля адреси доставки є обов'язковими!", "");
            }

            // Перевірка наявності списку товарів у замовленні
            if (orderDTO.OrderItemIds == null || !orderDTO.OrderItemIds.Any())
            {
                throw new ValidationException("Замовлення повинно містити хоча б один товар!", "");
            }

            var orderItems = new List<OrderItem>();
            await foreach (var orderItemId in Database.OrderItems.GetByIdsAsync(orderDTO.OrderItemIds))
            {
                if (orderItemId == null)
                {
                    throw new ValidationException($"Одна з BlogCategory2 не знайдена!", "");
                }
                orderItems.Add(orderItemId);
            }
            // Створення нового замовлення
            var orderDAL = new Order {
                OrderDate = orderDTO.OrderDate.Value,
                Phone = orderDTO.Phone,
                Comment = orderDTO.Comment,
                Status = ExistedStatus,
                Shop = ExistedShop,
                Customer = ExistedCustomer,
                DeliveryAddress = deliveryAddress,
                OrderItems = orderItems
            };

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
                throw new ValidationException($"Замовлення з таким ID не знайдено! Id: {orderDTO.Id}", "");
            }

            // Перевірка наявності статусу замовлення
            if (orderDTO.StatusId == null)
            {
                throw new ValidationException($"Не вказано Id статусу замовлення! Id:{orderDTO.StatusId}", "");
            }
            var ExistedStatus = await Database.OrderStatuses.GetById(orderDTO.StatusId.Value);
            if (ExistedStatus == null)
            {
                throw new ValidationException($"Статус замовлення не знайдено! StatusId: {orderDTO.StatusId}", "");
            }

            // Перевірка наявності ShopId
            if (orderDTO.ShopId == null)
            {
                throw new ValidationException($"Id магазину не може бути пустим! Id:{orderDTO.ShopId}", "");
            }
            var ExistedShop = await Database.Shops.GetById(orderDTO.ShopId.Value);
            if (ExistedShop == null)
            {
                throw new ValidationException($"Магазин з таким ID не знайдено! ShopId: {orderDTO.ShopId}", "");
            }

            // Перевірка наявності CustomerId
            if (orderDTO.CustomerId == null)
            {
                throw new ValidationException($"Id клієнта не може бути пустим! Id:{orderDTO.CustomerId}", "");
            }
            var ExistedCustomer = await Database.Customers.GetByIdAsync(orderDTO.CustomerId);
            if (ExistedCustomer == null)
            {
                throw new ValidationException($"Клієнт з таким ID не знайдено! CustomerId: {orderDTO.CustomerId}", "");
            }

            // Перевірка наявності дати замовлення
            if (orderDTO.OrderDate == null)
            {
                throw new ValidationException($"Дата замовлення не може бути пустою! Date:{orderDTO.OrderDate}", "");
            }

            // Перевірка наявності телефону
            if (string.IsNullOrWhiteSpace(orderDTO.Phone))
            {
                throw new ValidationException($"Телефон не може бути пустим! Phone:{orderDTO.Phone}", "");
            }

            // Перевірка наявності DeliveryAddressId
            if (orderDTO.DeliveryAddressId == null)
            {
                throw new ValidationException($"Id адреси доставки не може бути пустим! Id:{orderDTO.DeliveryAddressId}", "");
            }

            // Отримання адреси з бази даних за її Id
            var deliveryAddress = await Database.Addresses.GetByIdAsync(orderDTO.DeliveryAddressId.Value);

            // Перевірка, чи існує адреса
            if (deliveryAddress == null)
            {
                throw new ValidationException("Адреса доставки не знайдена!", "");
            }

            // Перевірка на наявність обов'язкових полів адреси
            if (string.IsNullOrWhiteSpace(deliveryAddress.Street) ||
                string.IsNullOrWhiteSpace(deliveryAddress.HouseNumber) ||
                string.IsNullOrWhiteSpace(deliveryAddress.City) ||
                string.IsNullOrWhiteSpace(deliveryAddress.State) ||
                string.IsNullOrWhiteSpace(deliveryAddress.PostalCode))
            {
                throw new ValidationException("Всі поля адреси доставки є обов'язковими!", "");
            }

            // Перевірка наявності списку товарів у замовленні
            if (orderDTO.OrderItemIds == null || !orderDTO.OrderItemIds.Any())
            {
                throw new ValidationException("Замовлення повинно містити хоча б один товар!", "");
            }

            // Оновлення товарів замовлення
            var orderItems = new List<OrderItem>();
            await foreach (var orderItemId in Database.OrderItems.GetByIdsAsync(orderDTO.OrderItemIds))
            {
                if (orderItemId == null)
                {
                    throw new ValidationException($"Одна з BlogCategory2 не знайдена!", "");
                }
                orderItems.Add(orderItemId);
            }

            // Оновлення замовлення з новими значеннями
            existingOrder.OrderDate = orderDTO.OrderDate.Value;
            existingOrder.Phone = orderDTO.Phone;
            existingOrder.Comment = orderDTO.Comment;
            existingOrder.Status = ExistedStatus;
            existingOrder.Shop = ExistedShop;
            existingOrder.Customer = ExistedCustomer;
            existingOrder.DeliveryAddress = deliveryAddress;
            existingOrder.OrderItems = orderItems;

            // Оновлення замовлення в базі даних
            Database.Orders.Update(existingOrder);
            await Database.Save();

            // Повернення оновленого DTO
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
