using AutoMapper;
using Castle.Core.Resource;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class BlogCategory1Service : IBlogCategory1Service
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public BlogCategory1Service(IUnitOfWork database, IMapper mapper)
        {
            Database = database;
            _mapper = mapper;
        }
        public async Task<BlogCategory1DTO> GetById(long id)
        {
            var blogCategory1 = await Database.BlogCategories1.GetById(id);
            return _mapper.Map<BlogCategory1DTO>(blogCategory1);
        }
        public async Task<BlogCategory1DTO> GetByBlogId(long blogId)
        {
            var blogCategory1 = await Database.BlogCategories1.GetByBlogId(blogId);
            return _mapper.Map<BlogCategory1DTO>(blogCategory1);
        }
        public async Task<BlogCategory1DTO> GetByBlogCategory2Id(long blogCategory2Id)
        {
            var blogCategory1 = await Database.BlogCategories1.GetByBlogCategory2Id(blogCategory2Id);
            return _mapper.Map<BlogCategory1DTO>(blogCategory1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByStringIds(string StringIds)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByStringIds(StringIds);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByFilePathSubstring(string FilePathSubstring)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByFilePathSubstring(FilePathSubstring);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByPreviewImagePathSubstring(PreviewImagePathSubstring);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByBlogCategory2NameSubstring(string BlogCategory2NameSubstring)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByBlogCategory2NameSubstring(BlogCategory2NameSubstring);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByBlogCategory1NameSubstring(BlogCategory1NameSubstring);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByBlogTitleSubstring(string BlogTitleSubstring)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByBlogTitleSubstring(BlogTitleSubstring);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByBlogKeywordSubstring(string BlogKeywordSubstring)
        {
            var blogCategories1 = await Database.BlogCategories1.GetByBlogKeywordSubstring(BlogKeywordSubstring);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetPagedBlogCategories1(int PageNumber, int PageSize)
        {
            var blogCategories1 = await Database.BlogCategories1.GetPagedBlogCategories1(PageNumber, PageSize);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<IEnumerable<BlogCategory1DTO>> GetByQuery(BlogCategory1QueryBLL queryBLL)
        {
            var queryDAL = _mapper.Map<BlogCategory1QueryDAL>(queryBLL);
            var blogCategories1 = await Database.BlogCategories1.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<BlogCategory1DTO>>(blogCategories1);
        }
        public async Task<BlogCategory1DTO> AddBlogCategory1(BlogCategory1DTO BlogCategory1DTO)
        {
            if (string.IsNullOrEmpty(BlogCategory1DTO.Name))
            {
                throw new ValidationException($"Не вказано BlogCategory1DTO.Name!", "");
            }

            var blogCategory1 = new BlogCategory1
            {
                Name = BlogCategory1DTO.Name,
                BlogCategories2 = new List<BlogCategory2>()
            };

            await Database.BlogCategories1.AddBlogCategory1(blogCategory1);
            await Database.Save();
            return _mapper.Map<BlogCategory1DTO>(blogCategory1);
        }
        public async Task<BlogCategory1DTO> UpdateBlogCategory1(BlogCategory1DTO blogCategory1DTO)
        {
            var exBlCat1 = await Database.BlogCategories1.GetById(blogCategory1DTO.Id);
            if (exBlCat1 == null)
            {
                throw new ValidationException($"BlogCategory1 з id={blogCategory1DTO.Id} не знайдено!", "");
            }
            if (blogCategory1DTO.BlogCategory2Ids != null)
            {

                if (!blogCategory1DTO.BlogCategory2Ids.Any())
                {
                    exBlCat1.BlogCategories2.Clear();  // Очищаємо колекцію, якщо масив порожній
                }
                else
                {
                    exBlCat1.BlogCategories2.Clear();
                    await foreach (var blCat2 in Database.BlogCategories2.GetByIdsAsync(blogCategory1DTO.BlogCategory2Ids))
                    {
                        if (blCat2 == null)
                        {
                            throw new ValidationException($"Одна з категорій товарів 2 не знайдена!", "");
                        }
                        exBlCat1.BlogCategories2.Add(blCat2);  // Додаємо нові замовлення
                    }
                }

            }

            exBlCat1.Name = blogCategory1DTO.Name;

            Database.BlogCategories1.UpdateBlogCategory1(exBlCat1);
            await Database.Save();
            return _mapper.Map<BlogCategory1DTO>(exBlCat1);
        }
        public async Task<BlogCategory1DTO> DeleteBlogCategory1(long id)
        {
            var blogCategory1 = await Database.BlogCategories1.GetById(id);
            if (blogCategory1 == null)
            {
                throw new ValidationException($"BlogCategory1 з id={id} не знайдено!", "");
            }
            await Database.BlogCategories1.DeleteBlogCategory1(id);
            await Database.Save();
            return _mapper.Map<BlogCategory1DTO>(blogCategory1);
        }
    }
}
