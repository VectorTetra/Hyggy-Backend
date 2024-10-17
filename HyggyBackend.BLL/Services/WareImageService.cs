using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class WareImageService : IWareImageService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public WareImageService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<WareImageDTO?> GetById(long id)
        {
            return _mapper.Map<WareImageDTO>(await Database.WareImages.GetById(id));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByWareId(long wareId)
        {

            return _mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByWareId(wareId));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByWareArticle(long wareArticle)
        {

            return _mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByWareArticle(wareArticle));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByPathSubstring(string path)
        {

            return _mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByPathSubstring(path));
        }
        public async Task<IEnumerable<WareImageDTO>> GetByQuery(WareImageQueryDAL queryDAL)
        {
            return _mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByQuery(queryDAL));
        }
        public async Task<WareImageDTO> Create(WareImageDTO wareImage)
        {
            var existedWareId = await Database.Wares.GetById(wareImage.WareId);
            if (existedWareId == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareImage.WareId.ToString());
            }


            var wareImageDAL = _mapper.Map<WareImage>(wareImage);
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
            var existedWareId = await Database.Wares.GetById(wareImage.WareId);
            if (existedWareId == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareImage.WareId.ToString());
            }

            var existedWareImageId = await Database.WareImages.GetById(wareImage.Id);
            if (existedWareImageId == null)
            {
                throw new ValidationException("Зображення товару з таким Id не існує!", wareImage.Id.ToString());
            }


            var wareImageDAL = _mapper.Map<WareImage>(wareImage);
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
