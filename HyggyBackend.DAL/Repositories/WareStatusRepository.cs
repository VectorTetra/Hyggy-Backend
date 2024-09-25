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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.DAL.Repositories
{
    public class WareStatusRepository: IWareStatusRepository
    {
        private readonly HyggyContext _context;

        public WareStatusRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<WareStatus?> GetById(long id)
        {
            return await _context.WareStatuses.FindAsync(id);
        }
        public async Task<WareStatus?> GetByWareId(long id)
        {
            return await _context.WareStatuses.FirstOrDefaultAsync(x => x.Wares.Any(w => w.Id == id));
        }
        public async Task<WareStatus?> GetByWareArticle(long article)
        {
            return await _context.WareStatuses.FirstOrDefaultAsync(x => x.Wares.Any(w => w.Article == article));
        }
        public async Task<IEnumerable<WareStatus>> GetPagedWareStatuses(int pageNumber, int pageSize)
        {
            return await _context.WareStatuses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<WareStatus>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.WareStatuses.Where(x => x.Name.Contains(nameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareStatus>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.WareStatuses.Where(x => x.Description.Contains(descriptionSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<WareStatus>> GetByQuery(WareStatusQueryDAL queryDAL)
        {
            var wareStatusCollections = new List<IEnumerable<WareStatus>>();


            if (queryDAL.Id != null)
            {
                wareStatusCollections.Add(await _context.WareStatuses.Where(x => x.Id == queryDAL.Id).ToListAsync());
            }

            if (queryDAL.WareId != null)
            {
                wareStatusCollections.Add(await _context.WareStatuses.Where(x => x.Wares.Any(w => w.Id == queryDAL.WareId)).ToListAsync());
            }

            if (queryDAL.WareArticle != null)
            {
                wareStatusCollections.Add(await _context.WareStatuses.Where(x => x.Wares.Any(w => w.Article == queryDAL.WareArticle)).ToListAsync());
            }

            if (queryDAL.NameSubstring != null)
            {
                wareStatusCollections.Add(await _context.WareStatuses.Where(x => x.Name.Contains(queryDAL.NameSubstring)).ToListAsync());
            }

            if (queryDAL.DescriptionSubstring != null)
            {
                wareStatusCollections.Add(await _context.WareStatuses.Where(x => x.Description.Contains(queryDAL.DescriptionSubstring)).ToListAsync());
            }

            if (!wareStatusCollections.Any())
            {
                return new List<WareStatus>();
            }

            if (queryDAL.PageNumber != null && queryDAL.PageSize != null)
            {
                return wareStatusCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList())
                    .Skip((queryDAL.PageNumber.Value - 1) * queryDAL.PageSize.Value)
                    .Take(queryDAL.PageSize.Value);
            }
            return wareStatusCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        public async Task Create(WareStatus wareStatus)
        {
            await _context.WareStatuses.AddAsync(wareStatus);
        }
        public void Update(WareStatus wareStatus) 
        {
            _context.Entry(wareStatus).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var wareStatus = await _context.WareStatuses.FindAsync(id);
            if (wareStatus != null)
            {
                _context.WareStatuses.Remove(wareStatus);
            }
        }
    }
}
