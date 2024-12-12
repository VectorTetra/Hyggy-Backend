using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class OrderDeliveryTypeRepository : IOrderDeliveryTypeRepository
    {
        private readonly HyggyContext _context;

        public OrderDeliveryTypeRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<OrderDeliveryType?> GetById(long id)
        {
            return await _context.OrderDeliveryTypes.FindAsync(id);
        }
        public async IAsyncEnumerable<OrderDeliveryType> GetByIdsAsync(IEnumerable<long> ids)
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
        public async Task<IEnumerable<OrderDeliveryType>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<OrderDeliveryType>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async Task<IEnumerable<OrderDeliveryType>> GetByOrderId(long orderId)
        {
            return await _context.OrderDeliveryTypes.Where(x => x.Orders.Any(order => order.Id == orderId)).ToListAsync();
        }

        public async Task<IEnumerable<OrderDeliveryType>> GetByName(string nameSubstring)
        {
            return await _context.OrderDeliveryTypes.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<OrderDeliveryType>> GetByDescription(string descriptionSubstring)
        {
            return await _context.OrderDeliveryTypes.Where(x => x.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<OrderDeliveryType>> GetByPriceRange(float minPrice, float maxPrice)
        {
            return await _context.OrderDeliveryTypes.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToListAsync();
        }

        public async Task<IEnumerable<OrderDeliveryType>> GetByDeliveryTimeInDaysRange(int minDeliveryTimeInDays, int maxDeliveryTimeInDays)
        {
            return await _context.OrderDeliveryTypes.Where(x => x.MinDeliveryTimeInDays >= minDeliveryTimeInDays && x.MaxDeliveryTimeInDays <= maxDeliveryTimeInDays).ToListAsync();
        }
        public async Task<IEnumerable<OrderDeliveryType>> GetByQuery(OrderDeliveryTypeQueryDAL query)
        {
            var collections = new List<IEnumerable<OrderDeliveryType>>();

            // Перевірка на наявність QueryAny
            if (!string.IsNullOrEmpty(query.QueryAny))
            {
                // Ви можете використовувати, наприклад, TryParse для конвертації
                if (long.TryParse(query.QueryAny, out long longVal))
                {
                    collections.Add(new List<OrderDeliveryType> { await GetById(longVal) });
                    collections.Add(await GetByOrderId(longVal));
                }
                if (float.TryParse(query.QueryAny, out float floatVal))
                {
                    collections.Add(await GetByPriceRange(floatVal, floatVal));
                }
                if (int.TryParse(query.QueryAny, out int intVal))
                {
                    collections.Add(await GetByDeliveryTimeInDaysRange((int)intVal, (int)intVal));
                }
                else
                {
                    collections.Add(await GetByName(query.QueryAny));
                    collections.Add(await GetByDescription(query.QueryAny));
                }
            }
            else
            {
                if (query.Id != null)
                {
                    collections.Add(new List<OrderDeliveryType> { await GetById(query.Id.Value) });
                }

                if (!string.IsNullOrEmpty(query.Name))
                {
                    collections.Add(await GetByName(query.Name));
                }
                if (!string.IsNullOrEmpty(query.Description))
                {
                    collections.Add(await GetByDescription(query.Description));
                }
                if(query.OrderId != null)
                {
                    collections.Add(await GetByOrderId(query.OrderId.Value));
                }
                if (query.MinPrice != null && query.MaxPrice != null)
                {
                    collections.Add(await GetByPriceRange(query.MinPrice.Value, query.MaxPrice.Value));
                }
                if (query.MinDeliveryTimeInDays != null && query.MaxDeliveryTimeInDays != null)
                {
                    collections.Add(await GetByDeliveryTimeInDaysRange(query.MinDeliveryTimeInDays.Value, query.MaxDeliveryTimeInDays.Value));
                }
                if (!string.IsNullOrEmpty(query.StringIds))
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<OrderDeliveryType>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.OrderDeliveryTypes
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
                    .Aggregate(new List<OrderDeliveryType>(), (previousList, nextList) => previousList.Intersect(nextList).ToList());
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
                    case "NameAsc":
                        result = result.OrderBy(x => x.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "DescriptionAsc":
                        result = result.OrderBy(x => x.Description).ToList();
                        break;
                    case "DescriptionDesc":
                        result = result.OrderByDescending(x => x.Description).ToList();
                        break;
                    case "PriceAsc":
                        result = result.OrderBy(x => x.Price).ToList();
                        break;
                    case "PriceDesc":
                        result = result.OrderByDescending(x => x.Price).ToList();
                        break;
                    case "MinDeliveryTimeInDaysAsc":
                        result = result.OrderBy(x => x.MinDeliveryTimeInDays).ToList();
                        break;
                    case "MinDeliveryTimeInDaysDesc":
                        result = result.OrderByDescending(x => x.MinDeliveryTimeInDays).ToList();
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

            return result.Any() ? result : new List<OrderDeliveryType>();
        }
        public async Task Create(OrderDeliveryType orderDeliveryType)
        {
            await _context.OrderDeliveryTypes.AddAsync(orderDeliveryType);
        }
        public void Update(OrderDeliveryType orderDeliveryType) 
        {
            _context.Entry(orderDeliveryType).State = EntityState.Modified;
        }
        public async Task Delete(long id) 
        {
            var orderDeliveryType = await GetById(id);
            if (orderDeliveryType != null)
            {
                _context.OrderDeliveryTypes.Remove(orderDeliveryType);
            }
        }
    }
}
