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
    [ApiController]
    [Route("api/BlogCategory1")]
    public class BlogCategory1Controller : ControllerBase
    {
        private readonly IBlogCategory1Service _serv;
        IWebHostEnvironment _appEnvironment;

        public BlogCategory1Controller(IBlogCategory1Service service, IWebHostEnvironment appEnvironment)
        {
            _serv = service;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<BlogCategory1QueryPL, BlogCategory1QueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.BlogTitle, opt => opt.MapFrom(src => src.BlogTitle))
             .ForMember(dest => dest.Keyword, opt => opt.MapFrom(src => src.Keyword))
             .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
             .ForMember(dest => dest.PreviewImagePath, opt => opt.MapFrom(src => src.PreviewImagePath))
             .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.BlogId))
             .ForMember(dest => dest.BlogCategory1Name, opt => opt.MapFrom(src => src.BlogCategory1Name))
             .ForMember(dest => dest.BlogCategory2Id, opt => opt.MapFrom(src => src.BlogCategory2Id))
             .ForMember(dest => dest.BlogCategory2Name, opt => opt.MapFrom(src => src.BlogCategory2Name))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogCategory1DTO>>> Get([FromQuery] BlogCategory1QueryPL query)
        {
            try
            {
                IEnumerable<BlogCategory1DTO> collection = null;
                switch (query.SearchParameter)
                {
                    case "Id":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.Id для пошуку!", "");
                            }
                            else
                            {
                                collection = new List<BlogCategory1DTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "BlogTitle":
                        {
                            if (query.BlogTitle == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.BlogTitle для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogTitleSubstring(query.BlogTitle);
                            }
                        }
                        break;
                    case "Keyword":
                        {
                            if (query.Keyword == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.Keyword для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogKeywordSubstring(query.Keyword);
                            }
                        }
                        break;
                    case "FilePath":
                        {
                            if (query.FilePath == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.FilePath для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByFilePathSubstring(query.FilePath);
                            }
                        }
                        break;
                    case "PreviewImagePath":
                        {
                            if (query.PreviewImagePath == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.PreviewImagePath для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByPreviewImagePathSubstring(query.PreviewImagePath);
                            }
                        }
                        break;
                    case "BlogId":
                        {
                            if (query.BlogId == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.BlogId для пошуку!", "");
                            }
                            else
                            {
                                collection = new List<BlogCategory1DTO> { await _serv.GetByBlogId(query.BlogId.Value) };
                            }
                        }
                        break;
                    case "Name":
                        {
                            if (query.BlogCategory1Name == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.BlogCategory1Name для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory1NameSubstring(query.BlogCategory1Name);
                            }
                        }
                        break;
                    case "BlogCategory2Id":
                        {
                            if (query.BlogCategory2Id == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.BlogCategory2Id для пошуку!", "");
                            }
                            else
                            {
                                collection = new List<BlogCategory1DTO> { await _serv.GetByBlogCategory2Id(query.BlogCategory2Id.Value) };
                            }
                        }
                        break;
                    case "BlogCategory2Name":
                        {
                            if (query.BlogCategory2Name == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory1.BlogCategory2Name для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory2NameSubstring(query.BlogCategory2Name);
                            }
                        }
                        break;
                    case "Paged":
                        {
                            var pNumber = query.PageNumber ?? throw new ValidationException("Не вказано BlogCategory1.PageNumber для пошуку!", "");
                            var pSize = query.PageSize ?? throw new ValidationException("Не вказано BlogCategory1.PageSize для пошуку!", "");
                            collection = await _serv.GetPagedBlogCategories1(pNumber, pSize);

                        }
                        break;
                    case "Query":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<BlogCategory1QueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр BlogCategory1Query.SearchParameter!", nameof(BlogQueryPL.SearchParameter));
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
        public async Task<ActionResult<BlogCategory1DTO>> Createblog([FromBody] BlogCategory1DTO blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ValidationException("Не вказано BlogCategory1 для створення!", nameof(BlogCategory1DTO));
                }
                var result = await _serv.AddBlogCategory1(blog);
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
        public async Task<ActionResult<BlogCategory1DTO>> Updateblog([FromBody] BlogCategory1DTO blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ValidationException("Не вказано BlogCategory1 для оновлення!", nameof(BlogCategory1DTO));
                }
                var result = await _serv.UpdateBlogCategory1(blog);
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
        public async Task<ActionResult<BlogCategory1DTO>> Deleteblog(long id)
        {
            try
            {
                var result = await _serv.DeleteBlogCategory1(id);
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
    }

    public class BlogCategory1QueryPL
    {
        public string SearchParameter { get; set; } // Назва статті блогу
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogId { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogCategory2Id { get; set; }
        public string? BlogCategory2Name { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
