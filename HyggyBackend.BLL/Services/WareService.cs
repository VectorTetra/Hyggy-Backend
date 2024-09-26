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
        IMapper _mapper;

        public WareService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }
        public async Task<WareDTO?> GetById(long id)
        {
            var ware = await Database.Wares.GetById(id);
            if (ware == null)
            {
                return null;
            }
            return _mapper.Map<Ware, WareDTO>(ware);
        }
        public async Task<WareDTO?> GetByArticle(long article)
        {
            var ware = await Database.Wares.GetByArticle(article);
            if (ware == null)
            {
                return null;
            }
            return _mapper.Map<Ware, WareDTO>(ware);
        }
        public async Task<IEnumerable<WareDTO>> GetPagedWares(int pageNumber, int pageSize)
        {
            var wares = await Database.Wares.GetPagedWares(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory1Id(long category1Id)
        {
            var wares = await Database.Wares.GetByCategory1Id(category1Id);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory2Id(long category2Id)
        {
            var wares = await Database.Wares.GetByCategory2Id(category2Id);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory3Id(long category3Id)
        {
            var wares = await Database.Wares.GetByCategory3Id(category3Id);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByNameSubstring(string nameSubstring)
        {
            var wares = await Database.Wares.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var wares = await Database.Wares.GetByDescriptionSubstring(descriptionSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory1NameSubstring(string category1NameSubstring)
        {
            var wares = await Database.Wares.GetByCategory1NameSubstring(category1NameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory2NameSubstring(string category2NameSubstring)
        {
            var wares = await Database.Wares.GetByCategory2NameSubstring(category2NameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByCategory3NameSubstring(string category3NameSubstring)
        {
            var wares = await Database.Wares.GetByCategory3NameSubstring(category3NameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByPriceRange(float minPrice, float maxPrice)
        { 
            var wares = await Database.Wares.GetByPriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByDiscountRange(float minDiscount, float maxDiscount)
        {
            var wares = await Database.Wares.GetByDiscountRange(minDiscount, maxDiscount);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByIsDeliveryAvailable(bool isDeliveryAvailable)
        {
            var wares = await Database.Wares.GetByIsDeliveryAvailable(isDeliveryAvailable);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByStatusId(long statusId)
        {
            var wares = await Database.Wares.GetByStatusId(statusId);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByStatusNameSubstring(string statusNameSubstring)
        {
            var wares = await Database.Wares.GetByStatusNameSubstring(statusNameSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByStatusDescriptionSubstring(string statusDescriptionSubstring)
        {
            var wares = await Database.Wares.GetByStatusDescriptionSubstring(statusDescriptionSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByImagePathSubstring(string imagePathSubstring)
        {
            var wares = await Database.Wares.GetByImagePathSubstring(imagePathSubstring);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<IEnumerable<WareDTO>> GetByQuery(WareQueryBLL queryDAL)
        {
            var query = _mapper.Map<WareQueryBLL, WareQueryDAL>(queryDAL);
            var wares = await Database.Wares.GetByQuery(query);
            return _mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares);
        }
        public async Task<WareDTO> Create(WareDTO wareDTO)
        {
            //Перевірка на унікальність артикулу та назви
            var existedArticle = await Database.Wares.GetByArticle(wareDTO.Article.Value);
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

            if (wareDTO.WareCategory3Id == null)
            {
                throw new ValidationException("Категорія не може бути пустою!", wareDTO.WareCategory3Id.ToString());
            }

            if (wareDTO.StatusId == null)
            {
                throw new ValidationException("Статус не може бути пустим!", wareDTO.StatusId.ToString());
            }


            var wareDAL = _mapper.Map<WareDTO, Ware>(wareDTO);

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

            var existedArticle = await Database.Wares.GetByArticle(wareDTO.Article.Value);
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

            if (wareDTO.WareCategory3Id == null)
            {
                throw new ValidationException("Категорія не може бути пустою!", wareDTO.WareCategory3Id.ToString());
            }

            if (wareDTO.StatusId == null)
            {
                throw new ValidationException("Статус не може бути пустим!", wareDTO.StatusId.ToString());
            }


            var wareDAL = _mapper.Map<WareDTO, Ware>(wareDTO);

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



            await Database.Wares.Delete(id);
            await Database.Save();

            return _mapper.Map<Ware, WareDTO>(existedId);
        }
    }
}
