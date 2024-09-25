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
            var warePriceHistories = _context.WarePriceHistories.AsQueryable();

            if (query.WareId.HasValue)
            {
                warePriceHistories = warePriceHistories.Where(wph => wph.Ware.Id == query.WareId.Value);
            }

            if (query.MinPrice.HasValue)
            {
                warePriceHistories = warePriceHistories.Where(wph => wph.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                warePriceHistories = warePriceHistories.Where(wph => wph.Price <= query.MaxPrice.Value);
            }
            
            if (query.StartDate.HasValue)
            {
                warePriceHistories = warePriceHistories.Where(wph => wph.EffectiveDate >= query.StartDate.Value);

            }

            if (query.EndDate.HasValue)
            {
                warePriceHistories = warePriceHistories.Where(wph => wph.EffectiveDate <= query.EndDate.Value);
            }

            return await warePriceHistories.ToListAsync();
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
