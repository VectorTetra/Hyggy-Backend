using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareStatus")]
    [ApiController]
    public class WareStatusController : ControllerBase
    {
        private readonly IWareStatusService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareStatusController(IWareStatusService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareStatusQueryPL, WareStatusQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
             .ForMember(dest => dest.WareArticle, opt => opt.MapFrom(src => src.WareArticle))
             .ForMember(dest => dest.NameSubstring, opt => opt.MapFrom(src => src.NameSubstring))
             .ForMember(dest => dest.DescriptionSubstring, opt => opt.MapFrom(src => src.DescriptionSubstring))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareStatusDTO>>> GetWares([FromQuery] WareStatusQueryPL query)
        {
            try
            {
                IEnumerable<WareStatusDTO> collection = null;

                switch (query.SearchParameter)
                {

                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано WareStatus.Id для пошуку!", nameof(WareStatusQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareStatusDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "WareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано WareStatus.WareId для пошуку!", nameof(WareStatusQueryPL.WareId));
                            }
                            else
                            {
                                collection = new List<WareStatusDTO> { await _serv.GetByWareId(query.WareId.Value) };
                            }
                        }
                        break;
                    case "WareArticle":
                        {
                            if (query.WareArticle == null)
                            {
                                throw new ValidationException("Не вказано WareStatus.WareArticle для пошуку!", nameof(WareStatusQueryPL.WareArticle));
                            }
                            else
                            {
                                collection = new List<WareStatusDTO> { await _serv.GetByWareArticle(query.WareArticle.Value) };
                            }
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано WareStatusQuery.PageNumber для пошуку!", nameof(WareStatusQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано WareStatusQuery.PageSize для пошуку!", nameof(WareStatusQueryPL.PageSize));
                            }
                            collection = await _serv.GetPagedWareStatuses(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "Name":
                        {
                            if (query.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareStatus.NameSubstring для пошуку!", nameof(WareStatusQueryPL.NameSubstring));
                            }
                            collection = await _serv.GetByNameSubstring(query.NameSubstring);
                        }
                        break;
                    case "Description":
                        {
                            if (query.DescriptionSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareStatus.DescriptionSubstring для пошуку!", nameof(WareStatusQueryPL.DescriptionSubstring));
                            }
                            collection = await _serv.GetByDescriptionSubstring(query.DescriptionSubstring);
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано WareStatus.StringIds для пошуку!", nameof(WareStatusQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Query":
                        {
                            collection = await _serv.GetByQuery(config.CreateMapper().Map<WareStatusQueryBLL>(query));
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр WareStatusQuery.SearchParameter!", nameof(WareQueryPL.SearchParameter));
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
        public async Task<ActionResult<WareStatusDTO>> CreateWare([FromBody] WareStatusDTO wareStatus)
        {
            try
            {
                if (wareStatus == null)
                {
                    throw new ValidationException("Не вказано WareStatusDTO для створення!", nameof(WareStatusDTO));
                }
                var result = await _serv.Create(wareStatus);
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
        public async Task<ActionResult<WareStatusDTO>> UpdateWare([FromBody] WareStatusDTO wareStatus)
        {
            try
            {
                if (wareStatus == null)
                {
                    throw new ValidationException("Не вказано WareStatusDTO для оновлення!", nameof(WareStatusDTO));
                }
                var result = await _serv.Update(wareStatus);
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
        public async Task<ActionResult<WareStatusDTO>> DeleteWare(long id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ValidationException("Не вказано WareStatus.Id для видалення!", nameof(WareStatusDTO.Id));
                }
                var result = await _serv.Delete(id);
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
    }

    public class WareStatusQueryPL
    {
        public string SearchParameter { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public long? Id { get; set; }
        public long? WareId { get; set; }
        public long? WareArticle { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
