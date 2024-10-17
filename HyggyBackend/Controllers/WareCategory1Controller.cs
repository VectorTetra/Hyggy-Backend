using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareCategory1")]
    [ApiController]
    public class WareCategory1Controller : ControllerBase
    {
        private readonly IWareCategory1Service _serv;
        IWebHostEnvironment _appEnvironment;

        public WareCategory1Controller(IWareCategory1Service serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareCategory1QueryPL, WareCategory1QueryBLL>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("NameSubstring", opt => opt.MapFrom(c => c.NameSubstring))
            .ForMember("WareCategory2Id", opt => opt.MapFrom(c => c.WareCategory2Id))
            .ForMember("WareCategory2NameSubstring", opt => opt.MapFrom(c => c.WareCategory2NameSubstring))
            .ForMember("WareCategory3Id", opt => opt.MapFrom(c => c.WareCategory3Id))
            .ForMember("WareCategory3NameSubstring", opt => opt.MapFrom(c => c.WareCategory3NameSubstring))
            .ForMember("StringIds", opt => opt.MapFrom(c => c.StringIds))
            .ForMember("Sorting", opt => opt.MapFrom(c => c.Sorting))
            .ForMember("PageSize", opt => opt.MapFrom(c => c.PageSize))
            .ForMember("PageNumber", opt => opt.MapFrom(c => c.PageNumber));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareCategory1DTO>>> GetWareCategory1([FromQuery] WareCategory1QueryPL wareCategory1Query)
        {
            try
            {
                IEnumerable<WareCategory1DTO?> collection = null;
                switch (wareCategory1Query.SearchParameter)
                {
                    case "Id":
                        {
                            if (wareCategory1Query.Id == null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(WareCategory1QueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareCategory1DTO> { await _serv.GetById((long)wareCategory1Query.Id) };
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (wareCategory1Query.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано NameSubstring для пошуку!", nameof(wareCategory1Query.NameSubstring));
                            }
                            collection = await _serv.GetByNameSubstring(wareCategory1Query.NameSubstring);
                        }
                        break;
                    case "WareCategory3Name":
                        {
                            if (wareCategory1Query.WareCategory3NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3NameSubstring для пошуку!", nameof(wareCategory1Query.WareCategory3NameSubstring));
                            }
                            collection = await _serv.GetByWareCategory3NameSubstring(wareCategory1Query.WareCategory3NameSubstring);
                        }
                        break;
                    case "WareCategory3Id":
                        {
                            if (wareCategory1Query.WareCategory3Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3Id для пошуку!", nameof(wareCategory1Query.WareCategory3Id));
                            }
                        }
                        break;
                    case "WareCategory2Name":
                        {
                            if (wareCategory1Query.WareCategory2NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory2NameSubstring для пошуку!", nameof(wareCategory1Query.WareCategory2NameSubstring));
                            }
                            collection = await _serv.GetByWareCategory2NameSubstring(wareCategory1Query.WareCategory2NameSubstring);
                        }
                        break;
                    case "WareCategory2Id":
                        {
                            if (wareCategory1Query.WareCategory2Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory2Id для пошуку!", nameof(wareCategory1Query.WareCategory2Id));
                            }
                            collection = await _serv.GetByWareCategory2Id((long)wareCategory1Query.WareCategory2Id);
                        }
                        break;
                    case "StringIds":
                        {
                            if (wareCategory1Query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано StringIds для пошуку!", nameof(wareCategory1Query.StringIds));
                            }
                            collection = await _serv.GetByStringIds(wareCategory1Query.StringIds);
                        }
                        break;
                    case "Paged":
                        {
                            if (wareCategory1Query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано PageSize для пошуку!", nameof(wareCategory1Query.PageSize));
                            }
                            if (wareCategory1Query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано PageNumber для пошуку!", nameof(wareCategory1Query.PageNumber));
                            }
                            collection = await _serv.GetPagedCategories((int)wareCategory1Query.PageNumber, (int)wareCategory1Query.PageSize);
                        }
                        break;
                    case "Query":
                        {
                            collection = await _serv.GetByQuery(config.CreateMapper().Map<WareCategory1QueryBLL>(wareCategory1Query));
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Не вказано параметр для пошуку!", nameof(wareCategory1Query.SearchParameter));
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WareCategory1DTO>> Create([FromBody] WareCategory1DTO category1DTO)
        {
            try
            {
                if (category1DTO == null)
                {
                    throw new ValidationException("Не вказано WareCategory1DTO для створення!", nameof(WareDTO));
                }
                var result = await _serv.Create(category1DTO);
                return result;
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<WareCategory1DTO>> Update([FromBody] WareCategory1DTO category1DTO)
        {
            try
            {
                if (category1DTO == null)
                {
                    throw new ValidationException("Не вказано WareCategory1DTO для оновлення!", nameof(WareDTO));
                }
                var result = await _serv.Update(category1DTO);
                return result;
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WareCategory1DTO>> Delete([FromBody] long id)
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
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class WareCategory1QueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public long? WareCategory2Id { get; set; }
        public string? WareCategory2NameSubstring { get; set; }
        public long? WareCategory3Id { get; set; }
        public string? WareCategory3NameSubstring { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
