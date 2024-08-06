using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;


namespace HyggyBackend.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HyggyContext _context;

        public OrderRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<Order?> GetById(long id)
        {
            return await _context.Orders.FindAsync(id);
        }
        public async Task<IEnumerable<Order>> GetByDeliveryAddressSubstring(string deliveryAddressSubstring)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.Contains(deliveryAddressSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByOrderDateRange(DateTime minOrderDate, DateTime maxOrderDate)
        {
            return await _context.Orders.Where(x => x.OrderDate >= minOrderDate && x.OrderDate <= maxOrderDate).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByPhoneSubstring(string phoneSubstring)
        {
            return await _context.Orders.Where(x => x.Phone.Contains(phoneSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByCommentSubstring(string commentSubstring)
        {
            return await _context.Orders.Where(x => x.Comment.Contains(commentSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByStatusId(long statusId)
        {
            return await _context.Orders.Where(x => x.Status.Id == statusId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            return await _context.Orders.Where(x => x.Status.Name.Contains(statusNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            return await _context.Orders.Where(x => x.Status.Description.Contains(statusDescriptionSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByOrderItemId(long orderItemId)
        {
            return await _context.Orders.Where(x => x.OrderItems.Any(m => m.Id == orderItemId)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByWareId(long wareId)
        {
            return await _context.Orders.Where(x => x.OrderItems.Any(m => m.Ware.Id == wareId)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByWarePriceHistoryId(long warePriceHistoryId)
        {
            return await _context.Orders.Where(x => x.OrderItems.Any(m => m.PriceHistory.Id == warePriceHistoryId)).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByCustomerId(long customerId)
        {
            return await _context.Orders.Where(x => x.Customer.Id == customerId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByShopId(long shopId)
        {
            return await _context.Orders.Where(x => x.Shop.Id == shopId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByQuery(OrderQueryDAL query)
        {
            var orderCollections = new List<IEnumerable<Order>>();

            if (query.DeliveryAddress != null)
            {
                orderCollections.Add(await GetByDeliveryAddressSubstring(query.DeliveryAddress));
            }

            if (query.MaxOrderDate != null && query.MinOrderDate != null)
            {
                orderCollections.Add(await GetByOrderDateRange(query.MinOrderDate.Value, query.MaxOrderDate.Value));
            }

            if (query.Phone != null)
            {
                orderCollections.Add(await GetByPhoneSubstring(query.Phone));
            }

            if (query.Comment != null)
            {
                orderCollections.Add(await GetByCommentSubstring(query.Comment));
            }

            if (query.StatusId != null)
            {
                orderCollections.Add(await GetByStatusId(query.StatusId.Value));
            }

            if (query.StatusName != null)
            {
                orderCollections.Add(await GetByStatusNameSubstring(query.StatusName));
            }

            if (query.StatusDescription != null)
            {
                orderCollections.Add(await GetByStatusDescriptionSubstring(query.StatusDescription));
            }

            if (query.OrderItemId != null)
            {
                orderCollections.Add(await GetByOrderItemId(query.OrderItemId.Value));
            }

            if (query.WareId != null)
            {
                orderCollections.Add(await GetByWareId(query.WareId.Value));
            }

            if (query.WarePriceHistoryId != null)
            {
                orderCollections.Add(await GetByWarePriceHistoryId(query.WarePriceHistoryId.Value));
            }

            if (query.CustomerId != null)
            {
                orderCollections.Add(await GetByCustomerId(query.CustomerId.Value));
            }

            if (query.ShopId != null)
            {
                orderCollections.Add(await GetByShopId(query.ShopId.Value));
            }

            return orderCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());



        }
        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
        public async Task Delete(long id)
        {
            var order = await GetById(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }
    }
}
