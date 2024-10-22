using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using HyggyBackend.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Services
{
    public class OrderItemService : IOrderItemService
    {
        IUnitOfWork Database;

        MapperConfiguration OrderItem_OrderItemDTOMapConfig = new MapperConfiguration(cfg =>
        cfg.CreateMap<OrderItem, OrderItemDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("OrderId", opt => opt.MapFrom(c => c.OrderId))
        .ForMember("WareId", opt => opt.MapFrom(c => c.WareId))
        .ForMember("PriceHistoryId", opt => opt.MapFrom(c => c.PriceHistoryId))
        .ForMember("Count", opt => opt.MapFrom(c => c.Count))
        );

        MapperConfiguration OrderItemQueryBLL_OrderItemQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemQueryBLL, OrderItemQueryDAL>());

        public OrderItemService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OrderItemDTO?> GetById(long id)
        {
            var mapper = new Mapper(OrderItem_OrderItemDTOMapConfig);
            var orderItem = await Database.OrderItems.GetById(id);
            if (orderItem == null)
            {
                return null;
            }
            return mapper.Map<OrderItem, OrderItemDTO>(orderItem);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByOrderId(long orderId)
        {
            IMapper mapper = new Mapper(OrderItem_OrderItemDTOMapConfig);
            return mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByOrderId(orderId));
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByWareId(long wareId)
        {
            IMapper mapper = new Mapper(OrderItem_OrderItemDTOMapConfig);
            return mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByWareId(wareId));
        }
        public async Task<IEnumerable<OrderItemDTO>> GetByPriceHistoryId(long priceHistoryId)
        {
            IMapper mapper = new Mapper(OrderItem_OrderItemDTOMapConfig);
            return mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(await Database.OrderItems.GetByPriceHistoryId(priceHistoryId));
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByCount(int count)
        {
            var mapper = new Mapper(OrderItem_OrderItemDTOMapConfig);
            var orderItems = await Database.OrderItems.GetByCount(count);
            return mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems);
        }

        public async Task<OrderItemDTO> Create(OrderItemDTO orderItemDTO)
        {
            var existingId = await Database.OrderItems.GetById(orderItemDTO.Id);
            if (existingId != null)
            {
                throw new ValidationException($"Такий ID вже зайнято! (Id : {existingId.Id.ToString()})", "");
            }
            if (orderItemDTO.OrderId != null)
            {
                var existingOrderId = await Database.Orders.GetById((long)orderItemDTO.OrderId);
                if (existingOrderId == null)
                {
                    throw new ValidationException($"Такий OrderId не знайдено : {orderItemDTO.OrderId.ToString()})", "");
                }
            }
            if (orderItemDTO.WareId != null)
            {
                var existingWareId = await Database.Wares.GetById((long)orderItemDTO.WareId);
                if (existingWareId == null)
                {
                    throw new ValidationException($"Такий WareId не знайдено : {orderItemDTO.WareId.ToString()})", "");
                }
            }
            if (orderItemDTO.PriceHistoryId != null)
            {
                var existingPriceHistoryId = await Database.WarePriceHistories.GetById((long)orderItemDTO.PriceHistoryId);
                if (existingPriceHistoryId == null)
                {
                    throw new ValidationException($"Такий PriceHistoryId не знайдено : {orderItemDTO.PriceHistoryId.ToString()})", "");
                }
            }

            var orderItemDAL = new OrderItem
            {
                Id = 0,
                OrderId = orderItemDTO.OrderId,
                WareId = orderItemDTO.WareId,
                PriceHistoryId = orderItemDTO.PriceHistoryId,
                Count = orderItemDTO.Count.Value
            };
            await Database.OrderItems.Create(orderItemDAL);
            await Database.Save();
            orderItemDTO.Id = orderItemDAL.Id;
            return orderItemDTO;
        }
        public async Task<OrderItemDTO> Update(OrderItemDTO orderItemDTO)
        {
            var existingId = await Database.OrderItems.GetById(orderItemDTO.Id);
            if (existingId == null)
            {
                throw new ValidationException("Такий ID не знайдено!", orderItemDTO.Id.ToString());
            }
            if (orderItemDTO.OrderId != null)
            {
                var existingOrderId = await Database.Orders.GetById((long)orderItemDTO.OrderId);
                if (existingOrderId == null)
                {
                    throw new ValidationException($"Такий OrderId не знайдено : {orderItemDTO.OrderId.ToString()})", "");
                }
            }
            if (orderItemDTO.WareId != null)
            {
                var existingWareId = await Database.Orders.GetById((long)orderItemDTO.WareId);
                if (existingWareId == null)
                {
                    throw new ValidationException($"Такий WareId не знайдено : {orderItemDTO.WareId.ToString()})", "");
                }
            }
            if (orderItemDTO.PriceHistoryId != null)
            {
                var existingPriceHistoryId = await Database.Orders.GetById((long)orderItemDTO.PriceHistoryId);
                if (existingPriceHistoryId == null)
                {
                    throw new ValidationException($"Такий PriceHistoryId не знайдено : {orderItemDTO.PriceHistoryId.ToString()})", "");
                }
            }

            var orderItemDAL = _mapper.Map<OrderItemDTO, OrderItem>(orderItemDTO);
            Database.OrderItems.Update(orderItemDAL);
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
            var mapper = new Mapper(OrderItem_OrderItemDTOMapConfig);
            await Database.OrderItems.Delete(id);
            await Database.Save();
            return mapper.Map<OrderItem, OrderItemDTO>(existedId);
        }
    }
}
