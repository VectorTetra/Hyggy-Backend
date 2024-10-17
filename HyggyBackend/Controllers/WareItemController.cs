using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/WareItem")]
    [ApiController]
    public class WareItemController : ControllerBase
    {
        private readonly IWareItemService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareItemController(IWareItemService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareItemQueryPL, WareItemQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article))
             .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
             .ForMember(dest => dest.WareName, opt => opt.MapFrom(src => src.WareName))
             .ForMember(dest => dest.WareDescription, opt => opt.MapFrom(src => src.WareDescription))
             .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
             .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
             .ForMember(dest => dest.MinDiscount, opt => opt.MapFrom(src => src.MinDiscount))
             .ForMember(dest => dest.MaxDiscount, opt => opt.MapFrom(src => src.MaxDiscount))
             .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
             .ForMember(dest => dest.WareCategory3Id, opt => opt.MapFrom(src => src.WareCategory3Id))
             .ForMember(dest => dest.WareCategory2Id, opt => opt.MapFrom(src => src.WareCategory2Id))
             .ForMember(dest => dest.WareCategory1Id, opt => opt.MapFrom(src => src.WareCategory1Id))
             .ForMember(dest => dest.WareImageId, opt => opt.MapFrom(src => src.WareImageId))
             .ForMember(dest => dest.PriceHistoryId, opt => opt.MapFrom(src => src.PriceHistoryId))
             .ForMember(dest => dest.OrderItemId, opt => opt.MapFrom(src => src.OrderItemId))
             .ForMember(dest => dest.IsDeliveryAvailable, opt => opt.MapFrom(src => src.IsDeliveryAvailable))
             .ForMember(dest => dest.StorageId, opt => opt.MapFrom(src => src.StorageId))
             .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.ShopId))
             .ForMember(dest => dest.MinQuantity, opt => opt.MapFrom(src => src.MinQuantity))
             .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.MaxQuantity))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting));
        });

        [HttpPost]
        public async Task<ActionResult<WareItemDTO>> CreateWare([FromBody] WareItemDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано WareItem для створення!", nameof(WareItemDTO));
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
        public async Task<ActionResult<WareItemDTO>> UpdateWare([FromBody] WareItemDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано WareItem для оновлення!", nameof(WareItemDTO));
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<WareItemDTO>> DeleteWare(long id)
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareItemDTO>>> GetWares([FromQuery] WareItemQueryPL query)
        {
            try
            {
                IEnumerable<WareItemDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Id для пошуку!", nameof(WareItemQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareItemDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "Article":
                        {
                            if (query.Article == null)
                            {
                                throw new ValidationException("Не вказано Ware.Article для пошуку!", nameof(WareItemQueryPL.Article));
                            }
                            else
                            {
                                collection = await _serv.GetByArticle(query.Article.Value) ;
                            }
                        }
                        break;
                    case "WareId":
                        {
                            if (query.WareId == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareId для пошуку!", nameof(WareItemQueryPL.WareId));
                            }
                            else
                            {
                                collection = await _serv.GetByWareId(query.WareId.Value);
                            }
                        }
                        break;
                    case "WareName":
                        {
                            if (query.WareName == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareName для пошуку!", nameof(WareItemQueryPL.WareName));
                            }
                            else
                            {
                                collection = await _serv.GetByWareName(query.WareName);
                            }
                        }
                        break;
                    case "WareDescription":
                        {
                            if (query.WareDescription == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareDescription для пошуку!", nameof(WareItemQueryPL.WareDescription));
                            }
                            else
                            {
                                collection = await _serv.GetByWareDescription(query.WareDescription);
                            }
                        }
                        break;
                    case "PriceRange":
                        {
                            if (query.MinPrice == null)
                            {
                                throw new ValidationException("Не вказано Ware.MinPrice для пошуку!", nameof(WareItemQueryPL.MinPrice));
                            }
                            if (query.MaxPrice == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxPrice для пошуку!", nameof(WareItemQueryPL.MaxPrice));
                            }
                            collection = await _serv.GetByWarePriceRange(query.MinPrice.Value, query.MaxPrice.Value);                            
                        }
                        break;
                    case "DiscountRange":
                        {
                            if (query.MinDiscount == null)
                            {
                                throw new ValidationException("Не вказано Ware.MinDiscount для пошуку!", nameof(WareItemQueryPL.MinDiscount));
                            }
                            if (query.MaxDiscount == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxDiscount для пошуку!", nameof(WareItemQueryPL.MaxDiscount));
                            }
                            collection = await _serv.GetByWareDiscountRange(query.MinDiscount.Value, query.MaxDiscount.Value);
                        }
                        break;
                    case "WareStatusId":
                        {
                            if (query.StatusId == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusId для пошуку!", nameof(WareItemQueryPL.StatusId));
                            }
                            collection = await _serv.GetByWareStatusId(query.StatusId.Value);
                        }
                        break;
                    case "WareCategory3Id":
                        {
                            if (query.WareCategory3Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareCategory3Id для пошуку!", nameof(WareItemQueryPL.WareCategory3Id));
                            }
                            collection = await _serv.GetByWareCategory3Id(query.WareCategory3Id.Value);
                        }
                        break;
                    case "WareCategory2Id":
                        {
                            if (query.WareCategory2Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareCategory2Id для пошуку!", nameof(WareItemQueryPL.WareCategory2Id));
                            }
                            collection = await _serv.GetByWareCategory2Id(query.WareCategory2Id.Value);
                        }
                        break;
                    case "WareCategory1Id":
                        {
                            if (query.WareCategory1Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareCategory1Id для пошуку!", nameof(WareItemQueryPL.WareCategory1Id));
                            }
                            collection = await _serv.GetByWareCategory1Id(query.WareCategory1Id.Value);
                        }
                        break;
                    case "WareImageId":
                        {
                            if (query.WareImageId == null)
                            {
                                throw new ValidationException("Не вказано Ware.WareImageId для пошуку!", nameof(WareItemQueryPL.WareImageId));
                            }
                            collection = await _serv.GetByWareImageId(query.WareImageId.Value);
                        }
                        break;
                    case "WarePriceHistoryId":
                        {
                            if (query.PriceHistoryId == null)
                            {
                                throw new ValidationException("Не вказано Ware.PriceHistoryId для пошуку!", nameof(WareItemQueryPL.PriceHistoryId));
                            }
                            collection = await _serv.GetByPriceHistoryId(query.PriceHistoryId.Value);
                        }
                        break;
                    case "OrderItemId":
                        {
                            if (query.OrderItemId == null)
                            {
                                throw new ValidationException("Не вказано Ware.OrderItemId для пошуку!", nameof(WareItemQueryPL.OrderItemId));
                            }
                            collection = await _serv.GetByOrderItemId(query.OrderItemId.Value);
                        }
                        break;
                    case "IsDeliveryAvailable":
                        {
                            if (query.IsDeliveryAvailable == null)
                            {
                                throw new ValidationException("Не вказано Ware.IsDeliveryAvailable для пошуку!", nameof(WareItemQueryPL.IsDeliveryAvailable));
                            }
                            collection = await _serv.GetByIsDeliveryAvailable(query.IsDeliveryAvailable.Value);
                        }
                        break;
                    case "StorageId":
                        {
                            if (query.StorageId == null)
                            {
                                throw new ValidationException("Не вказано Ware.StorageId для пошуку!", nameof(WareItemQueryPL.StorageId));
                            }
                            collection = await _serv.GetByStorageId(query.StorageId.Value);
                        }
                        break;
                    case "ShopId":
                        {
                            if (query.ShopId == null)
                            {
                                throw new ValidationException("Не вказано Ware.ShopId для пошуку!", nameof(WareItemQueryPL.ShopId));
                            }
                            collection = await _serv.GetByShopId(query.ShopId.Value);
                        }
                        break;
                    case "QuantityRange":
                        {
                            if (query.MinQuantity == null)
                            {
                                throw new ValidationException("Не вказано Ware.MinQuantity для пошуку!", nameof(WareItemQueryPL.MinQuantity));
                            }
                            if (query.MaxQuantity == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxQuantity для пошуку!", nameof(WareItemQueryPL.MaxQuantity));
                            }
                            collection = await _serv.GetByQuantityRange(query.MinQuantity.Value, query.MaxQuantity.Value);
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringIds для пошуку!", nameof(WareItemQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано Ware.PageNumber для пошуку!", nameof(WareItemQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано Ware.PageSize для пошуку!", nameof(WareItemQueryPL.PageSize));
                            }
                            collection = await _serv.GetPagedWareItems(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<WareItemQueryBLL>(query);
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
    }

    public class WareItemQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? Article { get; set; }
        public long? WareId { get; set; }
        public string? WareName { get; set; }
        public string? WareDescription { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinDiscount { get; set; }
        public float? MaxDiscount { get; set; }
        public long? StatusId { get; set; }
        public long? WareCategory3Id { get; set; }
        public long? WareCategory2Id { get; set; }
        public long? WareCategory1Id { get; set; }
        public long? WareImageId { get; set; }
        public long? PriceHistoryId { get; set; }
        public long? OrderItemId { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StorageId { get; set; }
        public long? ShopId { get; set; }
        public long? MinQuantity { get; set; }
        public long? MaxQuantity { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
