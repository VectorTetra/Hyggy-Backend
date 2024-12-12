using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class OrderDeliveryTypeService : IOrderDeliveryTypeService
    {
        IUnitOfWork Database;
        IMapper _mapper;
        public OrderDeliveryTypeService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<OrderDeliveryTypeDTO?> GetById(long id)
        {
            var orderDeliveryType = await Database.OrderDeliveryTypes.GetById(id);
            if (orderDeliveryType == null)
            {
                return null;
            }
            return _mapper.Map<OrderDeliveryTypeDTO>(orderDeliveryType);
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByStringIds(string stringIds)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByStringIds(stringIds));
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByOrderId(long orderId)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByOrderId(orderId));
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByName(string nameSubstring)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByName(nameSubstring));
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByDescription(string descriptionSubstring)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByDescription(descriptionSubstring));
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByPriceRange(float minPrice, float maxPrice)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByPriceRange(minPrice, maxPrice));
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByDeliveryTimeInDaysRange(int minDeliveryTimeInDays, int maxDeliveryTimeInDays)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByDeliveryTimeInDaysRange(minDeliveryTimeInDays, maxDeliveryTimeInDays));
        }
        public async Task<IEnumerable<OrderDeliveryTypeDTO>> GetByQuery(OrderDeliveryTypeQueryBLL query)
        {
            return _mapper.Map<IEnumerable<OrderDeliveryTypeDTO>>(await Database.OrderDeliveryTypes.GetByQuery(_mapper.Map<OrderDeliveryTypeQueryBLL, OrderDeliveryTypeQueryDAL>(query)));
        }
        public async Task<OrderDeliveryTypeDTO> Create(OrderDeliveryTypeDTO orderDeliveryType) 
        {
            if (string.IsNullOrEmpty(orderDeliveryType.Name))
            {
                throw new ValidationException($"Не вказано назву для типу доставки замовлення! orderDeliveryType.Name:{orderDeliveryType.Name}", "");
            }
            var ExistingName = await Database.OrderDeliveryTypes.GetByName(orderDeliveryType.Name);
            if (ExistingName == null)
            {
                throw new ValidationException($"Тип доставки замовлення з такою назвою вже існує! orderDeliveryType.Name:{orderDeliveryType.Name}", "");
            }

            if (orderDeliveryType.Price == null)
            {
                throw new ValidationException($"Не вказано ціну для типу доставки замовлення! orderDeliveryType.Price:{orderDeliveryType.Price}", "");
            }

            if (orderDeliveryType.MinDeliveryTimeInDays == null)
            {
                throw new ValidationException($"Не вказано мінімальний час доставки для типу доставки замовлення! orderDeliveryType.MinDeliveryTimeInDays:{orderDeliveryType.MinDeliveryTimeInDays}", "");
            }

            if (orderDeliveryType.MaxDeliveryTimeInDays == null)
            {
                throw new ValidationException($"Не вказано максимальний час доставки для типу доставки замовлення! orderDeliveryType.MaxDeliveryTimeInDays:{orderDeliveryType.MaxDeliveryTimeInDays}", "");
            }

            var newOrderDeliveryType = new OrderDeliveryType
            {
                Name = orderDeliveryType.Name,
                Description = orderDeliveryType.Description ?? "",
                Price = orderDeliveryType.Price.Value,
                MinDeliveryTimeInDays = orderDeliveryType.MinDeliveryTimeInDays.Value,
                MaxDeliveryTimeInDays = orderDeliveryType.MaxDeliveryTimeInDays.Value,
                Orders = new List<Order>()
            };

            await Database.OrderDeliveryTypes.Create(newOrderDeliveryType);
            await Database.Save();

            return _mapper.Map<OrderDeliveryTypeDTO>(newOrderDeliveryType);

        }
        public async Task<OrderDeliveryTypeDTO> Update(OrderDeliveryTypeDTO orderDeliveryType) 
        {
            var existingOrderDeliveryType = await Database.OrderDeliveryTypes.GetById(orderDeliveryType.Id);
            if (existingOrderDeliveryType == null)
            {
                throw new ValidationException($"Тип доставки замовлення не знайдено! orderDeliveryType.Id:{orderDeliveryType.Id}", "");
            }
            if (string.IsNullOrEmpty(orderDeliveryType.Name))
            {
                throw new ValidationException($"Не вказано назву для типу доставки замовлення! orderDeliveryType.Name:{orderDeliveryType.Name}", "");
            }
            var ExistingName = await Database.OrderDeliveryTypes.GetByName(orderDeliveryType.Name);
            if (ExistingName == null)
            {
                throw new ValidationException($"Тип доставки замовлення з такою назвою вже існує! orderDeliveryType.Name:{orderDeliveryType.Name}", "");
            }
            if (orderDeliveryType.Price == null)
            {
                throw new ValidationException($"Не вказано ціну для типу доставки замовлення! orderDeliveryType.Price:{orderDeliveryType.Price}", "");
            }
            if (orderDeliveryType.MinDeliveryTimeInDays == null)
            {
                throw new ValidationException($"Не вказано мінімальний час доставки для типу доставки замовлення! orderDeliveryType.MinDeliveryTimeInDays:{orderDeliveryType.MinDeliveryTimeInDays}", "");
            }
            if (orderDeliveryType.MaxDeliveryTimeInDays == null)
            {
                throw new ValidationException($"Не вказано максимальний час доставки для типу доставки замовлення! orderDeliveryType.MaxDeliveryTimeInDays:{orderDeliveryType.MaxDeliveryTimeInDays}", "");
            }
            // Оновлення товарів замовлення
            var orders = new List<Order>();
            if (orderDeliveryType.OrderIds != null)
            {
                if (!orderDeliveryType.OrderIds.Any())
                {
                    existingOrderDeliveryType.Orders.Clear();  // Очищаємо колекцію, якщо масив порожній
                }
                else
                {
                    existingOrderDeliveryType.Orders.Clear();
                    await foreach (var orderEntity in Database.Orders.GetByIdsAsync(orderDeliveryType.OrderIds))
                    {
                        if (orderEntity == null)
                        {
                            throw new ValidationException($"Замовлення з id:{orderEntity.Id} не знайдено", "");
                        }
                        orders.Add(orderEntity);  // Додаємо нові замовлення
                    }
                }
            }
            existingOrderDeliveryType.Name = orderDeliveryType.Name;
            existingOrderDeliveryType.Description = orderDeliveryType.Description ?? "";
            existingOrderDeliveryType.Price = orderDeliveryType.Price.Value;
            existingOrderDeliveryType.MinDeliveryTimeInDays = orderDeliveryType.MinDeliveryTimeInDays.Value;
            existingOrderDeliveryType.MaxDeliveryTimeInDays = orderDeliveryType.MaxDeliveryTimeInDays.Value;



            Database.OrderDeliveryTypes.Update(existingOrderDeliveryType);
            await Database.Save();
            return _mapper.Map<OrderDeliveryTypeDTO>(existingOrderDeliveryType);
        }
        public async Task<OrderDeliveryTypeDTO> Delete(long id) 
        {
            var existedId = await Database.OrderDeliveryTypes.GetById(id);
            if (existedId == null)
            {
                throw new ValidationException($"Тип доставки замовлення з id:{id} не знайдено", id.ToString());
            }
            await Database.OrderDeliveryTypes.Delete(id);
            await Database.Save();
            return _mapper.Map<OrderDeliveryTypeDTO>(existedId);
        }
    }
}
