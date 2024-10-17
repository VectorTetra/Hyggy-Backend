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
    public class OrderItemService : IOrderItemService
    {
        IUnitOfWork Database;
        private IMapper _mapper;

        public OrderItemService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<OrderItemDTO?> GetById(long id)
        {

            var orderItem = await Database.OrderItems.GetById(id);
            if (orderItem == null)
            {
                return null;
            }
            return _mapper.Map<OrderItem, OrderItemDTO>(orderItem);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByStringIds(string stringIds)
        {
            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByStringIds(stringIds));
        }

        public async Task<IEnumerable<OrderItemDTO>> GetPaged(int pageNumber, int pageSize)
        {

            var orderItems = await Database.OrderItems.GetPaged(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByQuery(OrderItemQueryBLL query)
        {
            var orderItems = await Database.OrderItems.GetByQuery(_mapper.Map<OrderItemQueryBLL, OrderItemQueryDAL>(query));
            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByOrderId(long orderId)
        {

            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByOrderId(orderId));
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByWareId(long wareId)
        {

            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByWareId(wareId));
        }
        public async Task<IEnumerable<OrderItemDTO>> GetByPriceHistoryId(long priceHistoryId)
        {

            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByPriceHistoryId(priceHistoryId));
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByCount(int count)
        {

            var orderItems = await Database.OrderItems.GetByCount(count);
            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<OrderItemDTO> Create(OrderItemDTO orderItemDTO)
        {
            var existingId = await Database.OrderItems.GetById(orderItemDTO.Id);
            if (existingId != null)
            {
                throw new ValidationException($"Такий ID вже зайнято! (Id : {existingId.Id.ToString()})", "");
            }

            if (orderItemDTO.OrderId == null)
            {
                throw new ValidationException("OrderId не може бути пустим!", "");
            }

            var existingOrderId = await Database.Orders.GetById((long)orderItemDTO.OrderId);
            if (existingOrderId == null)
            {
                throw new ValidationException($"Такий OrderId не знайдено : {orderItemDTO.OrderId.ToString()})", "");
            }

            if (orderItemDTO.WareId == null)
            {
                throw new ValidationException("WareId не може бути пустим!", "");
            }
            var existingWareId = await Database.Wares.GetById((long)orderItemDTO.WareId);
            if (existingWareId == null)
            {
                throw new ValidationException($"Такий WareId не знайдено : {orderItemDTO.WareId.ToString()})", "");
            }

            if (orderItemDTO.PriceHistoryId == null)
            {
                throw new ValidationException("PriceHistoryId не може бути пустим!", "");
            }

            var existingPriceHistoryId = await Database.WarePriceHistories.GetById((long)orderItemDTO.PriceHistoryId);
            if (existingPriceHistoryId == null)
            {
                throw new ValidationException($"Такий PriceHistoryId не знайдено : {orderItemDTO.PriceHistoryId.ToString()})", "");
            }

            if (orderItemDTO.Count == null)
            {
                throw new ValidationException("Count не може бути пустим!", "");
            }

            if (orderItemDTO.Count < 1)
            {
                throw new ValidationException("Count не може бути менше 1!", "");
            }

            var orderItemDAL = new OrderItem
            {
                OrderId = orderItemDTO.OrderId.Value,
                WareId = orderItemDTO.WareId.Value,
                PriceHistoryId = orderItemDTO.PriceHistoryId.Value,
                Count = orderItemDTO.Count.Value
            };
            await Database.OrderItems.Create(orderItemDAL);
            await Database.Save();
            orderItemDTO.Id = orderItemDAL.Id;
            return orderItemDTO;
        }
        public async Task<OrderItemDTO> Update(OrderItemDTO orderItemDTO)
        {
            var existingOrderItem = await Database.OrderItems.GetById(orderItemDTO.Id);
            if (existingOrderItem == null)
            {
                throw new ValidationException("Такий ID не знайдено!", orderItemDTO.Id.ToString());
            }

            if (orderItemDTO.OrderId == null)
            {
                throw new ValidationException("OrderId не може бути пустим!", "");
            }

            var existingOrderId = await Database.Orders.GetById((long)orderItemDTO.OrderId);
            if (existingOrderId == null)
            {
                throw new ValidationException($"Такий OrderId не знайдено : {orderItemDTO.OrderId.ToString()})", "");
            }

            if (orderItemDTO.WareId == null)
            {
                throw new ValidationException("WareId не може бути пустим!", "");
            }

            var existingWareId = await Database.Orders.GetById((long)orderItemDTO.WareId);
            if (existingWareId == null)
            {
                throw new ValidationException($"Такий WareId не знайдено : {orderItemDTO.WareId.ToString()})", "");
            }

            if (orderItemDTO.PriceHistoryId == null)
            {
                throw new ValidationException("PriceHistoryId не може бути пустим!", "");
            }

            var existingPriceHistoryId = await Database.Orders.GetById((long)orderItemDTO.PriceHistoryId);
            if (existingPriceHistoryId == null)
            {
                throw new ValidationException($"Такий PriceHistoryId не знайдено : {orderItemDTO.PriceHistoryId.ToString()})", "");
            }

            if (orderItemDTO.Count == null)
            {
                throw new ValidationException("Count не може бути пустим!", "");
            }

            if (orderItemDTO.Count < 1)
            {
                throw new ValidationException("Count не може бути менше 1!", "");
            }

            existingOrderItem.OrderId = orderItemDTO.OrderId.Value;
            existingOrderItem.WareId = orderItemDTO.WareId.Value;
            existingOrderItem.PriceHistoryId = orderItemDTO.PriceHistoryId.Value;
            existingOrderItem.Count = orderItemDTO.Count.Value;
            Database.OrderItems.Update(existingOrderItem);
            await Database.Save();
            return orderItemDTO;
        }

        public async Task<OrderItemDTO> Delete(long id)
        {
            var existedId = await Database.OrderItems.GetById(id);
            if (existedId == null)
            {
                throw new ValidationException("Такий ID не існує!", id.ToString());
            }

            await Database.OrderItems.Delete(id);
            await Database.Save();
            return _mapper.Map<OrderItem, OrderItemDTO>(existedId);
        }
    }
}
