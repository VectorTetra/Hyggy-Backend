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

        public async Task<IEnumerable<Order>> GetByAddressId(long addressId)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.Id == addressId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStreet(string streetSubstring)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.Street.Contains(streetSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByHouseNumber(string houseNumber)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.HouseNumber == houseNumber).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCity(string city)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByPostalCode(string postalCode)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.PostalCode == postalCode).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByState(string state)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.State == state).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByLatitudeAndLongitude(double latitude, double longitude)
        {
            return await _context.Orders.Where(x => x.DeliveryAddress.Latitude == latitude && x.DeliveryAddress.Longitude == longitude).ToListAsync();
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


            if (query.AddressId != null)
            {
                orderCollections.Add(await GetByAddressId(query.AddressId.Value));
            }

            if (query.Street != null)
            {
                orderCollections.Add(await GetByStreet(query.Street));
            }

            if (query.HouseNumber != null)
            {
                orderCollections.Add(await GetByHouseNumber(query.HouseNumber));
            }

            if (query.City != null)
            {
                orderCollections.Add(await GetByCity(query.City));
            }

            if (query.PostalCode != null)
            {
                orderCollections.Add(await GetByPostalCode(query.PostalCode));
            }

            if (query.State != null)
            {
                orderCollections.Add(await GetByState(query.State));
            }

            if (query.Latitude != null && query.Longitude != null)
            {
                orderCollections.Add(await GetByLatitudeAndLongitude(query.Latitude.Value, query.Longitude.Value));
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
            _context.Entry(order).State = EntityState.Modified;
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
