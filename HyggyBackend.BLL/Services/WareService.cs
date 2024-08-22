using HyggyBackend.BLL.Interfaces;
using AutoMapper;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using HyggyBackend.BLL.Infrastructure;

namespace HyggyBackend.BLL.Services
{
    public class WareService : IWareService
    {
        IUnitOfWork Database;

        public WareService(IUnitOfWork uow)
        {
            Database = uow;
        }

        //  Цей код створює маппінг для двостороннього перетворення між Ware і WareDTO.
        //  Використовуючи об'єкт Ware_WareDTOMapConfig, ви зможете легко перетворити об'єкт WareDTO на Ware, і навпаки.
        MapperConfiguration Ware_WareDTOMapConfig = new MapperConfiguration(cfg =>
        {
        cfg.CreateMap<Ware, WareDTO>()
            .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(d => d.Article, opt => opt.MapFrom(c => c.Article))
            .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Description))
            .ForMember(d => d.Price, opt => opt.MapFrom(c => c.Price))
            .ForMember(d => d.Discount, opt => opt.MapFrom(c => c.Discount))
            .ForMember(d => d.IsDeliveryAvailable, opt => opt.MapFrom(c => c.IsDeliveryAvailable))
            .ForPath(d => d.WareCategory3, opt => opt.MapFrom(c =>
                new WareCategory3DTO
                {
                    Id = c.WareCategory3.Id,
                    JSONStructureFilePath = c.WareCategory3.JSONStructureFilePath,
                    Name = c.WareCategory3.Name,
                    WareCategory2 = new WareCategory2DTO
                    {
                        Id = c.WareCategory3.WareCategory2.Id,
                        JSONStructureFilePath = c.WareCategory3.WareCategory2.JSONStructureFilePath,
                        Name = c.WareCategory3.WareCategory2.Name,
                        WareCategory1 = new WareCategory1DTO
                        {
                            Id = c.WareCategory3.WareCategory2.WareCategory1.Id,
                            JSONStructureFilePath = c.WareCategory3.WareCategory2.WareCategory1.JSONStructureFilePath,
                            Name = c.WareCategory3.WareCategory2.WareCategory1.Name
                        }
                    }
                }))
            .ForPath(d => d.Status, opt => opt.MapFrom(c =>
                new WareStatusDTO
                {
                    Id = c.Status.Id,
                    Name = c.Status.Name,
                    Description = c.Status.Description
                }))
            .ForPath(d => d.Images, opt => opt.MapFrom(c => c.Images.Select(image => new WareImageDTO
            {
                Id = image.Id,
                Path = image.Path,
                Ware = image.Ware
            })));

