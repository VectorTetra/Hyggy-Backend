using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class BlogService : IBlogService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public BlogService(IUnitOfWork database, IMapper mapper)
        {
            Database = database;
            _mapper = mapper;
        }

        public async Task<BlogDTO> GetById(long id)
        {
            var blog = await Database.Blogs.GetById(id);
            return _mapper.Map<BlogDTO>(blog);
        }
        public async Task<IEnumerable<BlogDTO>> GetByKeywordSubstring(string keyword)
        {
            var blogs = await Database.Blogs.GetByKeywordSubstring(keyword);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByStringIds(string stringIds)
        {
            var blogs = await Database.Blogs.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByTitleSubstring(string title)
        {
            var blogs = await Database.Blogs.GetByTitleSubstring(title);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByFilePathSubstring(string FilePathSubstring)
        {
            var blogs = await Database.Blogs.GetByFilePathSubstring(FilePathSubstring);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByPreviewImagePathSubstring(string PreviewImagePathSubstring)
        {
            var blogs = await Database.Blogs.GetByPreviewImagePathSubstring(PreviewImagePathSubstring);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByBlogCategory1Id(long blogCategory1Id)
        {
            var blogs = await Database.Blogs.GetByBlogCategory1Id(blogCategory1Id);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByBlogCategory2NameSubstring(string blogCategory2NameSubstring)
        {
            var blogs = await Database.Blogs.GetByBlogCategory2NameSubstring(blogCategory2NameSubstring);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByBlogCategory2Id(long blogCategory2Id)
        {
            var blogs = await Database.Blogs.GetByBlogCategory2Id(blogCategory2Id);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByBlogCategory1NameSubstring(string BlogCategory1NameSubstring)
        {
            var blogs = await Database.Blogs.GetByBlogCategory1NameSubstring(BlogCategory1NameSubstring);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetPagedBlogs(int PageNumber, int PageSize)
        {
            var blogs = await Database.Blogs.GetPagedBlogs(PageNumber, PageSize);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<IEnumerable<BlogDTO>> GetByQuery(BlogQueryBLL queryBLL)
        {
            var queryDAL = _mapper.Map<BlogQueryDAL>(queryBLL);
            var blogs = await Database.Blogs.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
        public async Task<BlogDTO> AddBlog(BlogDTO BlogDTO)
        {
            if (!BlogDTO.BlogCategory2Id.HasValue)
            {
                throw new ValidationException($"Не вказано BlogDTO.BlogCategory2Id!", "");
            }
            var exCat2 = await Database.BlogCategories2.GetById(BlogDTO.BlogCategory2Id.Value);
            if (exCat2 == null)
            {
                throw new ValidationException($"BlogCategory2 з id={BlogDTO.BlogCategory2Id.Value} не знайдено!", "");
            }
            if (string.IsNullOrEmpty(BlogDTO.BlogTitle))
            {
                throw new ValidationException($"Не вказано BlogDTO.BlogTitle!", "");
            }
            //if (string.IsNullOrEmpty(BlogDTO.Keywords))
            //{
            //    throw new ValidationException($"Не вказано BlogDTO.Keywords!", "");
            //}
            if (string.IsNullOrEmpty(BlogDTO.FilePath))
            {
                throw new ValidationException($"Не вказано BlogDTO.FilePath!", "");
            }
            //if (string.IsNullOrEmpty(BlogDTO.PreviewImagePath))
            //{
            //    throw new ValidationException($"Не вказано BlogDTO.PreviewImagePath!", "");
            //}
            var blogDAL = new Blog
            {
                BlogCategory2 = exCat2,
                BlogTitle = BlogDTO.BlogTitle,
                Keywords = BlogDTO.Keywords,
                FilePath = BlogDTO.FilePath,
                PreviewImagePath = BlogDTO.PreviewImagePath
            };

            await Database.Blogs.AddBlog(blogDAL);
            await Database.Save();
            var returnedBlog = await Database.Blogs.GetById(blogDAL.Id);
            return _mapper.Map<BlogDTO>(returnedBlog);
        }
        public async Task<BlogDTO> UpdateBlog(BlogDTO BlogDTO)
        {
            var blogDAL = await Database.Blogs.GetById(BlogDTO.Id);
            if (blogDAL == null)
            {
                throw new ValidationException($"Blog з id={BlogDTO.Id} не знайдено!", "");
            }
            if (!BlogDTO.BlogCategory2Id.HasValue)
            {
                throw new ValidationException($"Не вказано BlogDTO.BlogCategory2Id!", "");
            }
            var exCat2 = await Database.BlogCategories2.GetById(BlogDTO.BlogCategory2Id.Value);
            if (exCat2 == null)
            {
                throw new ValidationException($"BlogCategory2 з id={BlogDTO.BlogCategory2Id.Value} не знайдено!", "");
            }
            if (string.IsNullOrEmpty(BlogDTO.BlogTitle))
            {
                throw new ValidationException($"Не вказано BlogDTO.BlogTitle!", "");
            }
            //if (string.IsNullOrEmpty(BlogDTO.Keywords))
            //{
            //    throw new ValidationException($"Не вказано BlogDTO.Keywords!", "");
            //}
            if (string.IsNullOrEmpty(BlogDTO.FilePath))
            {
                throw new ValidationException($"Не вказано BlogDTO.FilePath!", "");
            }
            //if (string.IsNullOrEmpty(BlogDTO.PreviewImagePath))
            //{
            //    throw new ValidationException($"Не вказано BlogDTO.PreviewImagePath!", "");
            //}


            blogDAL.BlogCategory2 = exCat2;
            blogDAL.BlogTitle = BlogDTO.BlogTitle;
            blogDAL.Keywords = BlogDTO.Keywords;
            blogDAL.FilePath = BlogDTO.FilePath;
            blogDAL.PreviewImagePath = BlogDTO.PreviewImagePath;

            Database.Blogs.UpdateBlog(blogDAL);
            await Database.Save();
            var returnedBlog = await Database.Blogs.GetById(blogDAL.Id);
            return _mapper.Map<BlogDTO>(returnedBlog);
        }
        public async Task<BlogDTO> DeleteBlog(long id)
        {
            var blog = await Database.Blogs.GetById(id);
            if (blog == null)
            {
                throw new ValidationException($"Blog з id={id} не знайдено!", "");
            }
            await Database.Blogs.DeleteBlog(id);
            await Database.Save();
            return _mapper.Map<BlogDTO>(blog);
        }
    }
}
