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

        public OrderStatusController(IOrderStatusService serv)
        {
            _serv = serv;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<OrderStatusQueryPL, OrderStatusQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.NameSubstring, opt => opt.MapFrom(src => src.NameSubstring))
             .ForMember(dest => dest.DescriptionSubstring, opt => opt.MapFrom(src => src.DescriptionSubstring));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDTO>>> GetOrderStatuses([FromQuery] OrderStatusQueryPL orderStatusQueryPL)
        {
            try
            {
                IEnumerable<OrderStatusDTO> collection = null;
                switch (orderStatusQueryPL.SearchParameter)
                {
                    case "GetById":
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

                    case "GetByNameSubstring":
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

                    case "GetByDescriptionSubstring":
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

                    case "GetByQuery":
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

        [HttpDelete]
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
        public string? SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
    }
}