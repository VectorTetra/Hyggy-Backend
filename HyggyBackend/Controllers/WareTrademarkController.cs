using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareTrademark")]
    [ApiController]
    public class WareTrademarkController : ControllerBase
    {
        private readonly IWareTrademarkService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareTrademarkController(IWareTrademarkService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }
        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareTramedarkQueryPL, WareTrademarkQueryBLL>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
            .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareTrademarkDTO>>> GetWares([FromQuery] WareTramedarkQueryPL query)
        {

            try
            {
                IEnumerable<WareTrademarkDTO> collection = null;
                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано WareTrademark.Id для пошуку!", nameof(WareTramedarkQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareTrademarkDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (query.Name == null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(WareTramedarkQueryPL.Name));
                            }
                            else
                            {
                                collection = await _serv.GetByName(query.Name);
                            }
                        }
                        break;
                    case "WareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано WareId для пошуку!", nameof(WareTramedarkQueryPL.WareId));
                            }
                            else
                            {
                                collection = new List<WareTrademarkDTO> { await _serv.GetByWareId(query.WareId.Value) };
                            }
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано PageNumber для пошуку!", nameof(WareTramedarkQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано PageSize для пошуку!", nameof(WareTramedarkQueryPL.PageSize));
                            }
                            collection = await _serv.GetPagedWareTrademarks(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано StringIds для пошуку!", nameof(WareTramedarkQueryPL.StringIds));
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
                            var queryBLL = mapper.Map<WareTrademarkQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Не вказано SearchParameter для пошуку!", nameof(WareTramedarkQueryPL.SearchParameter));
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
        public async Task<ActionResult<WareTrademarkDTO>> CreateWare([FromBody] WareTrademarkDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано WareTrademark для створення!", nameof(WareTrademarkDTO));
                }
                var result = await _serv.Add(ware);
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
        public async Task<ActionResult<WareTrademarkDTO>> UpdateWare([FromBody] WareTrademarkDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано WareTrademark для оновлення!", nameof(WareTrademarkDTO));
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
        public async Task<ActionResult<WareTrademarkDTO>> DeleteWare(long id)
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

    public class WareTramedarkQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? Name { get; set; }
        public long? WareId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
    }
}
