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
        public async Task<IEnumerable<OrderItem>> GetByPriceHistoryId(long priceHistoryId)
        {
            return await _context.OrderItems.Where(x => x.PriceHistoryId == priceHistoryId).ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetByCount(int count)
        {
            return await _context.OrderItems.Where(x => x.Count == count).ToListAsync();
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
            if (query.PriceHistoryId != null)
            {
                orderItemCollections.Add(await GetByWareId(query.PriceHistoryId.Value));
            }
            if (query.Count != null)
            {
                orderItemCollections.Add(await GetByCount(query.Count.Value));
            }

            if (!orderItemCollections.Any())
            {
                return new List<OrderItem>();
            }

            return orderItemCollections.Aggregate((prev, next) => prev.Intersect(next).ToList());

        }

        public async IAsyncEnumerable<OrderItem> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var orderItem = await GetById(id);  // Виклик методу репозиторію
                if (orderItem != null)
                {
                    yield return orderItem;
                }
            }
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
