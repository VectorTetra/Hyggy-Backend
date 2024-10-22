using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AddressController : Controller
	{
        private readonly IAddressService _serv;
        IWebHostEnvironment _appEnvironment;

        public AddressController(IAddressService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
			mc.CreateMap<AddressQueryPL, AddressQueryBLL>()
            .ForMember("Id", opt => opt.MapFrom(src => src.Id))
            .ForMember("Street", opt => opt.MapFrom(src => src.Street))
            .ForMember("HouseNumber", opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember("City", opt => opt.MapFrom(src => src.City))
            .ForMember("State", opt => opt.MapFrom(src => src.State))
            .ForMember("PostalCode", opt => opt.MapFrom(src => src.PostalCode))
            .ForMember("Latitude", opt => opt.MapFrom(src => src.Latitude))
            .ForMember("Longitude", opt => opt.MapFrom(src => src.Longitude))
            .ForMember("ShopId", opt => opt.MapFrom(src => src.ShopId))
            .ForMember("StorageId", opt => opt.MapFrom(src => src.StorageId))
            .ForMember("OrderId", opt => opt.MapFrom(src => src.OrderId))
            .ForMember("PageNumber", opt => opt.MapFrom(src => src.PageNumber))
            .ForMember("PageSize", opt => opt.MapFrom(src => src.PageSize))
            .ForMember("StringIds", opt => opt.MapFrom(src => src.StringIds))
            .ForMember("Sorting", opt => opt.MapFrom(src => src.Sorting))
            .ForMember("QueryAny", opt => opt.MapFrom(src => src.QueryAny));

        });
        [HttpGet]
		public async Task<ActionResult<IEnumerable<AddressDTO>>> Get([FromQuery] AddressQueryPL query)
		{
            try
            {
                IEnumerable<AddressDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.Id для пошуку!", nameof(AddressQueryPL.Id));
                            }
                            else
                            {
                                var address = await _serv.GetByIdAsync(query.Id.Value);

                                if (address == null)
                                {
                                    return NoContent(); // Якщо адреса не знайдена, повертаємо NoContent
                                }

                                collection = new List<AddressDTO> { address }; // Додаємо знайдену адресу до колекції
                            }
                        }
                        break;
                    case "Street":
                        {
                            if (query.Street == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.Street для пошуку!", nameof(AddressQueryPL.Street));
                            }
                            collection = await _serv.GetByStreet(query.Street);
                        }
                        break;
                    case "HouseNumber":
                        {
                            if (query.HouseNumber == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.HouseNumber для пошуку!", nameof(AddressQueryPL.HouseNumber));
                            }
                            collection = await _serv.GetByHouseNumber(query.HouseNumber);
                        }
                        break;
                    case "City":
                        {
                            if (query.City == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.City для пошуку!", nameof(AddressQueryPL.City));
                            }
                            collection = await _serv.GetByCity(query.City);
                        }
                        break;
                    case "State":
                        {
                            if (query.State == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.State для пошуку!", nameof(AddressQueryPL.State));
                            }
                            collection = await _serv.GetByState(query.State);
                        }
                        break;
                    case "PostalCode":
                        {
                            if (query.PostalCode == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.PostalCode для пошуку!", nameof(AddressQueryPL.PostalCode));
                            }
                            collection = await _serv.GetByPostalCode(query.PostalCode);
                        }
                        break;
                    case "LatitudeAndLongitude":
                        {
                            if (query.Latitude == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.Latitude для пошуку!", nameof(AddressQueryPL.Latitude));
                            }
                            if (query.Longitude == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.Longitude для пошуку!", nameof(AddressQueryPL.Longitude));
                            }
                            collection = await _serv.GetByLatitudeAndLongitude(query.Latitude.Value, query.Longitude.Value);
                        }
                        break;
                    case "ShopId":
                        {
                            if (query.ShopId == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.ShopId для пошуку!", nameof(AddressQueryPL.ShopId));
                            }
                            var dTO = await _serv.GetByShopId(query.ShopId.Value);
                            if (dTO != null)
                            {
                                collection = new List<AddressDTO> { dTO };
                            }
                            else
                            { return NoContent(); }
                        }
                        break;
                    case "StorageId":
                        {
                            if (query.StorageId == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.StorageId для пошуку!", nameof(AddressQueryPL.StorageId));
                            }
                            var dTO = await _serv.GetByStorageId(query.StorageId.Value);
                            if (dTO != null)
                            {
                                collection = new List<AddressDTO> { dTO };
                            }
                            else
                            { return NoContent(); }
                        }
                        break;
                    case "OrderId":
                        {
                            if (query.OrderId == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.OrderId для пошуку!", nameof(AddressQueryPL.OrderId));
                            }
                            var dTO = await _serv.GetByOrderId(query.OrderId.Value);
                            if (dTO != null)
                            {
                                collection = new List<AddressDTO> { dTO };
                            }
                            else
                            { return NoContent(); }
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.StringIds для пошуку!", nameof(AddressQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.PageNumber для пошуку!", nameof(AddressQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано AddressQuery.PageSize для пошуку!", nameof(AddressQueryPL.PageSize));
                            }
                            collection = await _serv.GetPaged(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<AddressQueryBLL>(query);
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> CreateWare([FromBody] AddressDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано Ware для створення!", nameof(WareDTO));
                }
                var result = await _serv.CreateAsync(ware);
                return result;
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
        public async Task<ActionResult<AddressDTO>> UpdateWare([FromBody] AddressDTO ware)
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressDTO>> DeleteWare(long id)
        {
            try
            {
                var result = await _serv.DeleteAsync(id);
                return Ok(result);
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

	public class AddressQueryPL
	{
        public string SearchParameter{ get; set; } // Назва вулиці
        public long? Id { get; set; }
        public string? Street { get; set; } // Назва вулиці
        public string? HouseNumber { get; set; } // Номер будинку
        public string? City { get; set; } // Місто
        public string? State { get; set; } // Область або штат
        public string? PostalCode { get; set; } // Поштовий індекс
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public long? ShopId { get; set; }
        public long? StorageId { get; set; }
        public long? OrderId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
    }
}
