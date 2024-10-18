using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
	[ApiController]
	[Route("api/Storage")]
	public class StorageController : ControllerBase
	{
        private readonly IStorageService _serv;
        IWebHostEnvironment _appEnvironment;


        public StorageController(IStorageService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<StorageQueryPL, StorageQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
             .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.ShopId))
             .ForMember(dest => dest.WareItemId, opt => opt.MapFrom(src => src.WareItemId))
             .ForMember(dest => dest.StorageEmployeeId, opt => opt.MapFrom(src => src.StorageEmployeeId))
             .ForMember(dest => dest.ShopEmployeeId, opt => opt.MapFrom(src => src.ShopEmployeeId))
             .ForMember(dest => dest.IsGlobal, opt => opt.MapFrom(src => src.IsGlobal))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
        });

        [HttpPost]
		public async Task<ActionResult<StorageDTO>> Create([FromBody] StorageDTO storageDto)
        {
            try
			{
                var result = await _serv.Create(storageDto);
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
		public async Task<ActionResult<StorageDTO>> UpdateStorage([FromBody] StorageDTO storageDto)
		{
            try
            {
                var result = await _serv.Update(storageDto);
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
        public async Task<ActionResult<StorageDTO>> DeleteStorage(long storageId)
		{
            try
            {
                var result = await _serv.Delete(storageId);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageDTO>>> GetWares([FromQuery] StorageQueryPL query)
        {
            try
            {
                IEnumerable<StorageDTO> collection = null;

                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Storage.Id для пошуку!", nameof(StorageQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<StorageDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "AddressId":
                        {
                            if (query.AddressId == null)
                            {
                                throw new ValidationException("Не вказано Address.Id для пошуку!", nameof(StorageQueryPL.AddressId));
                            }
                            else
                            {
                                collection = new List<StorageDTO> { await _serv.GetByAddressId(query.AddressId.Value) };
                            }
                        }
                        break;
                    case "ShopId":
                        {
                            if (query.ShopId == null)
                            {
                                throw new ValidationException("Не вказано Shop.Id для пошуку!", nameof(StorageQueryPL.ShopId));
                            }
                            else
                            {
                                collection = new List<StorageDTO> { await _serv.GetByShopId(query.ShopId.Value) };
                            }
                        }
                        break;
                    case "WareItemId":
                        {
                            if (query.WareItemId == null)
                            {
                                throw new ValidationException("Не вказано WareItem.Id для пошуку!", nameof(StorageQueryPL.WareItemId));
                            }
                            else
                            {
                                collection = new List<StorageDTO> { await _serv.GetByWareItemId(query.WareItemId.Value) };
                            }
                        }
                        break;
                    case "StorageEmployeeId":
                        {
                            if (query.StorageEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано StorageEmployee.Id для пошуку!", nameof(StorageQueryPL.StorageEmployeeId));
                            }
                            else
                            {
                                collection = new List<StorageDTO> { await _serv.GetByStorageEmployeeId(query.StorageEmployeeId) };
                            }
                        }
                        break;
                    case "ShopEmployeeId":
                        {
                            if (query.ShopEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано ShopEmployee.Id для пошуку!", nameof(StorageQueryPL.ShopEmployeeId));
                            }
                            else
                            {
                                collection = new List<StorageDTO> { await _serv.GetByShopEmployeeId(query.ShopEmployeeId) };
                            }
                        }
                        break;
                    case "GetGlobalStorages":
                        {
                            collection = await _serv.GetGlobalStorages();
                        }
                        break;
                    case "GetNonGlobalStorages":
                        {
                            collection = await _serv.GetNonGlobalStorages();
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null || query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано PageNumber або PageSize для пошуку!", nameof(StorageQueryPL.PageNumber));
                            }
                            collection = await _serv.GetPaged(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано StringIds для пошуку!", nameof(StorageQueryPL.StringIds));
                            }
                            collection = await _serv.GetByStringIds(query.StringIds);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<StorageQueryBLL>(query);
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
    }

    public class StorageQueryPL
    {
        public string SearchParameter { get; set; }
        public long? AddressId { get; set; }
        public long? Id { get; set; }
        public long? ShopId { get; set; }
        public long? WareItemId { get; set; }
        public string? StorageEmployeeId { get; set; }
        public string? ShopEmployeeId { get; set; }
        public bool? IsGlobal { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
