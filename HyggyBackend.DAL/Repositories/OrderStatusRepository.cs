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

        public async Task<IEnumerable<OrderStatus>> GetByOrderId(long orderId)
        {
            return await _context.OrderStatuses.Where(os => os.Orders.Any(o => o.Id == orderId)).ToListAsync();
        }

        public async Task<IEnumerable<OrderStatus>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<OrderStatus>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async Task<IEnumerable<OrderStatus>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.OrderStatuses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderStatus>> GetByQuery(OrderStatusQueryDAL query)
        {
            var collections = new List<IEnumerable<OrderStatus>>();

            if (query.QueryAny != null)
            {
                if (long.TryParse(query.QueryAny, out long id))
                {
                    collections.Add(await GetByOrderId(id));
                    collections.Add(new List<OrderStatus> { await GetById(id) });

                }
                collections.Add(await GetByNameSubstring(query.QueryAny));
                collections.Add(await GetByDescriptionSubstring(query.QueryAny));
            }
            else
            {
                if (query.Id != null)
                {
                    var res = await GetById(query.Id.Value);
                    if (res != null)
                    {
                        collections.Add(new List<OrderStatus> { res });
                    }
                }

                if (query.NameSubstring != null)
                {
                    collections.Add(await GetByNameSubstring(query.NameSubstring));
                }

                if (query.DescriptionSubstring != null)
                {
                    collections.Add(await GetByDescriptionSubstring(query.DescriptionSubstring));
                }

                if (query.OrderId != null)
                {
                    collections.Add(await GetByOrderId(query.OrderId.Value));
                }

                if (query.StringIds != null)
                {
                    collections.Add(await GetByStringIds(query.StringIds));
                }
            }

            var result = new List<OrderStatus>();

            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.OrderStatuses
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
                // Використовуємо Intersect для знаходження записів, які задовольняють всі умови
                result = collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList)).ToList();
            }

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "IdAsc":
                        result = result.OrderBy(os => os.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(os => os.Id).ToList();
                        break;
                    case "NameAsc":
                        result = result.OrderBy(os => os.Name).ToList();
                        break;
                    case "NameDesc":
                        result = result.OrderByDescending(os => os.Name).ToList();
                        break;
                    case "DescriptionAsc":
                        result = result.OrderBy(os => os.Description).ToList();
                        break;
                    case "DescriptionDesc":
                        result = result.OrderByDescending(os => os.Description).ToList();
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

            return result.Any() ? result : new List<OrderStatus>();
        }


        public async IAsyncEnumerable<OrderStatus> GetByIdsAsync(IEnumerable<long> ids)
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
