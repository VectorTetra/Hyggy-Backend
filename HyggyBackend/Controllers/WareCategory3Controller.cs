using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareCategory3")]
    [ApiController]
    public class WareCategory3Controller : ControllerBase
    {
        private readonly IWareCategory3Service _serv;
        IWebHostEnvironment _appEnvironment;

        public WareCategory3Controller(IWareCategory3Service serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareCategory3QueryPL, WareCategory3QueryBLL>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("NameSubstring", opt => opt.MapFrom(c => c.NameSubstring))
            .ForMember("JSONStructureFilePathSubstring", opt => opt.MapFrom(c => c.JSONStructureFilePathSubstring))
            .ForMember("WareCategory1Id", opt => opt.MapFrom(c => c.WareCategory1Id))
            .ForMember("WareCategory1NameSubstring", opt => opt.MapFrom(c => c.WareCategory1NameSubstring))
            .ForMember("WareCategory2Id", opt => opt.MapFrom(c => c.WareCategory2Id))
            .ForMember("WareCategory2NameSubstring", opt => opt.MapFrom(c => c.WareCategory2NameSubstring))
            .ForMember("WareId", opt => opt.MapFrom(c => c.WareId))
            .ForMember("WareNameSubstring", opt => opt.MapFrom(c => c.WareNameSubstring))
            .ForMember("WareArticle", opt => opt.MapFrom(c => c.WareArticle))
            .ForMember("WareDescriptionSubstring", opt => opt.MapFrom(c => c.WareDescriptionSubstring))
            .ForMember("PageSize", opt => opt.MapFrom(c => c.PageSize))
            .ForMember("PageNumber", opt => opt.MapFrom(c => c.PageNumber));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareCategory3DTO>>> GetWares([FromQuery] WareCategory3QueryPL query)
        {
            try
            {
                IEnumerable<WareCategory3DTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "GetById":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.Id для пошуку!", nameof(WareCategory3QueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareCategory3DTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (query.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.NameSubstring для пошуку!", nameof(WareCategory3QueryPL.NameSubstring));
                            }
                            collection = await _serv.GetByNameSubstring(query.NameSubstring);
                        }
                        break;
                    case "GetByJSONStructureFilePath":
                        {
                            if (query.JSONStructureFilePathSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.JSONStructureFilePathSubstring для пошуку!", nameof(WareCategory3QueryPL.JSONStructureFilePathSubstring));
                            }
                            collection = await _serv.GetByJSONStructureFilePathSubstring(query.JSONStructureFilePathSubstring);
                        }
                        break;
                    case "GetByWareCategory1Id":
                        {
                            if (query.WareCategory1Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareCategory1Id для пошуку!", nameof(WareCategory3QueryPL.WareCategory1Id));
                            }
                            collection = await _serv.GetByWareCategory1Id(query.WareCategory1Id.Value);
                        }
                        break;
                    case "GetByWareCategory1Name":
                        {
                            if (query.WareCategory1NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareCategory1NameSubstring для пошуку!", nameof(WareCategory3QueryPL.WareCategory1NameSubstring));
                            }
                            collection = await _serv.GetByWareCategory1NameSubstring(query.WareCategory1NameSubstring);
                        }
                        break;
                    case "GetByWareCategory2Id":
                        {
                            if (query.WareCategory2Id == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareCategory2Id для пошуку!", nameof(WareCategory3QueryPL.WareCategory2Id));
                            }
                            collection = await _serv.GetByWareCategory2Id(query.WareCategory2Id.Value);
                        }
                        break;
                    case "GetByWareCategory2Name":
                        {
                            if (query.WareCategory2NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareCategory2NameSubstring для пошуку!", nameof(WareCategory3QueryPL.WareCategory2NameSubstring));
                            }
                            collection = await _serv.GetByWareCategory2NameSubstring(query.WareCategory2NameSubstring);
                        }
                        break;
                    case "GetByWareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareId для пошуку!", nameof(WareCategory3QueryPL.WareId));
                            }
                            collection = await _serv.GetByWareId(query.WareId.Value);
                        }
                        break;
                    case "GetByWareArticle":
                        {
                            if (query.WareArticle == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareArticle для пошуку!", nameof(WareCategory3QueryPL.WareArticle));
                            }
                            collection = await _serv.GetByWareArticle(query.WareArticle.Value);
                        }
                        break;
                    case "GetByWareName":
                        {
                            if (query.WareNameSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareNameSubstring для пошуку!", nameof(WareCategory3QueryPL.WareNameSubstring));
                            }
                            collection = await _serv.GetByWareNameSubstring(query.WareNameSubstring);
                        }
                        break;
                    case "GetByWareDescription":
                        {
                            if (query.WareDescriptionSubstring == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.WareDescriptionSubstring для пошуку!", nameof(WareCategory3QueryPL.WareDescriptionSubstring));
                            }
                            collection = await _serv.GetByWareDescriptionSubstring(query.WareDescriptionSubstring);
                        }
                        break;
                    case "GetPaged":
                        {
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.PageSize для пошуку!", nameof(WareCategory3QueryPL.PageSize));
                            }
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано WareCategory3DTO.PageNumber для пошуку!", nameof(WareCategory3QueryPL.PageNumber));
                            }
                            collection = await _serv.GetPagedCategories(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "GetByQuery":
                        {
                            WareCategory3QueryBLL queryBLL = config.CreateMapper().Map<WareCategory3QueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр WareCategory3QueryPL.SearchParameter!", nameof(WareCategory3QueryPL.SearchParameter));
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
        public async Task<ActionResult<WareCategory3DTO>> Create([FromBody] WareCategory3DTO category3DTO)
        {
            try
            {
                if (category3DTO == null)
                {
                    throw new ValidationException("Не вказано WareCategory3DTO для створення!", nameof(WareDTO));
                }
                var result = await _serv.Create(category3DTO);
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
        public async Task<ActionResult<WareCategory3DTO>> Update([FromBody] WareCategory3DTO category3DTO)
        {
            try
            {
                if (category3DTO == null)
                {
                    throw new ValidationException("Не вказано WareCategory3DTO для оновлення!", nameof(WareDTO));
                }
                var result = await _serv.Update(category3DTO);
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
        public async Task<ActionResult<WareCategory3DTO>> Delete([FromBody] long id)
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

    public class WareCategory3QueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? JSONStructureFilePathSubstring { get; set; }
        public long? WareCategory1Id { get; set; }
        public string? WareCategory1NameSubstring { get; set; }
        public long? WareCategory2Id { get; set; }
        public string? WareCategory2NameSubstring { get; set; }
        public long? WareId { get; set; }
        public string? WareNameSubstring { get; set; }
        public long? WareArticle { get; set; }
        public string? WareDescriptionSubstring { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
