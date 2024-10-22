using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class WareReviewService : IWareReviewService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public WareReviewService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<WareReviewDTO?> GetById(long id)
        {
            var wareReview = await Database.WareReviews.GetById(id);
            return _mapper.Map<WareReviewDTO>(wareReview);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByStringIds(string StringIds)
        {
            var wareReviews = await Database.WareReviews.GetByStringIds(StringIds);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByWareId(long wareId)
        {
            var wareReviews = await Database.WareReviews.GetByWareId(wareId);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByTextSubstring(string text)
        {
            var wareReviews = await Database.WareReviews.GetByTextSubstring(text);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByThemeSubstring(string theme)
        {
            var wareReviews = await Database.WareReviews.GetByThemeSubstring(theme);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByCustomerNameSubstring(string customerName)
        {
            var wareReviews = await Database.WareReviews.GetByCustomerNameSubstring(customerName);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }

        public async Task<IEnumerable<WareReviewDTO>> GetByAuthorizedCustomerId(string AuthorizedCustomerId)
        {
            var wareReviews = await Database.WareReviews.GetByAuthorizedCustomerId(AuthorizedCustomerId);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByEmailSubstring(string email)
        {
            var wareReviews = await Database.WareReviews.GetByEmailSubstring(email);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetPagedWareReviews(int pageNumber, int pageSize)
        {
            var wareReviews = await Database.WareReviews.GetPagedWareReviews(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByRatingRange(short minRating, short maxRating)
        {
            var wareReviews = await Database.WareReviews.GetByRatingRange(minRating, maxRating);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByDateRange(DateTime minDate, DateTime maxDate)
        {
            var wareReviews = await Database.WareReviews.GetByDateRange(minDate, maxDate);
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<IEnumerable<WareReviewDTO>> GetByQuery(WareReviewQueryBLL query)
        {
            var wareReviews = await Database.WareReviews.GetByQuery(_mapper.Map<WareReviewQueryDAL>(query));
            return _mapper.Map<IEnumerable<WareReviewDTO>>(wareReviews);
        }
        public async Task<WareReviewDTO> Create(WareReviewDTO wareReview)
        {

            var existedWare = await Database.Wares.GetById(wareReview.WareId.Value);
            if (existedWare == null)
            {
                throw new ValidationException($"Товару з вказаним Id ({wareReview.WareId}) не існує!", "");
            }
            if(wareReview.Rating == null)
            {
                throw new ValidationException("Оцінка не може бути пустою!", "");
            }
            if (wareReview.Rating < 1 || wareReview.Rating > 5)
            {
                throw new ValidationException("Оцінка повинна бути в межах від 1 до 5!", "");
            }
            if (wareReview.Text == null)
            {
                throw new ValidationException("Текст відгуку не може бути пустим!", "");
            }
            if (wareReview.Theme == null)
            {
                throw new ValidationException("Тема відгуку не може бути пустою!", "");
            }
            if (wareReview.CustomerName == null)
            {
                throw new ValidationException("Ім'я покупця не може бути пустим!", "");
            }
            if (wareReview.Email == null)
            {
                throw new ValidationException("Email покупця не може бути пустим!", "");
            }
            var reviewDAL = new WareReview
            {
                Text = wareReview.Text,
                Theme = wareReview.Theme,
                CustomerName = wareReview.CustomerName,
                Ware = existedWare,
                AuthorizedCustomerId = wareReview.AuthorizedCustomerId,
                Email = wareReview.Email,
                Rating = wareReview.Rating.Value,
                Date = DateTime.Now
            };
            await Database.WareReviews.Create(reviewDAL);
            await Database.Save();
            var returnedReview = await Database.WareReviews.GetById(reviewDAL.Id);
            return _mapper.Map<WareReviewDTO>(returnedReview);
        }
        public async Task<WareReviewDTO> Update(WareReviewDTO wareReview) 
        {
            var existedReview = await Database.WareReviews.GetById(wareReview.Id);
            if (existedReview == null)
            {
                throw new ValidationException($"Відгуку з вказаним Id ({wareReview.Id}) не існує!", "");
            }
            var existedWare = await Database.Wares.GetById(wareReview.WareId.Value);
            if (existedWare == null)
            {
                throw new ValidationException($"Товару з вказаним Id ({wareReview.WareId}) не існує!", "");
            }
            if (wareReview.Rating == null)
            {
                throw new ValidationException("Оцінка не може бути пустою!", "");
            }
            if (wareReview.Rating < 1 || wareReview.Rating > 5)
            {
                throw new ValidationException("Оцінка повинна бути в межах від 1 до 5!", "");
            }
            if (wareReview.Text == null)
            {
                throw new ValidationException("Текст відгуку не може бути пустим!", "");
            }
            if (wareReview.Theme == null)
            {
                throw new ValidationException("Тема відгуку не може бути пустою!", "");
            }
            if (wareReview.CustomerName == null)
            {
                throw new ValidationException("Ім'я покупця не може бути пустим!", "");
            }
            if (wareReview.Email == null)
            {
                throw new ValidationException("Email покупця не може бути пустим!", "");
            }
            existedReview.Ware = existedWare;
            existedReview.Text = wareReview.Text;
            existedReview.Theme = wareReview.Theme;
            existedReview.CustomerName = wareReview.CustomerName;
            existedReview.AuthorizedCustomerId = wareReview.AuthorizedCustomerId;
            existedReview.Email = wareReview.Email;
            existedReview.Rating = wareReview.Rating.Value;
            existedReview.Date = DateTime.Now;
            Database.WareReviews.Update(existedReview);
            await Database.Save();
            var returnedReview = await Database.WareReviews.GetById(existedReview.Id);
            return _mapper.Map<WareReviewDTO>(returnedReview);
        }
        public async Task<WareReviewDTO> Delete(long id) 
        {
            var existedReview = await Database.WareReviews.GetById(id);
            if (existedReview == null)
            {
                throw new ValidationException($"Відгуку з вказаним Id ({id}) не існує!", "");
            }
            Database.WareReviews.Delete(id);
            await Database.Save();
            return _mapper.Map<WareReviewDTO>(existedReview);
        }
    }
}
