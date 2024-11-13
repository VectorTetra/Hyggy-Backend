using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HyggyBackend.Controllers
{
    [Route("api/WareImage")]
    [ApiController]
    public class WareImageController : ControllerBase
    {
        private readonly IWareImageService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareImageController(IWareImageService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareImageQueryPL, WareImageQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
             .ForMember(dest => dest.WareArticle, opt => opt.MapFrom(src => src.WareArticle))
             .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
             .ForMember(dest => dest.QueryAny, opt => opt.MapFrom(src => src.QueryAny));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareImageDTO>>> GetWares([FromQuery] WareImageQueryPL query)
        {
            try
            {
                IEnumerable<WareImageDTO> collection = null;

                switch (query.SearchParameter)
                {

                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано WareImage.Id для пошуку!", nameof(WareImageQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareImageDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "WareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано WareImage.WareId для пошуку!", nameof(WareImageQueryPL.WareId));
                            }
                            else
                            {
                                collection = await _serv.GetByWareId(query.WareId.Value);
                            }
                        }
                        break;
                    case "WareArticle":
                        {
                            if (query.WareArticle == null)
                            {
                                throw new ValidationException("Не вказано WareImage.WareArticle для пошуку!", nameof(WareImageQueryPL.WareArticle));
                            }
                            else
                            {
                                collection = await _serv.GetByWareArticle(query.WareArticle.Value);
                            }
                        }
                        break;
                    case "Path":
                        {
                            if (query.Path == null)
                            {
                                throw new ValidationException("Не вказано WareImage.Path для пошуку!", nameof(WareImageQueryPL.Path));
                            }
                            else
                            {
                                collection = await _serv.GetByPathSubstring(query.Path);
                            }
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано WareImage.StringIds для пошуку!", nameof(WareImageQueryPL.StringIds));
                            }
                            else
                            {
                                collection = await _serv.GetByStringIds(query.StringIds);
                            }
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryDAL = mapper.Map<WareImageQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryDAL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр WareImageQuery.SearchParameter!", nameof(WareImageQueryPL.SearchParameter));
                        }
                }

                return Ok(collection);
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
        public async Task<ActionResult<WareImageDTO>> CreateWare([FromBody] WareImageDTO wareImage)
        {
            try
            {
                var result = await _serv.Create(wareImage);
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
        public async Task<ActionResult<WareImageDTO>> UpdateWare([FromBody] WareImageDTO wareImage)
        {
            try
            {
                var result = await _serv.Update(wareImage);
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
        public async Task<ActionResult<WareImageDTO>> DeleteWare(long id)
        {
            try
            {
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

    public class WareImageQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? Path { get; set; }
        public long? WareId { get; set; }
        public long? WareArticle { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
    }
}
