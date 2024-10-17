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
    public class WarePriceHistoryRepository : IWarePriceHistoryRepository
    {
        private readonly HyggyContext _context;

        public WarePriceHistoryRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<WarePriceHistory?> GetById(long id)
        {
            return await _context.WarePriceHistories.FindAsync(id);
        }

        public async Task<IEnumerable<WarePriceHistory>> GetPaged(int pageNumber, int pageSize)
        {
            return await _context.WarePriceHistories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<WarePriceHistory>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WarePriceHistory>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
        }

        public async Task<IEnumerable<WarePriceHistory>> GetByWareId(long wareId)
        {
            return await _context.WarePriceHistories.Where(wph => wph.Ware.Id == wareId).ToListAsync();
        }

        public async Task<IEnumerable<WarePriceHistory>> GetByPriceRange(float minPrice, float maxPrice)
        {
            return await _context.WarePriceHistories.Where(wph => wph.Price >= minPrice && wph.Price <= maxPrice).ToListAsync();
        }

        public async Task<IEnumerable<WarePriceHistory>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.WarePriceHistories.Where(wph => wph.EffectiveDate >= startDate && wph.EffectiveDate <= endDate).ToListAsync();
        }

        public async Task<IEnumerable<WarePriceHistory>> GetByQuery(WarePriceHistoryQueryDAL query)
        {
            var collections = new List<IEnumerable<WarePriceHistory>>();

            if (query.Id.HasValue)
            {
                var proto = await GetById(query.Id.Value);
                if(proto != null)
                {
                    collections.Add(new List<WarePriceHistory> { proto });
                }

            }
            if (query.WareId.HasValue)
            {
                collections.Add(await GetByWareId(query.WareId.Value));
            }

            if (query.MinPrice.HasValue && query.MaxPrice.HasValue)
            {
                collections.Add(await GetByPriceRange(query.MinPrice.Value, query.MaxPrice.Value));
            }

            if (query.StartDate.HasValue && query.EndDate.HasValue)
            {
                collections.Add(await GetByDateRange(query.StartDate.Value, query.EndDate.Value));
            }

            if (!string.IsNullOrEmpty(query.StringIds))
            {
                collections.Add(await GetByStringIds(query.StringIds));
            }

            var result = new List<WarePriceHistory>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.WarePriceHistories
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .ToList();
            }
            else
            {
                result = (List<WarePriceHistory>)collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
            }


            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "IdAsc":
                        result = result.OrderBy(ware => ware.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(ware => ware.Id).ToList();
                        break;
                    case "WareIdAsc":
                        result = result.OrderBy(ware => ware.Ware.Id).ToList();
                        break;
                    case "WareIdDesc":
                        result = result.OrderByDescending(ware => ware.Ware.Id).ToList();
                        break;
                    case "PriceAsc":
                        result = result.OrderBy(ware => ware.Price).ToList();
                        break;
                    case "PriceDesc":
                        result = result.OrderByDescending(ware => ware.Price).ToList();
                        break;
                    case "EffectiveDateAsc":
                        result = result.OrderBy(ware => ware.EffectiveDate).ToList();
                        break;
                    case "EffectiveDateDesc":
                        result = result.OrderByDescending(ware => ware.EffectiveDate).ToList();
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
            if (!result.Any())
            {
                return new List<WarePriceHistory>();
            }
            return result;
        }

        public async IAsyncEnumerable<WarePriceHistory> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var warePriceHistory = await GetById(id);
                if (warePriceHistory != null)
                {
                    yield return warePriceHistory;
                }
            }
        }

        public async Task Create(WarePriceHistory warePriceHistory)
        {
            await _context.WarePriceHistories.AddAsync(warePriceHistory);
        }

        public void Update(WarePriceHistory warePriceHistory)
        {
            _context.Entry(warePriceHistory).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var warePriceHistory = await GetById(id);
            if (warePriceHistory != null)
            {
                _context.WarePriceHistories.Remove(warePriceHistory);
            }
        }
    }
}
