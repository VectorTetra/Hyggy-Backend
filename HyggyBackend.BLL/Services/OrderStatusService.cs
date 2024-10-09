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
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;

        public OrderStatusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderStatusDTO?> GetById(long id)
        {
            var orderStatus = await Database.OrderStatuses.GetById(id);
            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }

        public async Task<IEnumerable<OrderStatusDTO>> GetByNameSubstring(string nameSubstring)
        {
            var orderStatuses = await Database.OrderStatuses.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(orderStatuses);
        }

        public async Task<IEnumerable<OrderStatusDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var orderStatuses = await Database.OrderStatuses.GetByDescriptionSubstring(descriptionSubstring);
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(orderStatuses);
        }

        public async Task<IEnumerable<OrderStatusDTO>> GetByQuery(OrderStatusQueryBLL query)
        {
            var queryDAL = _mapper.Map<OrderStatusQueryDAL>(query);
            var orderStatuses = await Database.OrderStatuses.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(orderStatuses);
        }

        public async Task<OrderStatusDTO> Create(OrderStatusDTO orderStatusDTO)
        {
            if(orderStatusDTO.Name == null)
            {
                throw new ValidationException("Не вказано OrderStatus.Name", "");
            }
            var existingStatusName = await Database.OrderStatuses.GetByNameSubstring(orderStatusDTO.Name);
            if(existingStatusName.Any(x=> x.Name == orderStatusDTO.Name))
            {
                throw new ValidationException("OrderStatus з такою назвою вже існує", "");
            }
            if(orderStatusDTO.Description == null)
            {
                throw new ValidationException("Не вказано OrderStatus.Description", "");
            }

            var orderStatusDAL = new OrderStatus
            {
                Name = orderStatusDTO.Name,
                Description = orderStatusDTO.Description,
                Orders = new List<Order>()
            };
            await Database.OrderStatuses.Create(orderStatusDAL);
            await Database.Save();
            return _mapper.Map<OrderStatusDTO>(orderStatusDAL);
        }

        public async Task<OrderStatusDTO> Update(OrderStatusDTO orderStatusDTO)
        {
            var existingOrderStatus = await Database.OrderStatuses.GetById(orderStatusDTO.Id);
            if (existingOrderStatus == null)
            {
                throw new ValidationException("OrderStatus не знайдено", "");
            }
            if (orderStatusDTO.Name == null)
            {
                throw new ValidationException("Не вказано OrderStatus.Name", "");
            }
            var existingStatusName = await Database.OrderStatuses.GetByNameSubstring(orderStatusDTO.Name);
            if (existingStatusName.Any(x => x.Name == orderStatusDTO.Name && x.Id != orderStatusDTO.Id))
            {
                throw new ValidationException("Інший OrderStatus з такою назвою вже існує", "");
            }
            if (orderStatusDTO.Description == null)
            {
                throw new ValidationException("Не вказано OrderStatus.Description", "");
            }
            existingOrderStatus.Name = orderStatusDTO.Name;
            existingOrderStatus.Description = orderStatusDTO.Description;

            existingOrderStatus.Orders.Clear();
            await foreach (var order in Database.Orders.GetByIdsAsync(orderStatusDTO.OrderIds))
            {
                if (order == null)
                {
                    throw new ValidationException("Один з Order не знайдено!", "");
                }
                existingOrderStatus.Orders.Add(order);
            }
            Database.OrderStatuses.Update(existingOrderStatus);
            await Database.Save();
            return _mapper.Map<OrderStatusDTO>(existingOrderStatus);
        }

        public async Task<OrderStatusDTO> Delete(long id)
        {
            var orderStatus = await Database.OrderStatuses.GetById(id);
            if (orderStatus == null)
            {
                throw new ValidationException("OrderStatus не знайдено", "");
            }

            await Database.OrderStatuses.Delete(id);
            await Database.Save();
            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }
    }
}
