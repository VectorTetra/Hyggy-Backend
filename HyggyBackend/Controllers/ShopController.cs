using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/Shop")]
	public class ShopController : Controller
	{
		private readonly IShopService _serv;
        IWebHostEnvironment _appEnvironment;
        public ShopController(IShopService shopService, IWebHostEnvironment webHostEnvironment)
		{
			_serv = shopService;
			_appEnvironment = webHostEnvironment;
        }
        MapperConfiguration config = new MapperConfiguration(mc =>
        {
			mc.CreateMap<ShopQueryPL, ShopQueryBLL>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.StorageId, opt => opt.MapFrom(src => src.StorageId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            .ForMember(dest => dest.NearestCount, opt => opt.MapFrom(src => src.NearestCount))
            .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
            .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting));
        });
        [HttpGet]
		public async Task<ActionResult<IEnumerable<ShopDTO>>> Get([FromQuery] ShopQueryPL query)
		{
            try
            {
                IEnumerable<ShopDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Shop.Id для пошуку!", "");
                            }
                            else
                            {
                                var res = await _serv.GetById(query.Id.Value);
                                if (res != null)
                                {
                                    collection = new List<ShopDTO> { res };
                                }
                                else
                                { return NoContent(); }
                            }
                        }
                        break;
                    case "AddressId":
                        {
                            if (query.AddressId == null)
                            {
                                throw new ValidationException("Не вказано Shop.AddressId для пошуку!", nameof(ShopQueryPL.AddressId));
                            }
                            else
                            {
                                var res = await _serv.GetByAddressId(query.AddressId.Value);
                                if (res != null)
                                {
                                    collection = new List<ShopDTO> { res };
                                }
                                else
                                { return NoContent(); }
                            }
                        }
                        break;
                    case "StorageId":
                        {
                            if (query.StorageId == null)
                            {
                                throw new ValidationException("Не вказано Shop.StorageId для пошуку!", nameof(ShopQueryPL.StorageId));
                            }
                            else
                            {
                                var res = await _serv.GetByStorageId(query.StorageId.Value);
                                if (res != null)
                                {
                                    collection = new List<ShopDTO> { res };
                                }
                                else
                                { return NoContent(); }
                            }
                        }
                        break;
                    case "OrderId":
                        {
                            if (query.OrderId == null)
                            {
                                throw new ValidationException("Не вказано Shop.OrderId для пошуку!", nameof(ShopQueryPL.OrderId));
                            }
                            else
                            {
                                var res = await _serv.GetByOrderId(query.OrderId.Value);
                                if (res != null)
                                {
                                    collection = new List<ShopDTO> { res };
                                }
                                else
                                { return NoContent(); }
                            }
                        }
                        break;
                    case "City":
                        {
                            if (query.City == null)
                            {
                                throw new ValidationException("Не вказано Shop.City для пошуку!", nameof(ShopQueryPL.City));
                            }
                            collection = await _serv.GetByCity(query.City);
                        }
                        break;
                    case "Name":
                        {
                            if (query.Name == null)
                            {
                                throw new ValidationException("Не вказано Shop.Name для пошуку!", nameof(ShopQueryPL.Name));
                            }
                            collection = await _serv.GetByName(query.Name);
                        }
                        break;
                    case "PostalCode":
                        {
                            if (query.PostalCode == null)
                            {
                                throw new ValidationException("Не вказано Shop.PostalCode для пошуку!", nameof(ShopQueryPL.PostalCode));
                            }
                            collection = await _serv.GetByPostalCode(query.PostalCode);
                        }
                        break;
                    case "State":
                        {
                            if (query.State == null)
                            {
                                throw new ValidationException("Не вказано Shop.State для пошуку!", nameof(ShopQueryPL.State));
                            }
                            collection = await _serv.GetByState(query.State);
                        }
                        break;
                    case "Street":
                        {
                            if (query.Street == null)
                            {
                                throw new ValidationException("Не вказано Shop.Street для пошуку!", nameof(ShopQueryPL.Street));
                            }
                            collection = await _serv.GetByStreet(query.Street);
                        }
                        break;
                    case "LatitudeAndLongitude":
                        {
                            if(query.Latitude == null)
                            {
                                throw new ValidationException("Не вказано Shop.Latitude для пошуку!", nameof(ShopQueryPL.Latitude));
                            }
                            if (query.Longitude == null)
                            {
                                throw new ValidationException("Не вказано Shop.Longitude для пошуку!", nameof(ShopQueryPL.Longitude));
                            }
                            var res = await _serv.GetByLatitudeAndLongitude(query.Latitude.Value, query.Longitude.Value);
                            if (res != null)
                            {
                                collection = new List<ShopDTO> { res };
                            }
                            else
                            { return NoContent(); }
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано ShopQuery.PageNumber для пошуку!", nameof(ShopQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано ShopQuery.PageSize для пошуку!", nameof(ShopQueryPL.PageSize));
                            }
                            collection = await _serv.GetPaginatedShops(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<ShopQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    case "NearestShops":
                        {
                            if (query.Latitude == null)
                            {
                                throw new ValidationException("Не вказано Shop.Latitude для пошуку!", nameof(ShopQueryPL.Latitude));
                            }
                            if (query.Longitude == null)
                            {
                                throw new ValidationException("Не вказано Shop.Longitude для пошуку!", nameof(ShopQueryPL.Longitude));
                            }
                            if (query.NearestCount == null)
                            {
                                collection = await _serv.GetNearestShopsAsync(query.Latitude.Value, query.Longitude.Value);
                            }
                            else
                            {
                                var checkNearestCount = query.NearestCount.Value > 0 ? query.NearestCount.Value : throw new ValidationException("Не вказано Shop.NearestCount не може бути менше 0!", nameof(ShopQueryPL.NearestCount)); ;
                                collection = await _serv.GetNearestShopsAsync(query.Latitude.Value, query.Longitude.Value, checkNearestCount);
                            }
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано Shop.StringIds для пошуку!", nameof(ShopQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр ShopQuery.SearchParameter!", nameof(ShopQueryPL.SearchParameter));
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
        public async Task<ActionResult<ShopDTO>> CreateShop([FromBody] ShopDTO Shop)
        {
            try
            {
                if (Shop == null)
                {
                    throw new ValidationException("Не вказано Shop для створення!", nameof(ShopDTO));
                }
                var result = await _serv.Create(Shop);
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
        public async Task<ActionResult<ShopDTO>> UpdateShop([FromBody] ShopDTO Shop)
        {
            try
            {
                if (Shop == null)
                {
                    throw new ValidationException("Не вказано Shop для оновлення!", nameof(ShopDTO));
                }
                var result = await _serv.Update(Shop);
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
        public async Task<ActionResult<ShopDTO>> DeleteShop(long id)
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
    }

	public class ShopQueryPL
	{
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        //Адреса розташування магазину
        public long? AddressId;
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        public string? Name { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public long? StorageId { get; set; }
        public long? OrderId { get; set; }
		public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? NearestCount { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }

    }
}
