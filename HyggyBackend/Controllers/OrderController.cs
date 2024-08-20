using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _serv;
        IWebHostEnvironment _appEnvironment;

        public OrderController(IOrderService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<OrderQueryPL, OrderQueryBLL>()
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.MinOrderDate, opt => opt.MapFrom(src => src.MinOrderDate))
            .ForMember(dest => dest.MaxOrderDate, opt => opt.MapFrom(src => src.MaxOrderDate))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName))
            .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.StatusDescription))
            .ForMember(dest => dest.OrderItemId, opt => opt.MapFrom(src => src.OrderItemId))
            .ForMember(dest => dest.WareId, opt => opt.MapFrom(src => src.WareId))
            .ForMember(dest => dest.WarePriceHistoryId, opt => opt.MapFrom(src => src.WarePriceHistoryId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.ShopId));
        });
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders([FromQuery] OrderQueryPL orderQueryPL)
        {
            try
            {
                IEnumerable<OrderDTO> collection = null;
                switch (orderQueryPL.SearchParameter)
                {

                    case "GetById":
                        {
                            if (orderQueryPL.Id == null)
                            {
                                throw new ValidationException("Не вказано Order.Id для пошуку!", nameof(OrderQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<OrderDTO> { await _serv.GetById((long)orderQueryPL.Id) };
                            }
                        }
                        break;

                    case "GetByAddressId":
                        {
                            if (orderQueryPL.AddressId == null)
                            {
                                throw new ValidationException("Не вказано Order.AddressId для пошуку!", nameof(OrderQueryPL.AddressId));
                            }
                            else
                            {
                                collection = await _serv.GetByAddressId((long)orderQueryPL.AddressId);
                            }
                        }
                        break;

                    case "GetByStreet":
                        {
                            if (orderQueryPL.Street == null)
                            {
                                throw new ValidationException("Не вказано Order.Street для пошуку!", nameof(OrderQueryPL.Street));
                            }
                            else
                            {
                                collection = await _serv.GetByStreet(orderQueryPL.Street);
                            }
                        }
                        break;

                    case "GetByHouseNumber":
                        {
                            if (orderQueryPL.HouseNumber == null)
                            {
                                throw new ValidationException("Не вказано Order.HouseNumber для пошуку!", nameof(OrderQueryPL.HouseNumber));
                            }
                            else
                            {
                                collection = await _serv.GetByHouseNumber(orderQueryPL.HouseNumber);
                            }
                        }
                        break;

                    case "GetByCity":
                        {
                            if (orderQueryPL.City == null)
                            {
                                throw new ValidationException("Не вказано Order.City для пошуку!", nameof(OrderQueryPL.City));
                            }
                            else
                            {
                                collection = await _serv.GetByCity(orderQueryPL.City);
                            }
                        }
                        break;

                    case "GetByPostalCode":
                        {
                            if (orderQueryPL.PostalCode == null)
                            {
                                throw new ValidationException("Не вказано Order.PostalCode для пошуку!", nameof(OrderQueryPL.PostalCode));
                            }
                            else
                            {
                                collection = await _serv.GetByPostalCode(orderQueryPL.PostalCode);
                            }
                        }
                        break;

                    case "GetByState":
                        {
                            if (orderQueryPL.State == null)
                            {
                                throw new ValidationException("Не вказано Order.State для пошуку!", nameof(OrderQueryPL.State));
                            }
                            else
                            {
                                collection = await _serv.GetByState(orderQueryPL.State);
                            }
                        }
                        break;

                    case "GetByLatitudeAndLongitude":
                        {
                            if (orderQueryPL.Latitude == null || orderQueryPL.Longitude == null)
                            {
                                throw new ValidationException("Не вказано Order.Latitude або Order.Longitude для пошуку!", nameof(OrderQueryPL.Latitude));
                            }
                            else
                            {
                                collection = await _serv.GetByLatitudeAndLongitude((double)orderQueryPL.Latitude, (double)orderQueryPL.Longitude);
                            }
                        }
                        break;

                    case "GetByOrderDateRange":
                        {
                            if (orderQueryPL.MinOrderDate == null || orderQueryPL.MaxOrderDate == null)
                            {
                                throw new ValidationException("Не вказано Order.MinOrderDate або Order.MaxOrderDate для пошуку!", nameof(OrderQueryPL.MinOrderDate));
                            }
                            else
                            {
                                collection = await _serv.GetByOrderDateRange((DateTime)orderQueryPL.MinOrderDate, (DateTime)orderQueryPL.MaxOrderDate);
                            }
                        }
                        break;

                    case "GetByPhoneSubstring":
                        {
                            if (orderQueryPL.Phone == null)
                            {
                                throw new ValidationException("Не вказано Order.Phone для пошуку!", nameof(OrderQueryPL.Phone));
                            }
                            else
                            {
                                collection = await _serv.GetByPhoneSubstring(orderQueryPL.Phone);
                            }
                        }
                        break;

                    case "GetByCommentSubstring":
                        {
                            if (orderQueryPL.Comment == null)
                            {
                                throw new ValidationException("Не вказано Order.Comment для пошуку!", nameof(OrderQueryPL.Comment));
                            }
                            else
                            {
                                collection = await _serv.GetByCommentSubstring(orderQueryPL.Comment);
                            }
                        }
                        break;

                    case "GetByStatusId":
                        {
                            if (orderQueryPL.StatusId == null)
                            {
                                throw new ValidationException("Не вказано Order.StatusId для пошуку!", nameof(OrderQueryPL.StatusId));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusId((long)orderQueryPL.StatusId);
                            }
                        }
                        break;

                    case "GetByStatusNameSubstring":
                        {
                            if (orderQueryPL.StatusName == null)
                            {
                                throw new ValidationException("Не вказано Order.StatusName для пошуку!", nameof(OrderQueryPL.StatusName));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusNameSubstring(orderQueryPL.StatusName);
                            }
                        }
                        break;

                    case "GetByStatusDescriptionSubstring":
                        {
                            if (orderQueryPL.StatusDescription == null)
                            {
                                throw new ValidationException("Не вказано Order.StatusDescription для пошуку!", nameof(OrderQueryPL.StatusDescription));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusDescriptionSubstring(orderQueryPL.StatusDescription);
                            }
                        }
                        break;

                    case "GetByOrderItemId":
                        {
                            if (orderQueryPL.OrderItemId == null)
                            {
                                throw new ValidationException("Не вказано Order.OrderItemId для пошуку!", nameof(OrderQueryPL.OrderItemId));
                            }
                            else
                            {
                                collection = await _serv.GetByOrderItemId((long)orderQueryPL.OrderItemId);
                            }
                        }
                        break;

                    case "GetByWareId":
                        {
                            if (orderQueryPL.WareId == null)
                            {
                                throw new ValidationException("Не вказано Order.WareId для пошуку!", nameof(OrderQueryPL.WareId));
                            }
                            else
                            {
                                collection = await _serv.GetByWareId((long)orderQueryPL.WareId);
                            }
                        }
                        break;

                    case "GetByWarePriceHistoryId":
                        {
                            if (orderQueryPL.WarePriceHistoryId == null)
                            {
                                throw new ValidationException("Не вказано Order.WarePriceHistoryId для пошуку!", nameof(OrderQueryPL.WarePriceHistoryId));
                            }
                            else
                            {
                                collection = await _serv.GetByWarePriceHistoryId((long)orderQueryPL.WarePriceHistoryId);
                            }
                        }
                        break;

                    case "GetByCustomerId":
                        {
                            if (orderQueryPL.CustomerId == null)
                            {
                                throw new ValidationException("Не вказано Order.CustomerId для пошуку!", nameof(OrderQueryPL.CustomerId));
                            }
                            else
                            {
                                collection = await _serv.GetByCustomerId(orderQueryPL.CustomerId.ToString());
                            }
                        }
                        break;

                    case "GetByShopId":
                        {
                            if (orderQueryPL.ShopId == null)
                            {
                                throw new ValidationException("Не вказано Order.ShopId для пошуку!", nameof(OrderQueryPL.ShopId));
                            }
                            else
                            {
                                collection = await _serv.GetByShopId((long)orderQueryPL.ShopId);
                            }
                        }
                        break;

                    case "GetByQuery":
                        {
                            var mapper = new Mapper(config);
                            var orderQueryBLL = mapper.Map<OrderQueryBLL>(orderQueryPL);
                            collection = await _serv.GetByQuery(orderQueryBLL);
                        }
                        break;

                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр tourNameQuery.SearchParameter!", nameof(orderQueryPL.SearchParameter));
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
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] OrderDTO order)
        {
            try
            {
                if (order == null)
                {
                    throw new ValidationException("Не вказано Order для створення!", nameof(OrderDTO));
                }
                var createdOrder = await _serv.Create(order);
                return createdOrder;
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
        public async Task<ActionResult<OrderDTO>> UpdateOrder([FromBody] OrderDTO order)
        {
            try
            {
                if (order == null)
                {
                    throw new ValidationException("Не вказано Order для оновлення!", nameof(OrderDTO));
                }
                var updatedOrder = await _serv.Update(order);
                return updatedOrder;
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
        public async Task<ActionResult<OrderDTO>> DeleteOrder(long id)
        {
            try
            {
                var deletedOrder = await _serv.Delete(id);
                return Ok(deletedOrder);
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

    public class OrderQueryPL
    {
        public string? SearchParameter { get; set; } // Вибраний критерій пошуку

        public long? Id;
        public long? AddressId;

        // Адреса доставки, розділена на компоненти
        public string? Street { get; set; } // Назва вулиці
        public string? HouseNumber { get; set; } // Номер будинку
        public string? City { get; set; } // Місто
        public string? State { get; set; } // Область або штат
        public string? PostalCode { get; set; } // Поштовий індекс

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Дані про клієнта

        public DateTime? MinOrderDate { get; set; }
        public DateTime? MaxOrderDate { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public long? OrderItemId { get; set; }
        public long? WareId { get; set; }
        public long? WarePriceHistoryId { get; set; }
        public long? CustomerId { get; set; }
        public long? ShopId { get; set; }
    }
}