        cfg.CreateMap<WareDTO, Ware>()
            .ForMember(c => c.Id, opt => opt.MapFrom(d => d.Id))
            .ForMember(c => c.Article, opt => opt.MapFrom(d => d.Article))
            .ForMember(c => c.Name, opt => opt.MapFrom(d => d.Name))
            .ForMember(c => c.Description, opt => opt.MapFrom(d => d.Description))
            .ForMember(c => c.Price, opt => opt.MapFrom(d => d.Price))
            .ForMember(c => c.Discount, opt => opt.MapFrom(d => d.Discount))
            .ForMember(c => c.IsDeliveryAvailable, opt => opt.MapFrom(d => d.IsDeliveryAvailable))
            .ForPath(c => c.WareCategory3, opt => opt.MapFrom(d =>
                new WareCategory3
                {
                    Id = d.WareCategory3.Id,
                    JSONStructureFilePath = d.WareCategory3.JSONStructureFilePath,
                    Name = d.WareCategory3.Name,
                    WareCategory2 = new WareCategory2
                    {
                        Id = d.WareCategory3.WareCategory2.Id,
                        JSONStructureFilePath = d.WareCategory3.WareCategory2.JSONStructureFilePath,
                        Name = d.WareCategory3.WareCategory2.Name,
                        WareCategory1 = new WareCategory1
                        {
                            Id = d.WareCategory3.WareCategory2.WareCategory1.Id,
                            JSONStructureFilePath = d.WareCategory3.WareCategory2.WareCategory1.JSONStructureFilePath,
                            Name = d.WareCategory3.WareCategory2.WareCategory1.Name
                        }
                    }
                }))
            .ForPath(c => c.Status, opt => opt.MapFrom(d =>
                new WareStatus
                {
                    Id = d.Status.Id,
                    Name = d.Status.Name,
                    Description = d.Status.Description
                }))
            .ForPath(c => c.Images, opt => opt.MapFrom(d => d.Images.Select(imageDTO => new WareImage
            {
                Id = imageDTO.Id,
                Path = imageDTO.Path,
                Ware = imageDTO.Ware
            })))
            .ForPath(c => c.PriceHistories, opt => opt.MapFrom(d => d.PriceHistories.Select(ph => new WarePriceHistoryDTO
            {
                Id = ph.Id, 
                Price = ph.Price,
                EffectiveDate = ph.EffectiveDate,
                Ware = ph.Ware
            })));

    });

        MapperConfiguration WareQueryBLL_WareQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<WareQueryBLL, WareQueryDAL>());



    public async Task<WareDTO?> GetById(long id)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var ware = await Database.Wares.GetById(id);
        if (ware == null)
        {
            return null;
        }
        return mapper.Map<Ware, WareDTO>(ware);
    }
    public async Task<WareDTO?> GetByArticle(long article)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var ware = await Database.Wares.GetByArticle(article);
        if (ware == null)
        {
            return null;
        }
        return mapper.Map<Ware, WareDTO>(ware);
    }
    public async Task<IEnumerable<WareDTO>> GetPagedWares(int pageNumber, int pageSize)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetPagedWares(pageNumber, pageSize);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByCategory1Id(long category1Id)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByCategory1Id(category1Id);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByCategory2Id(long category2Id)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByCategory2Id(category2Id);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByCategory3Id(long category3Id)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByCategory3Id(category3Id);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByNameSubstring(string nameSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByNameSubstring(nameSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByDescriptionSubstring(string descriptionSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByDescriptionSubstring(descriptionSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByCategory1NameSubstring(string category1NameSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByCategory1NameSubstring(category1NameSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByCategory2NameSubstring(string category2NameSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByCategory2NameSubstring(category2NameSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByCategory3NameSubstring(string category3NameSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByCategory3NameSubstring(category3NameSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByPriceRange(float minPrice, float maxPrice)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByPriceRange(minPrice, maxPrice);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByDiscountRange(float minDiscount, float maxDiscount)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByDiscountRange(minDiscount, maxDiscount);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByIsDeliveryAvailable(bool isDeliveryAvailable)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByIsDeliveryAvailable(isDeliveryAvailable);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByStatusId(long statusId)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByStatusId(statusId);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByStatusNameSubstring(string statusNameSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByStatusNameSubstring(statusNameSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByStatusDescriptionSubstring(statusDescriptionSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByImagePathSubstring(string imagePathSubstring)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wares = await Database.Wares.GetByImagePathSubstring(imagePathSubstring);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<IEnumerable<WareDTO>> GetByQuery(WareQueryBLL queryDAL)
    {
        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var queryMapper = new Mapper(WareQueryBLL_WareQueryDALMapConfig);
        var query = queryMapper.Map<WareQueryBLL, WareQueryDAL>(queryDAL);
        var wares = await Database.Wares.GetByQuery(query);
        return mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
    }
    public async Task<WareDTO> Create(WareDTO wareDTO)
    {
        //Перевірка на унікальність артикулу та назви
        var existedArticle = await Database.Wares.GetByArticle(wareDTO.Article);
        if (existedArticle != null)
        {
            throw new ValidationException("Товар з таким артикулом вже існує!", wareDTO.Article.ToString());
        }

        var existedNames = await Database.Wares.GetByNameSubstring(wareDTO.Name);
        if (existedNames.Any(x => x.Name == wareDTO.Name))
        {
            throw new ValidationException("Товар з таким іменем вже існує!", wareDTO.Name);
        }

        if (wareDTO.Price < 0)
        {
            throw new ValidationException("Ціна не може бути від'ємною!", wareDTO.Price.ToString());
        }

        if (wareDTO.Discount < 0)
        {
            throw new ValidationException("Знижка не може бути від'ємною!", wareDTO.Discount.ToString());
        }

        if (wareDTO.WareCategory3 == null)
        {
            throw new ValidationException("Категорія не може бути пустою!", wareDTO.WareCategory3.ToString());
        }

        if (wareDTO.Status == null)
        {
            throw new ValidationException("Статус не може бути пустим!", wareDTO.Status.ToString());
        }

        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wareDAL = mapper.Map<WareDTO, Ware>(wareDTO);

        await Database.Wares.Create(wareDAL);
        await Database.Save();

        wareDTO.Id = wareDAL.Id;
        return wareDTO;

    }
    public async Task<WareDTO> Update(WareDTO wareDTO)
    {

        var existedId = await Database.Wares.GetById(wareDTO.Id);
        if (existedId == null)
        {
            throw new ValidationException("Товару з таким Id не існує!", wareDTO.Id.ToString());
        }

        var existedArticle = await Database.Wares.GetByArticle(wareDTO.Article);
        if (existedArticle != null && existedArticle.Id != wareDTO.Id)
        {
            throw new ValidationException("Товар з таким артикулом вже існує!", wareDTO.Article.ToString());
        }

        if (wareDTO.Price < 0)
        {
            throw new ValidationException("Ціна не може бути від'ємною!", wareDTO.Price.ToString());
        }

        if (wareDTO.Discount < 0)
        {
            throw new ValidationException("Знижка не може бути від'ємною!", wareDTO.Discount.ToString());
        }

        if (wareDTO.WareCategory3 == null)
        {
            throw new ValidationException("Категорія не може бути пустою!", wareDTO.WareCategory3.ToString());
        }

        if (wareDTO.Status == null)
        {
            throw new ValidationException("Статус не може бути пустим!", wareDTO.Status.ToString());
        }

        var mapper = new Mapper(Ware_WareDTOMapConfig);
        var wareDAL = mapper.Map<WareDTO, Ware>(wareDTO);

        Database.Wares.Update(wareDAL);
        await Database.Save();

        var returnedDTO = await GetById(wareDTO.Id);
        return returnedDTO;
    }
    public async Task<WareDTO> Delete(long id)
    {
        var existedId = await Database.Wares.GetById(id);
        if (existedId == null)
        {
            throw new ValidationException("Товару з таким Id не існує!", id.ToString());
        }

        var mapper = new Mapper(Ware_WareDTOMapConfig);

        await Database.Wares.Delete(id);
        await Database.Save();

        return mapper.Map<Ware, WareDTO>(existedId);
    }
}
}
