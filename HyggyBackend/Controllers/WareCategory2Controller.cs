using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareCategory2")]
    [ApiController]
    public class WareCategory2Controller : ControllerBase
    {
        private readonly IWareCategory2Service _serv;
        IWebHostEnvironment _appEnvironment;

        public WareCategory2Controller(IWareCategory2Service serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareCategory2QueryPL, WareCategory2QueryBLL>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("NameSubstring", opt => opt.MapFrom(c => c.NameSubstring))
            .ForMember("WareCategory1Id", opt => opt.MapFrom(c => c.WareCategory1Id))
            .ForMember("WareCategory1NameSubstring", opt => opt.MapFrom(c => c.WareCategory1NameSubstring))
            .ForMember("WareCategory3Id", opt => opt.MapFrom(c => c.WareCategory3Id))
            .ForMember("WareCategory3NameSubstring", opt => opt.MapFrom(c => c.WareCategory3NameSubstring))
            .ForMember("PageSize", opt => opt.MapFrom(c => c.PageSize))
            .ForMember("PageNumber", opt => opt.MapFrom(c => c.PageNumber))
            .ForMember("StringIds", opt => opt.MapFrom(c => c.StringIds))
            .ForMember("Sorting", opt => opt.MapFrom(c => c.Sorting));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareCategory2DTO>>> GetWareCategory2([FromQuery] WareCategory2QueryPL wareCategory2Query)
        {
            try
            {
                IEnumerable<WareCategory2DTO?> collection = null;
                switch (wareCategory2Query.SearchParameter)
                {
                    case "Id":
                        {
                            if (wareCategory2Query.Id == null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(WareCategory2QueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareCategory2DTO> { await _serv.GetById((long)wareCategory2Query.Id) };
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (wareCategory2Query.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано NameSubstring для пошуку!", nameof(wareCategory2Query.NameSubstring));
                            }
                            collection = await _serv.GetByNameSubstring(wareCategory2Query.NameSubstring);
                        }
                        break;
                    case "WareCategory1Name":
                        {
                            if (wareCategory2Query.WareCategory1NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory1NameSubstring для пошуку!", nameof(wareCategory2Query.WareCategory1NameSubstring));
                            }
                            collection = await _serv.GetByWareCategory1NameSubstring(wareCategory2Query.WareCategory1NameSubstring);
                        }
                        break;
                    case "WareCategory3Name":
                        {
                            if (wareCategory2Query.WareCategory3NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3NameSubstring для пошуку!", nameof(wareCategory2Query.WareCategory3NameSubstring));
                            }
                            collection = await _serv.GetByWareCategory3NameSubstring(wareCategory2Query.WareCategory3NameSubstring);
                        }
                        break;
                    case "WareCategory1Id":
                        {
                            if (wareCategory2Query.WareCategory1Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory1Id для пошуку!", nameof(wareCategory2Query.WareCategory1Id));
                            }
                        }
                        break;
                    case "WareCategory3Id":
                        {
                            if (wareCategory2Query.WareCategory3Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3Id для пошуку!", nameof(wareCategory2Query.WareCategory3Id));
                            }
                        }
                        break;
                    case "StringIds":
                        {
                            if (wareCategory2Query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано StringIds для пошуку!", nameof(wareCategory2Query.StringIds));
                            }
                            collection = await _serv.GetByStringIds(wareCategory2Query.StringIds);
                        }
                        break;
                    case "Paged":
                        {
                            if (wareCategory2Query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано PageSize для пошуку!", nameof(wareCategory2Query.PageSize));
                            }
                            if (wareCategory2Query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано PageNumber для пошуку!", nameof(wareCategory2Query.PageNumber));
                            }
                            collection = await _serv.GetPagedCategories((int)wareCategory2Query.PageNumber, (int)wareCategory2Query.PageSize);
                        }
                        break;
                    case "Query":
                        {
                            collection = await _serv.GetByQuery(config.CreateMapper().Map<WareCategory2QueryBLL>(wareCategory2Query));
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Не вказано параметр для пошуку!", nameof(wareCategory2Query.SearchParameter));
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
        public async Task<ActionResult<WareCategory2DTO>> Create([FromBody] WareCategory2DTO category2DTO)
        {
            try
            {
                if (category2DTO == null)
                {
                    throw new ValidationException("Не вказано WareCategory2DTO для створення!", nameof(WareDTO));
                }
                var result = await _serv.Create(category2DTO);
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
        public async Task<ActionResult<WareCategory2DTO>> Update([FromBody] WareCategory2DTO category2DTO)
        {
            try
            {
                if (category2DTO == null)
                {
                    throw new ValidationException("Не вказано WareCategory2DTO для оновлення!", nameof(WareDTO));
                }
                var result = await _serv.Update(category2DTO);
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
        public async Task<ActionResult<WareCategory2DTO>> Delete([FromBody] long id)
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

    public class WareCategory2QueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public long? WareCategory1Id { get; set; }
        public string? WareCategory1NameSubstring { get; set; }
        public long? WareCategory3Id { get; set; }
        public string? WareCategory3NameSubstring { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
