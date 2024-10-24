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
        IMapper _mapper;
        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<OrderDTO?> GetById(long id)
        {
            var order = await Database.Orders.GetById(id);
            if (order == null)
            {
                return null;
            }
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetByStringIds(string stringIds)
        {
            var orders = await Database.Orders.GetByStringIds(stringIds);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetPagedOrders(int pageNumber, int pageSize)
        {
            var orders = await Database.Orders.GetPagedOrders(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByAddressId(long addressId)
        {
            var orders = await Database.Orders.GetByAddressId(addressId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStreet(string streetSubstring)
        {
            var orders = await Database.Orders.GetByStreet(streetSubstring);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByHouseNumber(string houseNumber)
        {
            var orders = await Database.Orders.GetByHouseNumber(houseNumber);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByCity(string city)
        {
            var orders = await Database.Orders.GetByCity(city);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByPostalCode(string postalCode)
        {
            var orders = await Database.Orders.GetByPostalCode(postalCode);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByState(string state)
        {
            var orders = await Database.Orders.GetByState(state);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByLatitudeAndLongitude(double latitude, double longitude)
        {
            var orders = await Database.Orders.GetByLatitudeAndLongitude(latitude, longitude);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByOrderDateRange(DateTime minOrderDate, DateTime maxOrderDate)
        {
            var orders = await Database.Orders.GetByOrderDateRange(minOrderDate, maxOrderDate);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByPhoneSubstring(string phoneSubstring)
        {
            var orders = await Database.Orders.GetByPhoneSubstring(phoneSubstring);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByCommentSubstring(string commentSubstring)
        {
            var orders = await Database.Orders.GetByCommentSubstring(commentSubstring);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStatusId(long statusId)
        {
            var orders = await Database.Orders.GetByStatusId(statusId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            var orders = await Database.Orders.GetByStatusNameSubstring(statusNameSubstring);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            var orders = await Database.Orders.GetByStatusDescriptionSubstring(statusDescriptionSubstring);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByOrderItemId(long orderItemId)
        {
            var orders = await Database.Orders.GetByOrderItemId(orderItemId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByWareId(long wareId)
        {
            var orders = await Database.Orders.GetByWareId(wareId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByWarePriceHistoryId(long warePriceHistoryId)
        {
            var orders = await Database.Orders.GetByWarePriceHistoryId(warePriceHistoryId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByCustomerId(string customerId)
        {
            var orders = await Database.Orders.GetByCustomerId(customerId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByShopId(long shopId)
        {
            var orders = await Database.Orders.GetByShopId(shopId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<IEnumerable<OrderDTO>> GetByQuery(OrderQueryBLL query)
        {
            var queryDAL = _mapper.Map<OrderQueryDAL>(query);
            var orders = await Database.Orders.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
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
            if ((deliveryAddress.Longitude != null) ||
                (deliveryAddress.Latitude != null))
            {
                throw new ValidationException("В полі адреси доставки довгота і широта є обов'язковими!", "");
            }

            //// Перевірка наявності списку товарів у замовленні
            //if (orderDTO.OrderItemIds == null || !orderDTO.OrderItemIds.Any())
            //{
            //    throw new ValidationException("Замовлення повинно містити хоча б один товар!", "");
            //}

            var orderItems = new List<OrderItem>(); // -- orderItems Заповнюється в циклі
            //await foreach (var orderItemId in Database.OrderItems.GetByIdsAsync(orderDTO.OrderItemIds))
            //{
            //    if (orderItemId == null)
            //    {
            //        throw new ValidationException($"Одна з BlogCategory2 не знайдена!", "");
            //    }
            //    orderItems.Add(orderItemId);
            //}
            // Створення нового замовлення
            var orderDAL = new Order
            {
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
            if ((deliveryAddress.Longitude != null) ||
                (deliveryAddress.Latitude != null))
            {
                throw new ValidationException("В полі адреси доставки довгота і широта є обов'язковими!", "");
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

            return _mapper.Map<OrderDTO>(existingOrder);
        }
    }
}
