using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Entities.Employes;
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
        public async Task<IEnumerable<Order>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<Order>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
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
        public async Task<IEnumerable<Order>> GetPagedOrders(int pageNumber, int pageSize)
        {
            return await _context.Orders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
        public async Task<IEnumerable<Order>> GetByCustomerId(string customerId)
        {
            return await _context.Orders.Where(x => x.Customer.Id == customerId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByShopId(long shopId)
        {
            return await _context.Orders.Where(x => x.Shop.Id == shopId).ToListAsync();
        }

        public async IAsyncEnumerable<Order> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var item = await GetById(id);  // Виклик методу репозиторію
                if (item != null)
                {
                    yield return item;
                }
            }
        }
        public async Task<IEnumerable<Order>> GetByQuery(OrderQueryDAL query)
        {
            var collections = new List<IEnumerable<Order>>();

            // Перевірка на наявність QueryAny
            if (!string.IsNullOrEmpty(query.QueryAny))
            {
                // Ви можете використовувати, наприклад, TryParse для конвертації
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(await GetByOrderItemId(id));
                    collections.Add(await GetByWareId(id));
                    collections.Add(await GetByShopId(id));
                    collections.Add(new List<Order> { await GetById(id) });
                }
                else
                {
                    collections.Add(await GetByPhoneSubstring(query.QueryAny));
                    collections.Add(await GetByCommentSubstring(query.QueryAny));
                    collections.Add(await GetByStatusNameSubstring(query.QueryAny));
                    collections.Add(await GetByStatusDescriptionSubstring(query.QueryAny));
                }
            }
            else
            {
                if (query.Id != null)
                {
                    collections.Add(new List<Order> { await GetById(query.Id.Value) });
                }

                if (query.AddressId != null)
                {
                    collections.Add(await GetByAddressId(query.AddressId.Value));
                }

                if (query.Street != null)
                {
                    collections.Add(await GetByStreet(query.Street));
                }

                if (query.HouseNumber != null)
                {
                    collections.Add(await GetByHouseNumber(query.HouseNumber));
                }

                if (query.City != null)
                {
                    collections.Add(await GetByCity(query.City));
                }

                if (query.PostalCode != null)
                {
                    collections.Add(await GetByPostalCode(query.PostalCode));
                }

                if (query.State != null)
                {
                    collections.Add(await GetByState(query.State));
                }

                if (query.Latitude != null && query.Longitude != null)
                {
                    collections.Add(await GetByLatitudeAndLongitude(query.Latitude.Value, query.Longitude.Value));
                }

                if (query.MaxOrderDate != null && query.MinOrderDate != null)
                {
                    collections.Add(await GetByOrderDateRange(query.MinOrderDate.Value, query.MaxOrderDate.Value));
                }

                if (query.Phone != null)
                {
                    collections.Add(await GetByPhoneSubstring(query.Phone));
                }

                if (query.Comment != null)
                {
                    collections.Add(await GetByCommentSubstring(query.Comment));
                }

                if (query.StatusId != null)
                {
                    collections.Add(await GetByStatusId(query.StatusId.Value));
                }

                if (query.StatusName != null)
                {
                    collections.Add(await GetByStatusNameSubstring(query.StatusName));
                }

                if (query.StatusDescription != null)
                {
                    collections.Add(await GetByStatusDescriptionSubstring(query.StatusDescription));
                }

                if (query.OrderItemId != null)
                {
                    collections.Add(await GetByOrderItemId(query.OrderItemId.Value));
                }

                if (query.WareId != null)
                {
                    collections.Add(await GetByWareId(query.WareId.Value));
                }

                if (query.WarePriceHistoryId != null)
                {
                    collections.Add(await GetByWarePriceHistoryId(query.WarePriceHistoryId.Value));
                }

                if (query.CustomerId != null)
                {
                    collections.Add(await GetByCustomerId(query.CustomerId));
                }

                if (query.ShopId != null)
                {
                    collections.Add(await GetByShopId(query.ShopId.Value));
                }

                if (!string.IsNullOrEmpty(query.StringIds))
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<Order>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.Orders
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }
            else if (query.QueryAny != null && collections.Any())
            {
                // Використовуємо Union для об'єднання результатів
                result = collections.SelectMany(x => x).Distinct().ToList();
            }
            else
            {
                result = collections
                    .Aggregate(new List<Order>(), (previousList, nextList) => previousList.Intersect(nextList).ToList());
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "IdAsc":
                        result = result.OrderBy(x => x.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(x => x.Id).ToList();
                        break;
                    case "OrderDateAsc":
                        result = result.OrderBy(x => x.OrderDate).ToList();
                        break;
                    case "OrderDateDesc":
                        result = result.OrderByDescending(x => x.OrderDate).ToList();
                        break;
                    case "PhoneAsc":
                        result = result.OrderBy(x => x.Phone).ToList();
                        break;
                    case "PhoneDesc":
                        result = result.OrderByDescending(x => x.Phone).ToList();
                        break;
                    case "CommentAsc":
                        result = result.OrderBy(x => x.Comment).ToList();
                        break;
                    case "CommentDesc":
                        result = result.OrderByDescending(x => x.Comment).ToList();
                        break;
                    case "StatusIdAsc":
                        result = result.OrderBy(x => x.Status.Id).ToList();
                        break;
                    case "StatusIdDesc":
                        result = result.OrderByDescending(x => x.Status.Id).ToList();
                        break;
                    case "ShopIdAsc":
                        result = result.OrderBy(x => x.Shop.Id).ToList();
                        break;
                    case "ShopIdDesc":
                        result = result.OrderByDescending(x => x.Shop.Id).ToList();
                        break;
                    case "CustomerIdAsc":
                        result = result.OrderBy(x => x.Customer.Id).ToList();
                        break;
                    case "CustomerIdDesc":
                        result = result.OrderByDescending(x => x.Customer.Id).ToList();
                        break;
                    case "DeliveryAddressIdAsc":
                        result = result.OrderBy(x => x.DeliveryAddress.Id).ToList();
                        break;
                    case "DeliveryAddressIdDesc":
                        result = result.OrderByDescending(x => x.DeliveryAddress.Id).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null)
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<Order>();
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
