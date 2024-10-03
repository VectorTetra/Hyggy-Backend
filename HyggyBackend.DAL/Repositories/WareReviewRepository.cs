using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Repositories
{
    public class WareReviewRepository : IWareReviewRepository
    {
        private readonly HyggyContext _context;

        public WareReviewRepository(HyggyContext context)
        {
            _context = context;
        }
        public async Task<WareReview?> GetById(long id)
        {
            return await _context.WareReviews.FindAsync(id);
        }
        public async Task<IEnumerable<WareReview>> GetByWareId(long wareId)
        {
            return await _context.WareReviews.Where(wr => wr.Ware.Id == wareId).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetByTextSubstring(string text)
        {
            return await _context.WareReviews.Where(wr => wr.Text.Contains(text)).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetByThemeSubstring(string theme)
        {
            return await _context.WareReviews.Where(wr => wr.Theme.Contains(theme)).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetByCustomerNameSubstring(string customerName)
        {
            return await _context.WareReviews.Where(wr => wr.CustomerName.Contains(customerName)).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetByEmailSubstring(string email)
        {
            return await _context.WareReviews.Where(wr => wr.Email.Contains(email)).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetPagedWareReviews(int pageNumber, int pageSize)
        {
            return await _context.WareReviews.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetByRatingRange(short minRating, short maxRating)
        {
            if (minRating > maxRating)
            {
                return await _context.WareReviews.Where(wr => wr.Rating >= maxRating && wr.Rating <= minRating).ToListAsync();
            }
            if (minRating == maxRating)
            {
                return await _context.WareReviews.Where(wr => wr.Rating == minRating).ToListAsync();
            }
            return await _context.WareReviews.Where(wr => wr.Rating >= minRating && wr.Rating <= maxRating).ToListAsync();
        }
        public async Task<IEnumerable<WareReview>> GetByDateRange(DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate)
            {
                return await _context.WareReviews.Where(wr => wr.Date >= maxDate && wr.Date <= minDate).ToListAsync();
            }
            if (minDate == maxDate)
            {
                return await _context.WareReviews.Where(wr => wr.Date == minDate).ToListAsync();
            }
            return await _context.WareReviews.Where(wr => wr.Date >= minDate && wr.Date <= maxDate).ToListAsync();
        }

        public async IAsyncEnumerable<WareReview> GetByIdsAsync(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                var wareReview = await GetById(id);  // Виклик методу репозиторію
                if (wareReview != null)
                {
                    yield return wareReview;
                }
            }
        }
        public async Task<IEnumerable<WareReview>> GetByQuery(WareReviewQueryDAL query)
        {
            var wareReviewCollections = new List<IEnumerable<WareReview>>();

            if (query.Id != null)
            {
                wareReviewCollections.Add(new List<WareReview> { await GetById(query.Id.Value) });
            }

            if (query.WareId != null)
            {
                wareReviewCollections.Add(await GetByWareId(query.WareId.Value));
            }

            if (query.Text != null)
            {
                wareReviewCollections.Add(await GetByTextSubstring(query.Text));
            }

            if (query.Theme != null)
            {
                wareReviewCollections.Add(await GetByThemeSubstring(query.Theme));
            }

            if (query.CustomerName != null)
            {
                wareReviewCollections.Add(await GetByCustomerNameSubstring(query.CustomerName));
            }

            if (query.Email != null)
            {
                wareReviewCollections.Add(await GetByEmailSubstring(query.Email));
            }

            if (query.MinRating != null && query.MaxRating != null)
            {
                wareReviewCollections.Add(await GetByRatingRange(query.MinRating.Value, query.MaxRating.Value));
            }

            if (query.MinDate != null && query.MaxDate != null)
            {
                wareReviewCollections.Add(await GetByDateRange(query.MinDate.Value, query.MaxDate.Value));
            }

            if (!wareReviewCollections.Any())
            {
                return new List<WareReview>();
            }

            var result = wareReviewCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());

            // Сортування
            if (query.Sorting != null)
            {
                switch (query.Sorting)
                {
                    case "RatingAsc":
                        result = result.OrderBy(wr => wr.Rating).ToList();
                        break;
                    case "RatingDesc":
                        result = result.OrderByDescending(wr => wr.Rating).ToList();
                        break;
                    case "DateAsc":
                        result = result.OrderBy(wr => wr.Date).ToList();
                        break;
                    case "DateDesc":
                        result = result.OrderByDescending(wr => wr.Date).ToList();
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
        public async Task Create(WareReview wareReview) 
        {
            await _context.WareReviews.AddAsync(wareReview);
        }
        public void Update(WareReview wareReview) 
        {
            _context.Entry(wareReview).State = EntityState.Modified;
        }
        public async Task Delete(long id) 
        {
            var wareReview = await GetById(id);
            if (wareReview != null)
            {
                _context.WareReviews.Remove(wareReview);
            }
        }
    }
}
