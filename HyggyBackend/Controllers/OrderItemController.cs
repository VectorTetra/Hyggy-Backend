using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/OrderItem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _serv;
        public OrderItemController(IOrderItemService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItem([FromQuery] OrderItemQuery orderItemQuery)
        {
            try
            {
                IEnumerable<OrderItemDTO> collection = null;
                switch (orderItemQuery.SearchParameter)
                {
                    case "Id":
                        {
                            if (orderItemQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(orderItemQuery.Id));
                            }
                            else
                            {
                                collection = new List<OrderItemDTO> { await _serv.GetById((long)orderItemQuery.Id) };
                            }
                        }
                        break;
                    case "OrderId":
                        {
                            if (orderItemQuery.OrderId is null)
                            {
                                throw new ValidationException("Не вказано OrderId для пошуку!", nameof(orderItemQuery.OrderId));
                            }
                            collection = await _serv.GetByOrderId((long)orderItemQuery.OrderId);
                        }
                        break;
                    case "WareId":
                        {
                            if (orderItemQuery.WareId is null)
                            {
                                throw new ValidationException("Не вказано WareId для пошуку!", nameof(orderItemQuery.WareId));
                            }
                            collection = await _serv.GetByWareId((long)orderItemQuery.WareId);
                        }
                        break;
                    case "Count":
                        {
                            if (orderItemQuery.OrderCount is null)
                            {
                                throw new ValidationException("Не вказано Count для пошуку!", nameof(orderItemQuery.OrderCount));
                            }
                            collection = await _serv.GetByCount((short)orderItemQuery.OrderCount);
                        }
                        break;
                    default:
                        throw new ValidationException("Невідомий параметр пошуку!", nameof(orderItemQuery.SearchParameter));
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
                return StatusCode(500, ex.Message);
            }
        }
    }
    public class OrderItemQuery
    {
        public string? SearchParameter { get; set; }

        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public long? WareId { get; set; }
        public int? OrderCount { get; set; }
    }
}
