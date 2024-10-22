using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareReview")]
    [ApiController]
    public class WareReviewController : ControllerBase
    {
        private readonly IWareReviewService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareReviewController(IWareReviewService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }
        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareReviewQueryPL, WareReviewQueryBLL>()
            .ForMember(dest => dest.MaxDate, opt => opt.MapFrom(src => src.MaxDate))
            .ForMember(dest => dest.MinDate, opt => opt.MapFrom(src => src.MinDate))
            .ForMember(dest => dest.MaxRating, opt => opt.MapFrom(src => src.MaxRating))
            .ForMember(dest => dest.MinRating, opt => opt.MapFrom(src => src.MinRating))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
            .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.AuthorizedCustomerId, opt => opt.MapFrom(src => src.AuthorizedCustomerId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.QueryAny, opt => opt.MapFrom(src => src.QueryAny));

        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareReviewDTO>>> GetWares([FromQuery] WareReviewQueryPL query)
        {

            try
            {
                IEnumerable<WareReviewDTO> collection = null;
                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано WareReview.Id для пошуку!", nameof(WareReviewQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareReviewDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "WareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано WareReview.WareId для пошуку!", nameof(WareReviewQueryPL.WareId));
                            }
                            else
                            {
                                collection = await _serv.GetByWareId(query.WareId.Value);
                            }
                        }
                        break;
                    case "Text":
                        {
                            if (query.Text == null)
                            {
                                throw new ValidationException("Не вказано Text для пошуку!", nameof(WareReviewQueryPL.Text));
                            }
                            else
                            {
                                collection = await _serv.GetByTextSubstring(query.Text);
                            }
                        }
                        break;
                    case "Theme":
                        {
                            if (query.Theme == null)
                            {
                                throw new ValidationException("Не вказано Theme для пошуку!", nameof(WareReviewQueryPL.Theme));
                            }
                            else
                            {
                                collection = await _serv.GetByThemeSubstring(query.Theme);
                            }
                        }
                        break;
                    case "CustomerName":
                        {
                            if (query.CustomerName == null)
                            {
                                throw new ValidationException("Не вказано CustomerName для пошуку!", nameof(WareReviewQueryPL.CustomerName));
                            }
                            else
                            {
                                collection = await _serv.GetByCustomerNameSubstring(query.CustomerName);
                            }
                        }
                        break;
                    case "AuthorizedCustomerId":
                        {
                            if (query.AuthorizedCustomerId == null)
                            {
                                throw new ValidationException("Не вказано AuthorizedCustomerId для пошуку!", nameof(WareReviewQueryPL.AuthorizedCustomerId));
                            }
                            else
                            {
                                collection = await _serv.GetByAuthorizedCustomerId(query.AuthorizedCustomerId);
                            }
                        }
                        break;
                    case "Email":
                        {
                            if (query.Email == null)
                            {
                                throw new ValidationException("Не вказано Email для пошуку!", nameof(WareReviewQueryPL.Email));
                            }
                            else
                            {
                                collection = await _serv.GetByEmailSubstring(query.Email);
                            }
                        }
                        break;
                    case "RatingRange":
                        {
                            if (query.MinRating == null)
                            {
                                throw new ValidationException("Не вказано MinRating для пошуку!", nameof(WareReviewQueryPL.MinRating));
                            }
                            if (query.MaxRating == null)
                            {
                                throw new ValidationException("Не вказано MaxRating для пошуку!", nameof(WareReviewQueryPL.MaxRating));
                            }
                            collection = await _serv.GetByRatingRange(query.MinRating.Value, query.MaxRating.Value);
                        }
                        break;
                    case "DateRange":
                        {
                            if (query.MinDate == null)
                            {
                                throw new ValidationException("Не вказано MinDate для пошуку!", nameof(WareReviewQueryPL.MinDate));
                            }
                            if (query.MaxDate == null)
                            {
                                throw new ValidationException("Не вказано MaxDate для пошуку!", nameof(WareReviewQueryPL.MaxDate));
                            }
                            collection = await _serv.GetByDateRange(query.MinDate.Value, query.MaxDate.Value);
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано StringIds для пошуку!", nameof(WareReviewQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано PageNumber для пошуку!", nameof(WareReviewQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано PageSize для пошуку!", nameof(WareReviewQueryPL.PageSize));
                            }
                            collection = await _serv.GetPagedWareReviews(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<WareReviewQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Не вказано SearchParameter для пошуку!", nameof(WareReviewQueryPL.SearchParameter));
                        }


                }
                if (collection.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return collection?.ToList();
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WareReviewDTO>> CreateWare([FromBody] WareReviewDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано WareReview для створення!", nameof(WareReviewDTO));
                }
                var result = await _serv.Create(ware);
                return result;
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<WareReviewDTO>> UpdateWare([FromBody] WareReviewDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано WareReview для оновлення!", nameof(WareReviewDTO));
                }
                var result = await _serv.Update(ware);
                return result;
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WareReviewDTO>> DeleteWare(long id)
        {
            try
            {
                var result = await _serv.Delete(id);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

    }

    public class WareReviewQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? WareId { get; set; }
        public string? Text { get; set; }
        public string? Theme { get; set; }
        public string? CustomerName { get; set; }
        public string? AuthorizedCustomerId { get; set; }
        public string? Email { get; set; }
        public short? MaxRating { get; set; }
        public short? MinRating { get; set; }
        public DateTime? MaxDate { get; set; }
        public DateTime? MinDate { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? QueryAny { get; set; }
    }
}
