using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using HyggyBackend.DAL.Entities;

namespace HyggyBackend.BLL.Services
{
    public class BlogCategory2Service : IBlogCategory2Service
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public BlogCategory2Service(IUnitOfWork database, IMapper mapper)
        {
            Database = database;
            _mapper = mapper;
        }

        public async Task<BlogCategory2DTO> GetById(long id)
        {
            var blogCategory2 = await Database.BlogCategories2.GetById(id);
            return _mapper.Map<BlogCategory2DTO>(blogCategory2);
        }

        public async Task<BlogCategory2DTO> GetByBlogId(long blogId)
        {
            var blogCategory2 = await Database.BlogCategories2.GetByBlogId(blogId);
            return _mapper.Map<BlogCategory2DTO>(blogCategory2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByStringIds(string stringIds)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByBlogCategory1Id(long blogCategory1Id)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByBlogCategory1Id(blogCategory1Id);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByBlogTitle(string title)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByBlogTitle(title);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByBlogKeyword(string keyword)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByBlogKeyword(keyword);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByFilePathSubstring(string filePath)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByFilePathSubstring(filePath);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByPreviewImagePathSubstring(PreviewImagePathSubstring);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByBlogCategory1NameSubstring(BlogCategory1NameSubstring);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring)
        {
            var blogCategories2 = await Database.BlogCategories2.GetByBlogCategory2NameSubstring(BlogCategory2NameSubstring);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetPagedBlogCategories2(int PageNumber, int PageSize)
        {
            var blogCategories2 = await Database.BlogCategories2.GetPagedBlogCategories2(PageNumber, PageSize);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<IEnumerable<BlogCategory2DTO>> GetByQuery(BlogCategory2QueryBLL queryBLL)
        {
            var queryDAL = _mapper.Map<BlogCategory2QueryDAL>(queryBLL);
            var blogCategories2 = await Database.BlogCategories2.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<BlogCategory2DTO>>(blogCategories2);
        }

        public async Task<BlogCategory2DTO> AddBlogCategory2(BlogCategory2DTO blogCategory2)
        {
            if (!blogCategory2.BlogCategory1Id.HasValue)
            {
                throw new ValidationException($"Не вказано BlogCategory1Id", "");
            }
            var exBlCat1 = await Database.BlogCategories1.GetById(blogCategory2.BlogCategory1Id.Value);
            if (exBlCat1 == null)
            {
                throw new ValidationException($"BlogCategory1Id={blogCategory2.BlogCategory1Id.Value} не знайдено!", "");
            }
            if (string.IsNullOrEmpty(blogCategory2.Name))
            {
                throw new ValidationException($"Не вказано BlogCategory2.Name!", "");
            }
            var blogCat2 = new BlogCategory2
            {
                Name = blogCategory2.Name,
                BlogCategory1 = exBlCat1,
                PreviewImagePath = blogCategory2.PreviewImagePath ?? "",
                Blogs = new List<Blog>()
            };

            await Database.BlogCategories2.AddBlogCategory2(blogCat2);
            await Database.Save();
            return _mapper.Map<BlogCategory2DTO>(blogCat2);
        }

        public async Task<BlogCategory2DTO> UpdateBlogCategory2(BlogCategory2DTO blogCategory2)
        {
            var exBlCat2 = await Database.BlogCategories2.GetById(blogCategory2.Id);
            if (exBlCat2 == null)
            {
                throw new ValidationException($"BlogCategory2Id={blogCategory2.Id} не знайдено!", "");
            }
            if (!blogCategory2.BlogCategory1Id.HasValue)
            {
                throw new ValidationException($"Не вказано BlogCategory1Id", "");
            }
            var exBlCat1 = await Database.BlogCategories1.GetById(blogCategory2.BlogCategory1Id.Value);
            if (exBlCat1 == null)
            {
                throw new ValidationException($"BlogCategory1Id={blogCategory2.BlogCategory1Id.Value} не знайдено!", "");
            }
            if (string.IsNullOrEmpty(blogCategory2.Name))
            {
                throw new ValidationException($"Не вказано BlogCategory2.Name!", "");
            }

            if (blogCategory2.BlogIds != null)
            {

                if (!blogCategory2.BlogIds.Any())
                {
                    exBlCat2.Blogs.Clear();  // Очищаємо колекцію, якщо масив порожній
                }
                else
                {
                    exBlCat2.Blogs.Clear();
                    await foreach (var blCat2 in Database.Blogs.GetByIdsAsync(blogCategory2.BlogIds))
                    {
                        if (blCat2 == null)
                        {
                            throw new ValidationException($"Один з блогів не знайдено!", "");
                        }
                        exBlCat2.Blogs.Add(blCat2);  // Додаємо нові замовлення
                    }
                }

            }

            exBlCat2.Name = blogCategory2.Name;
            exBlCat2.BlogCategory1 = exBlCat1;
            exBlCat2.PreviewImagePath = blogCategory2.PreviewImagePath ?? "";

            Database.BlogCategories2.UpdateBlogCategory2(exBlCat2);
            await Database.Save();

            var returnedBlogCat = await Database.BlogCategories2.GetById(exBlCat2.Id);
            return _mapper.Map<BlogCategory2DTO>(returnedBlogCat);
        }

        public async Task<BlogCategory2DTO> DeleteBlogCategory2(long id)
        {
            var blogCategory2 = await Database.BlogCategories2.GetById(id);
            if (blogCategory2 == null)
            {
                throw new ValidationException($"BlogCategory2 з id={id} не знайдено!", "");
            }
            await Database.BlogCategories2.DeleteBlogCategory2(id);
            await Database.Save();
            return _mapper.Map<BlogCategory2DTO>(blogCategory2);
        }
    }
}
