using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareReviewService
    {
        Task<WareReviewDTO?> GetById(long id);
        Task<IEnumerable<WareReviewDTO>> GetByWareId(long wareId);
        Task<IEnumerable<WareReviewDTO>> GetByStringIds(string StringIds);
        Task<IEnumerable<WareReviewDTO>> GetByTextSubstring(string text);
        Task<IEnumerable<WareReviewDTO>> GetByThemeSubstring(string theme);
        Task<IEnumerable<WareReviewDTO>> GetByCustomerNameSubstring(string customerName);
        Task<IEnumerable<WareReviewDTO>> GetByAuthorizedCustomerId(string AuthorizedCustomerId);
        Task<IEnumerable<WareReviewDTO>> GetByEmailSubstring(string email);
        Task<IEnumerable<WareReviewDTO>> GetPagedWareReviews(int pageNumber, int pageSize);
        Task<IEnumerable<WareReviewDTO>> GetByRatingRange(short minRating, short maxRating);
        Task<IEnumerable<WareReviewDTO>> GetByDateRange(DateTime minDate, DateTime maxDate);
        Task<IEnumerable<WareReviewDTO>> GetByQuery(WareReviewQueryBLL query);
        Task<WareReviewDTO> Create(WareReviewDTO wareReview);
        Task<WareReviewDTO> Update(WareReviewDTO wareReview);
        Task<WareReviewDTO> Delete(long id);
    }
}
