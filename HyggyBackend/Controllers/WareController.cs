using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.BLL.Services;
using HyggyBackend.DAL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace HyggyBackend.Controllers
{
    public class WareController : ControllerBase
    {
        private readonly IWareService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareController(IWareService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareQueryPL, WareQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article))
             .ForMember(dest => dest.Category1Id, opt => opt.MapFrom(src => src.Category1Id))
             .ForMember(dest => dest.Category2Id, opt => opt.MapFrom(src => src.Category2Id))
             .ForMember(dest => dest.Category3Id, opt => opt.MapFrom(src => src.Category3Id))
             .ForMember(dest => dest.NameSubstring, opt => opt.MapFrom(src => src.NameSubstring))
             .ForMember(dest => dest.DescriptionSubstring, opt => opt.MapFrom(src => src.DescriptionSubstring))
             .ForMember(dest => dest.Category1NameSubstring, opt => opt.MapFrom(src => src.Category1NameSubstring))
             .ForMember(dest => dest.Category2NameSubstring, opt => opt.MapFrom(src => src.Category2NameSubstring))
             .ForMember(dest => dest.Category3NameSubstring, opt => opt.MapFrom(src => src.Category3NameSubstring))
             .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
             .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
             .ForMember(dest => dest.MinDiscount, opt => opt.MapFrom(src => src.MinDiscount))
             .ForMember(dest => dest.MaxDiscount, opt => opt.MapFrom(src => src.MaxDiscount))
             .ForMember(dest => dest.IsDeliveryAvailable, opt => opt.MapFrom(src => src.IsDeliveryAvailable))
             .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
             .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName))
             .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.StatusDescription))
             .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath));

        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareDTO>>> GetWares([FromQuery] WareQueryPL query)
        {
            try
            {
                IEnumerable<WareDTO> collection = null;

                switch (query.SearchParameter)  
                {
                    case "GetById":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Id для пошуку!", nameof(WareQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "GetByArticle":
                        {
                            if (query.Article == null)
                            {
                                throw new ValidationException("Не вказано Ware.Article для пошуку!", nameof(WareQueryPL.Article));
                            }
                            else
                            {
                                collection = new List<WareDTO> { await _serv.GetByArticle(query.Article.Value) };
                            }
                        }
                        break;
                    case "GetByCategory1Id":
                        {
                            if (query.Category1Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category1Id для пошуку!", nameof(WareQueryPL.Category1Id));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory1Id(query.Category1Id.Value);
                            }
                        }
                        break;
                    case "GetByCategory2Id":
                        {
                            if (query.Category2Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category2Id для пошуку!", nameof(WareQueryPL.Category2Id));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory2Id(query.Category2Id.Value);
                            }
                        }
                        break;
                    case "GetByCategory3Id":
                        {
                            if (query.Category3Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category3Id для пошуку!", nameof(WareQueryPL.Category3Id));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory3Id(query.Category3Id.Value);
                            }
                        }
                        break;
                    case "GetByNameSubstring":
                        {
                            if (query.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Name для пошуку!", nameof(WareQueryPL.NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByNameSubstring(query.NameSubstring);
                            }
                        }
                        break;
                    case "GetByDescriptionSubstring":
                        {
                            if (query.DescriptionSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Description для пошуку!", nameof(WareQueryPL.DescriptionSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByDescriptionSubstring(query.DescriptionSubstring);
                            }
                        }
                        break;
                    case "GetByCategory1NameSubstring":
                        {
                            if (query.Category1NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category1Name для пошуку!", nameof(WareQueryPL.Category1NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory1NameSubstring(query.Category1NameSubstring);
                            }
                        }
                        break;
                    case "GetByCategory2NameSubstring":
                        {
                            if (query.Category2NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category2Name для пошуку!", nameof(WareQueryPL.Category2NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory2NameSubstring(query.Category2NameSubstring);
                            }
                        }
                        break;
                    case "GetByCategory3NameSubstring":
                        {
                            if (query.Category3NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category3Name для пошуку!", nameof(WareQueryPL.Category3NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory3NameSubstring(query.Category3NameSubstring);
                            }
                        }
                        break;
                    case "GetByPrice":
                        {
                            if (query.MinPrice == null )
                            {
                                throw new ValidationException("Не вказано Ware.MinPrice для пошуку!", nameof(WareQueryPL.MinPrice));
                            }
                            else if (query.MaxPrice == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxPrice для пошуку!", nameof(WareQueryPL.MaxPrice));
                            }

                            collection = await _serv.GetByPriceRange(query.MinPrice.Value, query.MaxPrice.Value);
                        }
                        break;
                    case "GetByDiscount":
                        {

                            if (query.MinDiscount == null)
                            {
                                throw new ValidationException("Не вказано Ware.MinDiscount для пошуку!", nameof(WareQueryPL.MinDiscount));
                            }
                            else if (query.MaxDiscount == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxDiscount для пошуку!", nameof(WareQueryPL.MaxDiscount));
                            }

                            collection = await _serv.GetByDiscountRange(query.MinDiscount.Value, query.MaxDiscount.Value);
                        }
                        break;
                    case "GetByIsDeliveryAvailable":
                        {
                            if (query.IsDeliveryAvailable == null)
                            {
                                throw new ValidationException("Не вказано Ware.IsDeliveryAvailable для пошуку!", nameof(WareQueryPL.IsDeliveryAvailable));
                            }
                            else
                            {
                                collection = await _serv.GetByIsDeliveryAvailable(query.IsDeliveryAvailable.Value);
                            }
                        }
                        break;
                    case "GetByStatusId":
                        {
                            if (query.StatusId == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusId для пошуку!", nameof(WareQueryPL.StatusId));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusId(query.StatusId.Value);
                            }
                        }
                        break;
                    case "GetByStatusName":
                        {
                            if (query.StatusName == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusName для пошуку!", nameof(WareQueryPL.StatusName));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusNameSubstring(query.StatusName);
                            }
                        }
                        break;
                    case "GetByStatusDescription":
                        {
                            if (query.StatusDescription == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusDescription для пошуку!", nameof(WareQueryPL.StatusDescription));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusDescriptionSubstring(query.StatusDescription);
                            }
                        }
                        break;
                    case "GetByImagePath":
                        {
                            if (query.ImagePath == null)
                            {
                                throw new ValidationException("Не вказано Ware.ImagePath для пошуку!", nameof(WareQueryPL.ImagePath));
                            }
                            else
                            {
                                collection = await _serv.GetByImagePathSubstring(query.ImagePath);
                            }
                        }
                        break;
                    case "GetByQuery":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<WareQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр WareQuery.SearchParameter!", nameof(WareQueryPL.SearchParameter));
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
        public async Task<ActionResult<WareDTO>> CreateWare([FromBody] WareDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано Ware для створення!", nameof(WareDTO));
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<WareDTO>> UpdateWare([FromBody] WareDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано Ware для оновлення!", nameof(WareDTO));
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<WareDTO>> DeleteWare(long id)
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
                return StatusCode(500, ex.Message);
            }
        }



    }

    public class WareQueryPL
    {
        public string? SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? Article { get; set; }
        public long? Category1Id { get; set; }
        public long? Category2Id { get; set; }
        public long? Category3Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
        public string? Category1NameSubstring { get; set; }
        public string? Category2NameSubstring { get; set; }
        public string? Category3NameSubstring { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinDiscount { get; set; }
        public float? MaxDiscount { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public string? ImagePath { get; set; }
    }

}
