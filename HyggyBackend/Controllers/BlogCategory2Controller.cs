﻿using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HyggyBackend.Controllers
{
    [ApiController]
    [Route("api/BlogCategory2")]
    public class BlogCategory2Controller : ControllerBase
    {
        private readonly IBlogCategory2Service _serv;
        IWebHostEnvironment _appEnvironment;

        public BlogCategory2Controller(IBlogCategory2Service service, IWebHostEnvironment appEnvironment)
        {
            _serv = service;
            _appEnvironment = appEnvironment;
        }

        MapperConfiguration config = new MapperConfiguration(mc =>
        {
            mc.CreateMap<BlogCategory2QueryPL, BlogCategory2QueryBLL>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.BlogTitle, opt => opt.MapFrom(src => src.BlogTitle))
             .ForMember(dest => dest.Keyword, opt => opt.MapFrom(src => src.Keyword))
             .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
             .ForMember(dest => dest.PreviewImagePath, opt => opt.MapFrom(src => src.PreviewImagePath))
             .ForMember(dest => dest.BlogCategory1Id, opt => opt.MapFrom(src => src.BlogCategory1Id))
             .ForMember(dest => dest.BlogCategory1Name, opt => opt.MapFrom(src => src.BlogCategory1Name))
             .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.BlogId))
             .ForMember(dest => dest.BlogCategory2Name, opt => opt.MapFrom(src => src.BlogCategory2Name))
             .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
             .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
        });

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogCategory2DTO>>> Get([FromQuery] BlogCategory2QueryPL query)
        {
            try
            {
                IEnumerable<BlogCategory2DTO> collection = null;
                switch (query.SearchParameter)
                {
                    case "GetById":
                        {
                            if (query.Id == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.Id для пошуку!", "");
                            }
                            else
                            {
                                collection = new List<BlogCategory2DTO> { await _serv.GetById(query.Id.Value) };
                            }
                        }
                        break;
                    case "GetByBlogTitle":
                        {
                            if (query.BlogTitle == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.BlogTitle для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogTitle(query.BlogTitle);
                            }
                        }
                        break;
                    case "GetByKeyword":
                        {
                            if (query.Keyword == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.Keyword для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogKeyword(query.Keyword);
                            }
                        }
                        break;
                    case "GetByFilePath":
                        {
                            if (query.FilePath == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.FilePath для пошуку!", "");
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
                                throw new ValidationException("Не вказано BlogCategory2.PreviewImagePath для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByPreviewImagePathSubstring(query.PreviewImagePath);
                            }
                        }
                        break;
                    case "GetByBlogId":
                        {
                            if (query.BlogId == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.BlogId для пошуку!", "");
                            }
                            else
                            {
                                collection = new List<BlogCategory2DTO> { await _serv.GetByBlogId(query.BlogId.Value) };
                            }
                        }
                        break;
                    case "GetByBlogCategory2Name":
                        {
                            if (query.BlogCategory2Name == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.BlogCategory2Name для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory2NameSubstring(query.BlogCategory2Name);
                            }
                        }
                        break;
                    case "GetByBlogCategory1Id":
                        {
                            if (query.BlogCategory1Id == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.BlogCategory1Id для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory1Id(query.BlogCategory1Id.Value);
                            }
                        }
                        break;
                    case "GetByBlogCategory1Name":
                        {
                            if (query.BlogCategory1Name == null)
                            {
                                throw new ValidationException("Не вказано BlogCategory2.BlogCategory1Name для пошуку!", "");
                            }
                            else
                            {
                                collection = await _serv.GetByBlogCategory1NameSubstring(query.BlogCategory1Name);
                            }
                        }
                        break;
                    case "GetByQuery":
                        {
                            var mapper = new Mapper(config);
                            var queryBLL = mapper.Map<BlogCategory2QueryBLL>(query);
                            collection = await _serv.GetByQuery(queryBLL);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр BlogCategory2Query.SearchParameter!", nameof(BlogQueryPL.SearchParameter));
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
        public async Task<ActionResult<BlogCategory2DTO>> Createblog([FromBody] BlogCategory2DTO blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ValidationException("Не вказано BlogCategory2 для створення!", nameof(BlogCategory2DTO));
                }
                var result = await _serv.AddBlogCategory2(blog);
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
        public async Task<ActionResult<BlogCategory2DTO>> Updateblog([FromBody] BlogCategory2DTO blog)
        {
            try
            {
                if (blog == null)
                {
                    throw new ValidationException("Не вказано BlogCategory2 для оновлення!", nameof(BlogCategory2DTO));
                }
                var result = await _serv.UpdateBlogCategory2(blog);
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
        public async Task<ActionResult<BlogCategory2DTO>> Deleteblog(long id)
        {
            try
            {
                var result = await _serv.DeleteBlogCategory2(id);
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

    public class BlogCategory2QueryPL
    {
        public string SearchParameter { get; set; } // Назва статті блогу
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogCategory1Id { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogId { get; set; }
        public string? BlogCategory2Name { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
