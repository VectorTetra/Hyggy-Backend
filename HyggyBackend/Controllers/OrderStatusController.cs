using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/OrderStatus")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _serv;
        IWebHostEnvironment _appEnvironment;

        public OrderStatusController(IOrderStatusService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<OrderStatusQueryPL, OrderStatusQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
             .ForMember(dest => dest.NameSubstring, opt => opt.MapFrom(src => src.NameSubstring))
             .ForMember(dest => dest.DescriptionSubstring, opt => opt.MapFrom(src => src.DescriptionSubstring))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds));

        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDTO>>> GetOrderStatuses([FromQuery] OrderStatusQueryPL orderStatusQueryPL)
        {
            try
            {
                IEnumerable<OrderStatusDTO> collection = null;
                switch (orderStatusQueryPL.SearchParameter)
                {
                    case "Id":
                        {
                            if (orderStatusQueryPL.Id == null)
                            {
                                throw new ValidationException("Не вказано OrderStatus.Id для пошуку!", nameof(OrderStatusQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<OrderStatusDTO> { await _serv.GetById((long)orderStatusQueryPL.Id) };
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (orderStatusQueryPL.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано OrderStatus.NameSubstring для пошуку!", nameof(OrderStatusQueryPL.NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByNameSubstring(orderStatusQueryPL.NameSubstring);
                            }
                        }
                        break;
                    case "Description":
                        {
                            if (orderStatusQueryPL.DescriptionSubstring == null)
                            {
                                throw new ValidationException("Не вказано OrderStatus.DescriptionSubstring для пошуку!", nameof(OrderStatusQueryPL.DescriptionSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByDescriptionSubstring(orderStatusQueryPL.DescriptionSubstring);
                            }
                        }
                        break;
                    case "OrderId":
                        {
                            if (orderStatusQueryPL.OrderId == null)
                            {
                                throw new ValidationException("Не вказано OrderStatus.OrderId для пошуку!", nameof(OrderStatusQueryPL.OrderId));
                            }
                            else
                            {
                                collection = await _serv.GetByOrderId((long)orderStatusQueryPL.OrderId);
                            }
                        }
                        break;
                    case "StringIds":
                        {
                            if (orderStatusQueryPL.StringIds == null)
                            {
                                throw new ValidationException("Не вказано OrderStatus.StringIds для пошуку!", nameof(OrderStatusQueryPL.StringIds));
                            }
                            else
                            {
                                collection = await _serv.GetByStringIds(orderStatusQueryPL.StringIds);
                            }
                        }
                        break;
                    case "Paged":
                        {
                            if (orderStatusQueryPL.PageNumber == null || orderStatusQueryPL.PageSize == null)
                            {
                                throw new ValidationException("Не вказано OrderStatus.PageNumber або OrderStatus.PageSize для пошуку!", nameof(OrderStatusQueryPL.PageNumber));
                            }
                            else
                            {
                                collection = await _serv.GetPaged(orderStatusQueryPL.PageNumber.Value, orderStatusQueryPL.PageSize.Value);
                            }
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var orderStatusQueryBLL = mapper.Map<OrderStatusQueryBLL>(orderStatusQueryPL);
                            collection = await _serv.GetByQuery(orderStatusQueryBLL);
                        }
                        break;

                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр orderStatusQueryPL.SearchParameter!", nameof(orderStatusQueryPL.SearchParameter));
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
        public async Task<ActionResult<OrderStatusDTO>> CreateOrderStatus([FromBody] OrderStatusDTO orderStatus)
        {
            try
            {
                if (orderStatus == null)
                {
                    throw new ValidationException("Не вказано OrderStatus для створення!", nameof(OrderStatusDTO));
                }
                var createdOrderStatus = await _serv.Create(orderStatus);
                return createdOrderStatus;
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
        public async Task<ActionResult<OrderStatusDTO>> UpdateOrderStatus([FromBody] OrderStatusDTO orderStatus)
        {
            try
            {
                if (orderStatus == null)
                {
                    throw new ValidationException("Не вказано OrderStatus для оновлення!", nameof(OrderStatusDTO));
                }
                var updatedOrderStatus = await _serv.Update(orderStatus);
                return updatedOrderStatus;
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
        public async Task<ActionResult<OrderStatusDTO>> DeleteOrderStatus(long id)
        {
            try
            {
                var deletedOrderStatus = await _serv.Delete(id);
                return Ok(deletedOrderStatus);
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

    public class OrderStatusQueryPL
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}