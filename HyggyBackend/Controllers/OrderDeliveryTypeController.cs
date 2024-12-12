using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDeliveryTypeController : ControllerBase
    {
        private readonly IOrderDeliveryTypeService _serv;
        IWebHostEnvironment _appEnvironment;

        public OrderDeliveryTypeController(IOrderDeliveryTypeService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<OrderDeliveryTypeQueryPL, OrderDeliveryTypeQueryBLL>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
            .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
            .ForMember(dest => dest.MinDeliveryTimeInDays, opt => opt.MapFrom(src => src.MinDeliveryTimeInDays))
            .ForMember(dest => dest.MaxDeliveryTimeInDays, opt => opt.MapFrom(src => src.MaxDeliveryTimeInDays))
            .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
            .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
            .ForMember(dest => dest.QueryAny, opt => opt.MapFrom(src => src.QueryAny))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));


        });
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDeliveryTypeDTO>>> GetOrders([FromQuery] OrderDeliveryTypeQueryPL orderQueryPL)
        {
            try
            {
                IEnumerable<OrderDeliveryTypeDTO> collection = null;
                switch (orderQueryPL.SearchParameter)
                {

                    case "Id":
                        {
                            if (orderQueryPL.Id == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.Id для пошуку!", nameof(OrderDeliveryTypeQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<OrderDeliveryTypeDTO> { await _serv.GetById((long)orderQueryPL.Id) };
                            }
                        }
                        break;
                    case "OrderId":
                        {
                            if (orderQueryPL.OrderId == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.OrderId для пошуку!", nameof(OrderDeliveryTypeQueryPL.OrderId));
                            }
                            else
                            {
                                collection = await _serv.GetByOrderId((long)orderQueryPL.OrderId);
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (orderQueryPL.Name == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.Name для пошуку!", nameof(OrderDeliveryTypeQueryPL.Name));
                            }
                            else
                            {
                                collection = await _serv.GetByName(orderQueryPL.Name);
                            }
                        }
                        break;
                    case "Description":
                        {
                            if (orderQueryPL.Description == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.Description для пошуку!", nameof(OrderDeliveryTypeQueryPL.Description));
                            }
                            else
                            {
                                collection = await _serv.GetByDescription(orderQueryPL.Description);
                            }
                        }
                        break;
                    case "PriceRange":
                        {
                            if (orderQueryPL.MinPrice == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.MinPrice для пошуку!", nameof(OrderDeliveryTypeQueryPL.MinPrice));
                            }
                            else if (orderQueryPL.MaxPrice == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.MaxPrice для пошуку!", nameof(OrderDeliveryTypeQueryPL.MaxPrice));
                            }
                            else
                            {
                                collection = await _serv.GetByPriceRange(orderQueryPL.MinPrice.Value, orderQueryPL.MaxPrice.Value);
                            }
                        }
                        break;
                    case "DeliveryTimeInDaysRange":
                        {
                            if (orderQueryPL.MinDeliveryTimeInDays == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.MinDeliveryTimeInDays для пошуку!", nameof(OrderDeliveryTypeQueryPL.MinDeliveryTimeInDays));
                            }
                            else if (orderQueryPL.MaxDeliveryTimeInDays == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.MaxDeliveryTimeInDays для пошуку!", nameof(OrderDeliveryTypeQueryPL.MaxDeliveryTimeInDays));
                            }
                            else
                            {
                                collection = await _serv.GetByDeliveryTimeInDaysRange(orderQueryPL.MinDeliveryTimeInDays.Value, orderQueryPL.MaxDeliveryTimeInDays.Value);
                            }
                        }
                        break;
                    case "StringIds":
                        {
                            if (orderQueryPL.StringIds == null)
                            {
                                throw new ValidationException("Не вказано OrderDeliveryType.StringIds для пошуку!", nameof(OrderDeliveryTypeQueryPL.StringIds));
                            }
                            else
                            {
                                collection = await _serv.GetByStringIds(orderQueryPL.StringIds);
                            }
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var orderQueryBLL = mapper.Map<OrderDeliveryTypeQueryBLL>(orderQueryPL);
                            collection = await _serv.GetByQuery(orderQueryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр OrderDeliveryType.SearchParameter!", nameof(orderQueryPL.SearchParameter));
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
        public async Task<ActionResult<OrderDeliveryTypeDTO>> CreateOrder([FromBody] OrderDeliveryTypeDTO orderDeliveryTypeDTO)
        {
            try
            {
                if (orderDeliveryTypeDTO == null)
                {
                    throw new ValidationException("Не вказано orderDeliveryTypeDTO для створення!", nameof(OrderDeliveryTypeDTO));
                }
                var createdOrderDeliveryTypeDTO = await _serv.Create(orderDeliveryTypeDTO);
                return createdOrderDeliveryTypeDTO;
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
        public async Task<ActionResult<OrderDeliveryTypeDTO>> UpdateOrder([FromBody] OrderDeliveryTypeDTO orderDeliveryTypeDTO)
        {
            try
            {
                if (orderDeliveryTypeDTO == null)
                {
                    throw new ValidationException("Не вказано orderDeliveryTypeDTO для оновлення!", nameof(OrderDeliveryTypeDTO));
                }
                var updatedOrderDeliveryType = await _serv.Update(orderDeliveryTypeDTO);
                return Ok(updatedOrderDeliveryType);
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
        public async Task<ActionResult<OrderDeliveryTypeDTO>> DeleteOrder(long id)
        {
            try
            {
                var deletedOrderDeliveryType = await _serv.Delete(id);
                return Ok(deletedOrderDeliveryType);
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

    public class OrderDeliveryTypeQueryPL
    {
        public string? SearchParameter { get; set; } // Вибраний критерій пошуку
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public int? MinDeliveryTimeInDays { get; set; }
        public int? MaxDeliveryTimeInDays { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? QueryAny { get; set; }
    }
}
