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
        public async Task<IEnumerable<OrderItem>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<OrderItem>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async Task<IEnumerable<OrderItem>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.OrderItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
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
            var collections = new List<IEnumerable<OrderItem>>();

            // Пошук за QueryAny
            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long wareId))
                {
                    collections.Add(await GetByWareId(wareId));
                    collections.Add(new List<OrderItem> { await GetById(wareId) });

                }

                if (long.TryParse(query.QueryAny, out long orderId))
                {
                    collections.Add(await GetByOrderId(orderId));
                }

                // Якщо потрібно, ви можете також обробити StringIds або інші поля, якщо це потрібно.
              
            }

            else
            {
                if (query.Id != null)
                {
                    var proto = await GetById(query.Id.Value);
                    if (proto != null)
                    {
                        collections.Add(new List<OrderItem> { proto });
                    }
                }
                if (query.OrderId != null)
                {
                    collections.Add(await GetByOrderId(query.OrderId.Value));
                }
                if (query.WareId != null)
                {
                    collections.Add(await GetByWareId(query.WareId.Value));
                }
                if (query.PriceHistoryId != null)
                {
                    collections.Add(await GetByPriceHistoryId(query.PriceHistoryId.Value));
                }
                if (query.Count != null)
                {
                    collections.Add(await GetByCount(query.Count.Value));
                }
                if (!string.IsNullOrEmpty(query.StringIds))
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<OrderItem>();

            // Пошук за сторінками, якщо не знайдено жодного результату
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.OrderItems
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
                var nonEmptyCollections = collections.Where(collection => collection.Any()).ToList();

                // Перетин результатів з відфільтрованих колекцій
                if (nonEmptyCollections.Any())
                {
                    result = nonEmptyCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
                }
                else
                {
                    result = new List<OrderItem>(); // Повертаємо порожній список, якщо всі колекції були порожні
                }
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
                    case "OrderIdAsc":
                        result = result.OrderBy(x => x.OrderId).ToList();
                        break;
                    case "OrderIdDesc":
                        result = result.OrderByDescending(x => x.OrderId).ToList();
                        break;
                    case "WareIdAsc":
                        result = result.OrderBy(x => x.WareId).ToList();
                        break;
                    case "WareIdDesc":
                        result = result.OrderByDescending(x => x.WareId).ToList();
                        break;
                    case "PriceHistoryIdAsc":
                        result = result.OrderBy(x => x.PriceHistoryId).ToList();
                        break;
                    case "PriceHistoryIdDesc":
                        result = result.OrderByDescending(x => x.PriceHistoryId).ToList();
                        break;
                    case "CountAsc":
                        result = result.OrderBy(x => x.Count).ToList();
                        break;
                    case "CountDesc":
                        result = result.OrderByDescending(x => x.Count).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Пагінація
            if (query.PageNumber != null && query.PageSize != null && result.Any())
            {
                result = result
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .ToList();
            }

            return result.Any() ? result : new List<OrderItem>();
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
