using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HyggyBackend.Controllers
{
    [Route("api/OrderItem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _serv;
        IWebHostEnvironment _appEnvironment;
        public OrderItemController(IOrderItemService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<OrderItemQueryPL, OrderItemQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
             .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
             .ForMember(dest => dest.PriceHistoryId, opt => opt.MapFrom(src => src.PriceHistoryId))
             .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
             .ForMember(dest => dest.QueryAny, opt => opt.MapFrom(src => src.QueryAny));

        });
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItem([FromQuery] OrderItemQueryPL query)
        {
            try
            {
                IEnumerable<OrderItemDTO> collection = null;
                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id is null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(query.Id));
                            }
                            else
                            {
                                var proto = await _serv.GetById((long)query.Id);
                                if (proto != null)
                                {
                                    collection = new List<OrderItemDTO> { proto };
                                }
                            }
                        }
                        break;
                    case "OrderId":
                        {
                            if (query.OrderId is null)
                            {
                                throw new ValidationException("Не вказано OrderId для пошуку!", nameof(query.OrderId));
                            }
                            collection = await _serv.GetByOrderId((long)query.OrderId);
                        }
                        break;
                    case "WareId":
                        {
                            if (query.WareId is null)
                            {
                                throw new ValidationException("Не вказано WareId для пошуку!", nameof(query.WareId));
                            }
                            collection = await _serv.GetByWareId((long)query.WareId);
                        }
                        break;
                    case "Count":
                        {
                            if (query.Count is null)
                            {
                                throw new ValidationException("Не вказано Count для пошуку!", nameof(query.Count));
                            }
                            collection = await _serv.GetByCount((short)query.Count);
                        }
                        break;
                    case "PriceHistoryId":
                        {
                            if (query.PriceHistoryId is null)
                            {
                                throw new ValidationException("Не вказано PriceHistoryId для пошуку!", nameof(query.PriceHistoryId));
                            }
                            collection = await _serv.GetByPriceHistoryId((long)query.PriceHistoryId);
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds is null)
                            {
                                throw new ValidationException("Не вказано StringIds для пошуку!", nameof(query.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber is null)
                            {
                                throw new ValidationException("Не вказано PageNumber для пошуку!", nameof(query.PageNumber));
                            }
                            if (query.PageSize is null)
                            {
                                throw new ValidationException("Не вказано PageSize для пошуку!", nameof(query.PageSize));
                            }
                            collection = await _serv.GetPaged((int)query.PageNumber, (int)query.PageSize);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<OrderItemQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        throw new ValidationException("Невідомий параметр пошуку!", nameof(query.SearchParameter));
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
        public async Task<ActionResult<OrderItemDTO>> Create(OrderItemDTO orderItemDTO)
        {
            try
            {
                var dto = await _serv.Create(orderItemDTO);
                return Ok(dto);
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
        public async Task<ActionResult<OrderItemDTO>> Update(OrderItemDTO orderItemDTO)
        {
            try
            {
                var dto = await _serv.Update(orderItemDTO);
                return Ok(dto);
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
        public async Task<ActionResult<OrderItemDTO>> Delete(long id)
        {
            try
            {
                var dto = await _serv.Delete(id);
                return Ok(dto);
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
    public class OrderItemQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public long? WareId { get; set; }
        public long? PriceHistoryId { get; set; }
        public int? Count { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
    }
}
