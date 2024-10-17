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
        public async Task<IEnumerable<WareReview>> GetByStringIds(string stringIds)
        {
            // Розділяємо рядок за символом '|' та конвертуємо в список long
            List<long> ids = stringIds.Split('|').Select(long.Parse).ToList();
            // Створюємо список для збереження результатів
            var waress = new List<WareReview>();
            // Викликаємо асинхронний метод та збираємо результати
            await foreach (var ware in GetByIdsAsync(ids))
            {
                waress.Add(ware);
            }
            return waress;
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

        public async Task<IEnumerable<WareReview>> GetByAuthorizedCustomerId(string AuthorizedCustomerId)
        {
            return await _context.WareReviews.Where(wr => wr.AuthorizedCustomerId != null && wr.AuthorizedCustomerId == AuthorizedCustomerId).ToListAsync();
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
            var collections = new List<IEnumerable<WareReview>>();

            if (query.Id != null)
            {
                var res = await GetById(query.Id.Value);
                if (res != null)
                {
                    return new List<WareReview> { res };
                }
            }

            if (query.WareId != null)
            {
                collections.Add(await GetByWareId(query.WareId.Value));
            }

            if (query.Text != null)
            {
                collections.Add(await GetByTextSubstring(query.Text));
            }

            if (query.Theme != null)
            {
                collections.Add(await GetByThemeSubstring(query.Theme));
            }

            if (query.CustomerName != null)
            {
                collections.Add(await GetByCustomerNameSubstring(query.CustomerName));
            }

            if (query.AuthorizedCustomerId != null)
            {
                collections.Add(await GetByAuthorizedCustomerId(query.AuthorizedCustomerId));
            }

            if (query.Email != null)
            {
                collections.Add(await GetByEmailSubstring(query.Email));
            }

            if (query.MinRating != null && query.MaxRating != null)
            {
                collections.Add(await GetByRatingRange(query.MinRating.Value, query.MaxRating.Value));
            }

            if (query.MinDate != null && query.MaxDate != null)
            {
                collections.Add(await GetByDateRange(query.MinDate.Value, query.MaxDate.Value));
            }

            if (query.StringIds != null)
            {
                collections.Add(await GetByStringIds(query.StringIds));
            }

            var result = new List<WareReview>();
            if (query.PageNumber != null && query.PageSize != null && !collections.Any())
            {
                result = _context.WareReviews
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .ToList();
            }
            else
            {
                result = (List<WareReview>)collections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
            }

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
                    case "CustomerNameAsc":
                        result = result.OrderBy(wr => wr.CustomerName).ToList();
                        break;
                    case "CustomerNameDesc":
                        result = result.OrderByDescending(wr => wr.CustomerName).ToList();
                        break;
                    case "ThemeAsc":
                        result = result.OrderBy(wr => wr.Theme).ToList();
                        break;
                    case "ThemeDesc":
                        result = result.OrderByDescending(wr => wr.Theme).ToList();
                        break;
                    case "TextAsc":
                        result = result.OrderBy(wr => wr.Text).ToList();
                        break;
                    case "TextDesc":
                        result = result.OrderByDescending(wr => wr.Text).ToList();
                        break;
                    case "EmailAsc":
                        result = result.OrderBy(wr => wr.Email).ToList();
                        break;
                    case "EmailDesc":
                        result = result.OrderByDescending(wr => wr.Email).ToList();
                        break;
                    case "IdAsc":
                        result = result.OrderBy(wr => wr.Id).ToList();
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(wr => wr.Id).ToList();
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
                return new List<WareReview>();
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
