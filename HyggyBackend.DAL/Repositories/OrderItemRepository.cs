using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class OrderItemRepository: IOrderItemRepository
    {
        private readonly HyggyContext _context;

        public OrderItemRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<OrderItem?> GetById(long id)
        {
            return await _context.OrderItems.FindAsync(id);
        }
        public async Task<IEnumerable<OrderItem>> GetByOrderId(long orderId)
        {
            return await _context.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
        }
        public async Task<IEnumerable<OrderItem>> GetByWareId(long wareId)
        {
            return await _context.OrderItems.Where(x => x.WareId == wareId).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetByCount(int orderCount)
        {
            return await _context.OrderItems.Where(x => x.OrderCount == orderCount).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetByQuery(OrderItemQueryDAL query)
        {
            var orderItemCollections = new List<IEnumerable<OrderItem>>();

            if (query.Id != null)
            {
                orderItemCollections.Add(new List<OrderItem> { await GetById(query.Id.Value) });
            }
            if (query.OrderId != null)
            {
                orderItemCollections.Add(await GetByOrderId(query.OrderId.Value));
            }
            if (query.WareId != null)
            {
                orderItemCollections.Add(await GetByWareId(query.WareId.Value));
            }
            if (query.OrderCount != null)
            {
                orderItemCollections.Add(await GetByCount(query.OrderCount.Value));
            }

            if (!orderItemCollections.Any())
            {
                return new List<OrderItem>();
            }

            return orderItemCollections.Aggregate((prev, next) => prev.Intersect(next).ToList());

        }
        public async Task Create(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }
        public void Update(OrderItem orderItem)
        {
            _context.Entry(orderItem).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var orderItem = await GetById(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
            }
        }
    }
}
