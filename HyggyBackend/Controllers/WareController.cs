using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.BLL.Services;
using HyggyBackend.DAL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace HyggyBackend.Controllers
{
    [Route("api/Ware")]
    [ApiController]
    public class WareController : ControllerBase
    {
        private readonly IWareService _serv;
        IWebHostEnvironment _appEnvironment;

        public WareController(IWareService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<WareQueryPL, WareQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article))
             .ForMember(dest => dest.Category1Id, opt => opt.MapFrom(src => src.Category1Id))
             .ForMember(dest => dest.Category2Id, opt => opt.MapFrom(src => src.Category2Id))
             .ForMember(dest => dest.Category3Id, opt => opt.MapFrom(src => src.Category3Id))
             .ForMember(dest => dest.NameSubstring, opt => opt.MapFrom(src => src.NameSubstring))
             .ForMember(dest => dest.DescriptionSubstring, opt => opt.MapFrom(src => src.DescriptionSubstring))
             .ForMember(dest => dest.Category1NameSubstring, opt => opt.MapFrom(src => src.Category1NameSubstring))
             .ForMember(dest => dest.Category2NameSubstring, opt => opt.MapFrom(src => src.Category2NameSubstring))
             .ForMember(dest => dest.Category3NameSubstring, opt => opt.MapFrom(src => src.Category3NameSubstring))
             .ForMember(dest => dest.TrademarkId, opt => opt.MapFrom(src => src.TrademarkId))
             .ForMember(dest => dest.TrademarkNameSubstring, opt => opt.MapFrom(src => src.TrademarkNameSubstring))
             .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.MinPrice))
             .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.MaxPrice))
             .ForMember(dest => dest.MinDiscount, opt => opt.MapFrom(src => src.MinDiscount))
             .ForMember(dest => dest.MaxDiscount, opt => opt.MapFrom(src => src.MaxDiscount))
             .ForMember(dest => dest.IsDeliveryAvailable, opt => opt.MapFrom(src => src.IsDeliveryAvailable))
             .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
             .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusName))
             .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.StatusDescription))
             .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
             .ForMember(dest => dest.Sorting, opt => opt.MapFrom(src => src.Sorting))
             .ForMember(dest => dest.StringIds, opt => opt.MapFrom(src => src.StringIds))
             .ForMember(dest => dest.StringTrademarkIds, opt => opt.MapFrom(src => src.StringTrademarkIds))
             .ForMember(dest => dest.StringStatusIds, opt => opt.MapFrom(src => src.StringStatusIds))
             .ForMember(dest => dest.StringCategory1Ids, opt => opt.MapFrom(src => src.StringCategory1Ids))
             .ForMember(dest => dest.StringCategory2Ids, opt => opt.MapFrom(src => src.StringCategory2Ids))
             .ForMember(dest => dest.StringCategory3Ids, opt => opt.MapFrom(src => src.StringCategory3Ids))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
             .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
             .ForMember(dest => dest.QueryAny, opt => opt.MapFrom(src => src.QueryAny));

        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WareDTO>>> GetWares([FromQuery] WareQueryPL query)
        {
            try
            {
                IEnumerable<WareDTO> collection = null;

                switch (query.SearchParameter)  
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Id для пошуку!", nameof(WareQueryPL.Id));
                            }
                            else
                            {
                                collection = new List<WareDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "Article":
                        {
                            if (query.Article == null)
                            {
                                throw new ValidationException("Не вказано Ware.Article для пошуку!", nameof(WareQueryPL.Article));
                            }
                            else
                            {
                                collection = new List<WareDTO> { await _serv.GetByArticle(query.Article.Value) };
                            }
                        }
                        break;
                    case "Category1Id":
                        {
                            if (query.Category1Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category1Id для пошуку!", nameof(WareQueryPL.Category1Id));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory1Id(query.Category1Id.Value);
                            }
                        }
                        break;
                    case "Category2Id":
                        {
                            if (query.Category2Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category2Id для пошуку!", nameof(WareQueryPL.Category2Id));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory2Id(query.Category2Id.Value);
                            }
                        }
                        break;
                    case "Category3Id":
                        {
                            if (query.Category3Id == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category3Id для пошуку!", nameof(WareQueryPL.Category3Id));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory3Id(query.Category3Id.Value);
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (query.NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Name для пошуку!", nameof(WareQueryPL.NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByNameSubstring(query.NameSubstring);
                            }
                        }
                        break;
                    case "Description":
                        {
                            if (query.DescriptionSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Description для пошуку!", nameof(WareQueryPL.DescriptionSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByDescriptionSubstring(query.DescriptionSubstring);
                            }
                        }
                        break;
                    case "Category1Name":
                        {
                            if (query.Category1NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category1Name для пошуку!", nameof(WareQueryPL.Category1NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory1NameSubstring(query.Category1NameSubstring);
                            }
                        }
                        break;
                    case "Category2Name":
                        {
                            if (query.Category2NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category2Name для пошуку!", nameof(WareQueryPL.Category2NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory2NameSubstring(query.Category2NameSubstring);
                            }
                        }
                        break;
                    case "Category3Name":
                        {
                            if (query.Category3NameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.Category3Name для пошуку!", nameof(WareQueryPL.Category3NameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByCategory3NameSubstring(query.Category3NameSubstring);
                            }
                        }
                        break;
                    case "TrademarkId":
                        {
                            if (query.TrademarkId == null)
                            {
                                throw new ValidationException("Не вказано Ware.TrademarkId для пошуку!", nameof(WareQueryPL.TrademarkId));
                            }
                            else
                            {
                                collection = await _serv.GetByTrademarkId(query.TrademarkId.Value);
                            }
                        }
                        break;
                    case "TrademarkName":
                        {
                            if (query.TrademarkNameSubstring == null)
                            {
                                throw new ValidationException("Не вказано Ware.TrademarkName для пошуку!", nameof(WareQueryPL.TrademarkNameSubstring));
                            }
                            else
                            {
                                collection = await _serv.GetByTrademarkNameSubstring(query.TrademarkNameSubstring);
                            }
                        }
                        break;
                    case "Price":
                        {
                            if (query.MinPrice == null )
                            {
                                throw new ValidationException("Не вказано Ware.MinPrice для пошуку!", nameof(WareQueryPL.MinPrice));
                            }
                            else if (query.MaxPrice == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxPrice для пошуку!", nameof(WareQueryPL.MaxPrice));
                            }

                            collection = await _serv.GetByPriceRange(query.MinPrice.Value, query.MaxPrice.Value);
                        }
                        break;
                    case "Discount":
                        {

                            if (query.MinDiscount == null)
                            {
                                throw new ValidationException("Не вказано Ware.MinDiscount для пошуку!", nameof(WareQueryPL.MinDiscount));
                            }
                            else if (query.MaxDiscount == null)
                            {
                                throw new ValidationException("Не вказано Ware.MaxDiscount для пошуку!", nameof(WareQueryPL.MaxDiscount));
                            }

                            collection = await _serv.GetByDiscountRange(query.MinDiscount.Value, query.MaxDiscount.Value);
                        }
                        break;
                    case "IsDeliveryAvailable":
                        {
                            if (query.IsDeliveryAvailable == null)
                            {
                                throw new ValidationException("Не вказано Ware.IsDeliveryAvailable для пошуку!", nameof(WareQueryPL.IsDeliveryAvailable));
                            }
                            else
                            {
                                collection = await _serv.GetByIsDeliveryAvailable(query.IsDeliveryAvailable.Value);
                            }
                        }
                        break;
                    case "StatusId":
                        {
                            if (query.StatusId == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusId для пошуку!", nameof(WareQueryPL.StatusId));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusId(query.StatusId.Value);
                            }
                        }
                        break;
                    case "StatusName":
                        {
                            if (query.StatusName == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusName для пошуку!", nameof(WareQueryPL.StatusName));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusNameSubstring(query.StatusName);
                            }
                        }
                        break;
                    case "StatusDescription":
                        {
                            if (query.StatusDescription == null)
                            {
                                throw new ValidationException("Не вказано Ware.StatusDescription для пошуку!", nameof(WareQueryPL.StatusDescription));
                            }
                            else
                            {
                                collection = await _serv.GetByStatusDescriptionSubstring(query.StatusDescription);
                            }
                        }
                        break;
                    case "ImagePath":
                        {
                            if (query.ImagePath == null)
                            {
                                throw new ValidationException("Не вказано Ware.ImagePath для пошуку!", nameof(WareQueryPL.ImagePath));
                            }
                            else
                            {
                                collection = await _serv.GetByImagePathSubstring(query.ImagePath);
                            }
                        }
                        break;
                    case "GetFavoritesByCustomerId":
                        {
                            if (query.CustomerId == null)
                            {
                                throw new ValidationException("Не вказано Ware.CustomerId для пошуку!", nameof(WareQueryPL.CustomerId));
                            }
                            else
                            {
                                collection = await _serv.GetFavoritesByCustomerId(query.CustomerId);
                            }
                        }
                        break;
                    case "StringIds":
                        {
                            if (query.StringIds == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringIds для пошуку!", nameof(WareQueryPL.StringIds));
                            }
                            else
                            {
                                collection = await _serv.GetByStringIds(query.StringIds);
                            }
                        }
                        break;
                    case "StringTrademarkIds":
                        {
                            if (query.StringTrademarkIds == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringTrademarkIds для пошуку!", nameof(WareQueryPL.StringTrademarkIds));
                            }
                            else
                            {
                                collection = await _serv.GetByStringTrademarkIds(query.StringTrademarkIds);
                            }
                        }
                        break;
                    case "StringStatusIds":
                        {
                            if (query.StringStatusIds == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringStatusIds для пошуку!", nameof(WareQueryPL.StringStatusIds));
                            }
                            else
                            {
                                collection = await _serv.GetByStringStatusIds(query.StringStatusIds);
                            }
                        }
                        break;
                    case "StringCategory1Ids":
                        {
                            if (query.StringCategory1Ids == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringCategory1Ids для пошуку!", nameof(WareQueryPL.StringCategory1Ids));
                            }
                            else
                            {
                                collection = await _serv.GetByStringCategory1Ids(query.StringCategory1Ids);
                            }
                        }
                        break;
                    case "StringCategory2Ids":
                        {
                            if (query.StringCategory2Ids == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringCategory2Ids для пошуку!", nameof(WareQueryPL.StringCategory2Ids));
                            }
                            else
                            {
                                collection = await _serv.GetByStringCategory2Ids(query.StringCategory2Ids);
                            }
                        }
                        break;
                    case "StringCategory3Ids":
                        {
                            if (query.StringCategory3Ids == null)
                            {
                                throw new ValidationException("Не вказано Ware.StringCategory3Ids для пошуку!", nameof(WareQueryPL.StringCategory3Ids));
                            }
                            else
                            {
                                collection = await _serv.GetByStringCategory3Ids(query.StringCategory3Ids);
                            }
                        }
                        break;
                    case "Paged":
                        {
                            if (query.PageNumber == null)
                            {
                                throw new ValidationException("Не вказано WareQuery.PageNumber для пошуку!", nameof(WareQueryPL.PageNumber));
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано WareQuery.PageSize для пошуку!", nameof(WareQueryPL.PageSize));
                            }
                            collection = await _serv.GetPagedWares(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<WareQueryBLL>(query);
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
        public async Task<ActionResult<WareDTO>> CreateWare([FromBody] WareDTO ware)
        {
            try
            {
                if (ware == null)
                {
                    throw new ValidationException("Не вказано Ware для створення!", nameof(WareDTO));
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
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<WareDTO>> UpdateWare([FromBody] WareDTO ware)
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
        public async Task<ActionResult<WareDTO>> DeleteWare(long id)
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

        [HttpPost]
        [Route("PostJsonConstructorFile")]
        public async Task<ActionResult<string>> PostJsonConstructorFile([FromForm] string JsonConstructorItems)
        {
            try
            {
                JToken jsonToken = JToken.Parse(JsonConstructorItems);
                return await SaveJsonToFile(jsonToken);
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
        private async Task<ActionResult<string>> SaveJsonToFile(JToken jsonContent)
        {
            try
            {
                // Генеруємо новий GUID
                string guid = Guid.NewGuid().ToString();
                string newFileName = $"{guid}.json";
                string folderPath = Path.Combine(_appEnvironment.WebRootPath, "WarePageJsonStructures");

                // Перевіряємо, чи існує папка, і створюємо її, якщо ні
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    using (var writer = new StreamWriter(fileStream))
                    {
                        writer.Write(jsonContent.ToString());
                    }
                }

                // Формуємо шлях до файлу для повернення
                string path = "http://www.hyggy.somee.com/WarePageJsonStructures/" + newFileName;
                return new ObjectResult(path);
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
        [Route("PutJsonConstructorFile")]
        public async Task<ActionResult<string>> PutJsonConstructorFile([FromForm] string oldConstructorFilePath, [FromForm] string JsonConstructorItems)
        {
            try
            {
                var oldFileUri = new Uri(oldConstructorFilePath);
                var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));
                Console.WriteLine(oldFilePath);

                if (System.IO.File.Exists(oldFilePath))
                {
                    // Записуємо новий контент у старий файл
                    System.IO.File.WriteAllText(oldFilePath, JsonConstructorItems);

                    // Повертаємо URL до оновленого файлу
                    string updatedFilePath = oldConstructorFilePath; // Якщо шлях до файлу не змінюється
                    return new ObjectResult(updatedFilePath);
                }
                else
                {
                    return StatusCode(404, "Старий файл не знайдено.");
                }
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message); // Змінив статус помилки на 400, якщо це валідна помилка
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteJsonConstructorFile")]
        public async Task<ActionResult<string>> DeleteJsonConstructorFile([FromForm] string ConstructorFilePath)
        {
            try
            {
                var oldFileUri = new Uri(ConstructorFilePath);
                var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));
                Console.WriteLine(oldFilePath);

                if (System.IO.File.Exists(oldFilePath))
                {
                    // Видаляємо старий файл конструктора
                    System.IO.File.Delete(oldFilePath);
                    return new ObjectResult($"Файл {ConstructorFilePath} було успішно видалено!");
                }
                else
                {
                    return StatusCode(404, "Файл не знайдено для видалення.");
                }
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message); // Змінив статус помилки на 400, якщо це валідна помилка
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }


    }

    public class WareQueryPL
    {
        public string? SearchParameter { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public long? Id { get; set; }
        public long? Article { get; set; }
        public long? Category1Id { get; set; }
        public long? Category2Id { get; set; }
        public long? Category3Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
        public string? Category1NameSubstring { get; set; }
        public string? Category2NameSubstring { get; set; }
        public string? Category3NameSubstring { get; set; }
        public long? TrademarkId { get; set; }
        public string? TrademarkNameSubstring { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinDiscount { get; set; }
        public float? MaxDiscount { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public string? CustomerId { get; set; }
        public string? ImagePath { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? StringTrademarkIds { get; set; }
        public string? StringStatusIds { get; set; }
        public string? StringCategory1Ids { get; set; }
        public string? StringCategory2Ids { get; set; }
        public string? StringCategory3Ids { get; set; }
        public string? QueryAny { get; set; }
    }

}
