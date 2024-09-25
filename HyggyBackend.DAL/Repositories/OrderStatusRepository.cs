using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly HyggyContext _context;

        public OrderStatusRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<OrderStatus?> GetById(long id)
        {
            return await _context.OrderStatuses.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.OrderStatuses.Where(os => os.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<OrderStatus>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.OrderStatuses.Where(os => os.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<OrderStatus>> GetByQuery(OrderStatusQueryDAL query)
        {
            var orderStatuses = _context.OrderStatuses.AsQueryable();

            if (query.Id.HasValue)
            {
                orderStatuses = orderStatuses.Where(os => os.Id == query.Id.Value);
            }

            if (!string.IsNullOrEmpty(query.NameSubstring))
            {
                orderStatuses = orderStatuses.Where(os => os.Name.Contains(query.NameSubstring));
            }

            if (!string.IsNullOrEmpty(query.DescriptionSubstring))
            {
                orderStatuses = orderStatuses.Where(os => os.Description.Contains(query.DescriptionSubstring));
            }

            return await orderStatuses.ToListAsync();
        }

        public async Task Create(OrderStatus orderStatus)
        {
            await _context.OrderStatuses.AddAsync(orderStatus);
        }

        public void Update(OrderStatus orderStatus)
        {
            _context.Entry(orderStatus).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var orderStatus = await GetById(id);
            if (orderStatus != null)
            {
                _context.OrderStatuses.Remove(orderStatus);
            }
        }
    }
}
