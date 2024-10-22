using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareReviewRepository
    {
        Task<WareReview?> GetById(long id);
        Task<IEnumerable<WareReview>> GetByWareId(long wareId);
        Task<IEnumerable<WareReview>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareReview>> GetByTextSubstring(string text);
        Task<IEnumerable<WareReview>> GetByThemeSubstring(string theme);
        Task<IEnumerable<WareReview>> GetByCustomerNameSubstring(string customerName);
        Task<IEnumerable<WareReview>> GetByAuthorizedCustomerId(string AuthorizedCustomerId);
        Task<IEnumerable<WareReview>> GetByEmailSubstring(string email);
        Task<IEnumerable<WareReview>> GetPagedWareReviews(int pageNumber, int pageSize);
        Task<IEnumerable<WareReview>> GetByRatingRange(short minRating, short maxRating);
        Task<IEnumerable<WareReview>> GetByDateRange(DateTime minDate, DateTime maxDate);
        Task<IEnumerable<WareReview>> GetByQuery(WareReviewQueryDAL query);
        IAsyncEnumerable<WareReview> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WareReview wareReview);
        void Update(WareReview wareReview);
        Task Delete(long id);
    }
}
