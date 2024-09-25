using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using static System.Net.Mime.MediaTypeNames;

namespace HyggyBackend.BLL.Services
{
    public class WareImageService : IWareImageService
    {
        IUnitOfWork Database;

        public WareImageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration WareImage_WareImageDTOMapConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WareImage, WareImageDTO>()
            .ForMember(dest => dest.Id, Path => Path.MapFrom(src => src.Id))
            .ForMember(dest => dest.Path, Path => Path.MapFrom(src => src.Path))
            .ForMember(dest => dest.Ware, Path => Path.MapFrom(image => new WareDTO
            {
                Id = image.Ware.Id,
                Name = image.Ware.Name,
                Description = image.Ware.Description,
                Price = image.Ware.Price,
                Discount = image.Ware.Discount,
                IsDeliveryAvailable = image.Ware.IsDeliveryAvailable,
                WareCategory3 = new WareCategory3DTO
                {
                    Id = image.Ware.WareCategory3.Id,
                    Name = image.Ware.WareCategory3.Name,
                    WareCategory2 = new WareCategory2DTO
                    {
                        Id = image.Ware.WareCategory3.WareCategory2.Id,
                        Name = image.Ware.WareCategory3.WareCategory2.Name,
                        WareCategory1 = new WareCategory1DTO
                        {
                            Id = image.Ware.WareCategory3.WareCategory2.WareCategory1.Id,
                            Name = image.Ware.WareCategory3.WareCategory2.WareCategory1.Name
                        }
                    }
                },
                Status = new WareStatusDTO
                {
                    Id = image.Ware.Status.Id,
                    Name = image.Ware.Status.Name,
                    Description = image.Ware.Status.Description
                },
                Images = image.Ware.Images.Select(i => new WareImageDTO
                {
                    Id = i.Id,
                    Path = i.Path
                }).ToList()
            }));


            cfg.CreateMap<WareImageDTO, WareImage>()
            .ForMember(dest => dest.Id, Path => Path.MapFrom(src => src.Id))
            .ForMember(dest => dest.Path, Path => Path.MapFrom(src => src.Path))
            .ForMember(dest => dest.Ware, Path => Path.MapFrom(image => new Ware
            {
                Id = image.Ware.Id,
                Name = image.Ware.Name,
                Description = image.Ware.Description,
                Price = image.Ware.Price,
                Discount = image.Ware.Discount,
                IsDeliveryAvailable = image.Ware.IsDeliveryAvailable,
                WareCategory3 = new WareCategory3
                {
                    Id = image.Ware.WareCategory3.Id,
                    Name = image.Ware.WareCategory3.Name,
                    WareCategory2 = new WareCategory2
                    {
                        Id = image.Ware.WareCategory3.WareCategory2.Id,
                        Name = image.Ware.WareCategory3.WareCategory2.Name,
                        WareCategory1 = new WareCategory1
                        {
                            Id = image.Ware.WareCategory3.WareCategory2.WareCategory1.Id,
                            Name = image.Ware.WareCategory3.WareCategory2.WareCategory1.Name
                        }
                    }
                },
                Status = new WareStatus
                {
                    Id = image.Ware.Status.Id,
                    Name = image.Ware.Status.Name,
                    Description = image.Ware.Status.Description
                },
                Images = image.Ware.Images.Select(i => new WareImage
                {
                    Id = i.Id,
                    Path = i.Path
                }).ToList()
            }));

        });

        MapperConfiguration WareImageQueryBLL_WareImageQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<WareImageQueryBLL, WareImageQueryDAL>());

        public async Task<WareImageDTO?> GetById(long id)
        {
            var mapper = new Mapper(WareImage_WareImageDTOMapConfig);
            return mapper.Map<WareImageDTO>(await Database.WareImages.GetById(id));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByWareId(long wareId)
        {
            var mapper = new Mapper(WareImage_WareImageDTOMapConfig);
            return mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByWareId(wareId));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByWareArticle(long wareArticle)
        {
            var mapper = new Mapper(WareImage_WareImageDTOMapConfig);
            return mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByWareArticle(wareArticle));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByPathSubstring(string path)
        {
            var mapper = new Mapper(WareImage_WareImageDTOMapConfig);
            return mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByPathSubstring(path));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByQuery(WareImageQueryDAL queryDAL)
        {
            var mapper = new Mapper(WareImageQueryBLL_WareImageQueryDALMapConfig);
            return mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByQuery(queryDAL));
        }
        public async Task<WareImageDTO> Create(WareImageDTO wareImage)
        {
            var existedWareId = await Database.Wares.GetById(wareImage.Ware.Id);
            if (existedWareId == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareImage.Ware.Id.ToString());
            }

            var mapper = new Mapper(WareImage_WareImageDTOMapConfig);
            var wareImageDAL = mapper.Map<WareImage>(wareImage);
            await Database.WareImages.Create(wareImageDAL);
            await Database.Save();
            var returnedDTO = await GetById(wareImageDAL.Id);
            if (returnedDTO == null)
            {
                throw new ValidationException("Помилка при створенні зображення товару!", wareImageDAL.Id.ToString());
            }
            return returnedDTO;

        }
        public async Task<WareImageDTO> Update(WareImageDTO wareImage)
        {
            var existedWareId = await Database.Wares.GetById(wareImage.Ware.Id);
            if (existedWareId == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareImage.Ware.Id.ToString());
            }

            var existedWareImageId = await Database.WareImages.GetById(wareImage.Id);
            if (existedWareImageId == null)
            {
                throw new ValidationException("Зображення товару з таким Id не існує!", wareImage.Id.ToString());
            }

            var mapper = new Mapper(WareImage_WareImageDTOMapConfig);
            var wareImageDAL = mapper.Map<WareImage>(wareImage);
            Database.WareImages.Update(wareImageDAL);
            await Database.Save();
            var returnedDTO = await GetById(wareImageDAL.Id);
            if (returnedDTO == null)
            {
                throw new ValidationException("Помилка при оновленні зображення товару!", wareImageDAL.Id.ToString());
            }
            return returnedDTO;
        }
        public async Task<WareImageDTO> Delete(long id)
        {
            var existedWareImageId = await Database.WareImages.GetById(id);
            if (existedWareImageId == null)
            {
                throw new ValidationException("Зображення товару з таким Id не існує!", id.ToString());
            }
            var returnedDTO = await GetById(id);
            await Database.WareImages.Delete(id);
            await Database.Save();
            return returnedDTO;
        }
    }
}
