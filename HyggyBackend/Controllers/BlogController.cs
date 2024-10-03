using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace HyggyBackend.Controllers
{
    [ApiController]
    [Route("api/Blog")]
    public class BlogController :ControllerBase
    {
        private readonly IBlogService _serv;
        IWebHostEnvironment _appEnvironment;

        public BlogController(IBlogService service, IWebHostEnvironment appEnvironment)
        {
            _serv = service;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<BlogQueryPL, BlogQueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.BlogTitle, opt => opt.MapFrom(src => src.BlogTitle))
             .ForMember(dest => dest.Keyword, opt => opt.MapFrom(src => src.Keyword))
             .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
             .ForMember(dest => dest.PreviewImagePath, opt => opt.MapFrom(src => src.PreviewImagePath))
             .ForMember(dest => dest.BlogCategory1Id, opt => opt.MapFrom(src => src.BlogCategory1Id))
             .ForMember(dest => dest.BlogCategory1Name, opt => opt.MapFrom(src => src.BlogCategory1Name))
             .ForMember(dest => dest.BlogCategory2Id, opt => opt.MapFrom(src => src.BlogCategory2Id))
             .ForMember(dest => dest.BlogCategory2Name, opt => opt.MapFrom(src => src.BlogCategory2Name))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
        });


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> Get([FromQuery] BlogQueryPL query)
        {
            try
            {
                IEnumerable<BlogDTO> collection = null;
                switch (query.SearchParameter)
                {
                    case "GetById":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано Blog.Id для пошуку!", "");
                            }
                            else
                            {
                                collection = new List<BlogDTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "GetByBlogTitle":
                        {
                            if (query.BlogTitle == null)
                            {
                                throw new ValidationException("Не вказано TitleSubstring для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByTitleSubstring(query.BlogTitle);
                            }
                        }
                        break;
                    case "GetByKeywords":
                        {
                            if (query.Keyword == null)
                            {
                                throw new ValidationException("Не вказано KeywordSubstring для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByKeywordSubstring(query.Keyword);
                            }
                        }
                        break;
                    case "GetByFilePath":
                        {
                            if (query.FilePath == null)
                            {
                                throw new ValidationException("Не вказано FilePathSubstring для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByFilePathSubstring(query.FilePath);
                            }
                        }
                        break;
                    case "GetByPreviewImagePath":
                        {
                            if (query.PreviewImagePath == null)
                            {
                                throw new ValidationException("Не вказано PreviewImagePathSubstring для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByPreviewImagePathSubstring(query.PreviewImagePath);
                            }
                        }
                        break;
                    case "GetByBlogCategory2Id":
                        {
                            if (query.BlogCategory2Id == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2Id для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory2Id(query.BlogCategory2Id.Value);
                            }
                        }
                        break;
                    case "GetByBlogCategory2Name":
                        {
                            if (query.BlogCategory2Name == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2NameSubstring для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory2NameSubstring(query.BlogCategory2Name);
                            }
                        }
                        break;
                    case "GetByBlogCategory1Name":
                        {
                            if (query.BlogCategory1Name == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1NameSubstring для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory1NameSubstring(query.BlogCategory1Name);
                            }
                        }
                        break;
                    case "GetPagedBlogs":
                        {
                            if(query.PageNumber == null)
                            {
                               throw new ValidationException("Не вказано PageNumber для пошуку!", "");
                            }
                            if (query.PageSize == null)
                            {
                                throw new ValidationException("Не вказано PageSize для пошуку!", "");
                            }
                            collection = await _serv.GetPagedBlogs(query.PageNumber.Value, query.PageSize.Value);
                        }
                        break;
                    case "GetByQuery":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<BlogQueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр BlogQuery.SearchParameter!", nameof(BlogQueryPL.SearchParameter));
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
        public async Task<ActionResult<BlogDTO>> Createblog([FromBody] BlogDTO blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ValidationException("Не вказано blog для створення!", nameof(BlogDTO));
                }
                var result = await _serv.AddBlog(blog);
                return result;
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
        public async Task<ActionResult<BlogDTO>> Updateblog([FromBody] BlogDTO blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ValidationException("Не вказано blog для оновлення!", nameof(BlogDTO));
                }
                var result = await _serv.UpdateBlog(blog);
                return result;
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
        public async Task<ActionResult<BlogDTO>> Deleteblog(long id)
        {
            try
            {
                var result = await _serv.DeleteBlog(id);
                return Ok(result);
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
                return StatusCode(500, ex.Message);
            }
        }
        private async Task<ActionResult<string>> SaveJsonToFile(JToken jsonContent)
        {
            // генеруємо новий GUID
            string guid = Guid.NewGuid().ToString();
            string newFileName = $"{guid}.json";
            string path = "/BlogPageJsonStructures/" + newFileName;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(jsonContent.ToString()); // jsonContent може бути і JObject, і JArray
                }
            }

            path = "https://localhost:7288" + path;
            return new ObjectResult(path);
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
                    // Отримати вміст файлу
                    //string fileContent = await System.IO.File.ReadAllTextAsync(oldFilePath);

                    // Парсимо JSON-масив
                    //JArray jsonOldFileObject = JArray.Parse(fileContent);
                    //foreach (var item in jsonOldFileObject)
                    //{
                    //    if (item["type"] != null && item["type"].ToString() == "gallery")
                    //    {
                    //        JArray valueArray = (JArray)item["value"];
                    //        foreach (var valueItem in valueArray)
                    //        {
                    //            if (valueItem["dataUrl"] != null)
                    //            {
                    //                string dataUrl = valueItem["dataUrl"].ToString();

                    //                var oldFileUri1 = new Uri(dataUrl);
                    //                var oldFilePath1 = Path.Combine(_appEnvironment.WebRootPath, oldFileUri1.AbsolutePath.TrimStart('/'));
                    //                Console.WriteLine(oldFilePath1);
                    //                if (System.IO.File.Exists(oldFilePath1))
                    //                {
                    //                    System.IO.File.Delete(oldFilePath1);
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

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
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                    // Отримати вміст файлу
                    string fileContent = await System.IO.File.ReadAllTextAsync(oldFilePath);

                    // Парсимо JSON-масив
                    JArray jsonOldFileObject = JArray.Parse(fileContent);
                    foreach (var item in jsonOldFileObject)
                    {
                        if (item["type"] != null && item["type"].ToString() == "gallery")
                        {
                            JArray valueArray = (JArray)item["value"];
                            foreach (var valueItem in valueArray)
                            {
                                if (valueItem["dataUrl"] != null)
                                {
                                    string dataUrl = valueItem["dataUrl"].ToString();

                                    var oldFileUri1 = new Uri(dataUrl);
                                    var oldFilePath1 = Path.Combine(_appEnvironment.WebRootPath, oldFileUri1.AbsolutePath.TrimStart('/'));
                                    Console.WriteLine(oldFilePath1);
                                    if (System.IO.File.Exists(oldFilePath1))
                                    {
                                        System.IO.File.Delete(oldFilePath1);
                                    }
                                }
                            }
                        }
                    }
                    // Видаляємо старий файл конструктора
                    System.IO.File.Delete(oldFilePath);
                }
                return new ObjectResult("OK");
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
        [Route("PostBlogImage")]
        public async Task<ActionResult<ICollection<string>>> PostBlogImage([FromForm] IFormFileCollection FormFiles)
        {
            try
            {
                if (FormFiles is null || FormFiles.Count == 0)
                {
                    throw new ValidationException("Файли не було завантажено!", nameof(FormFiles));
                }
                List<string> paths = new List<string>();
                foreach (var FormFile in FormFiles)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(FormFile.FileName);
                    fileName = fileName.Replace(" ", "_");

                    // генерируем новый GUID
                    string guid = Guid.NewGuid().ToString();

                    // добавляем GUID к имени файла
                    string newFileName = $"{fileName}_{guid}{Path.GetExtension(FormFile.FileName)}";

                    // Путь к папке Files
                    string path = "/BlogImages/" + newFileName; // новое имя файла

                    // Сохраняем файл в папку Files в каталоге wwwroot
                    // Для получения полного пути к каталогу wwwroot
                    // применяется свойство WebRootPath объекта IWebHostEnvironment
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await FormFile.CopyToAsync(fileStream); // копируем файл в поток
                    }
                    //return new ObjectResult(_appEnvironment.WebRootPath + path);
                    path = "https://localhost:7288" + path;
                    paths.Add(path);
                }
                return new ObjectResult(paths);
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

    public class BlogQueryPL
    {
        public string SearchParameter { get; set; } // Назва статті блогу
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogCategory1Id { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogCategory2Id { get; set; }
        public string? BlogCategory2Name { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
