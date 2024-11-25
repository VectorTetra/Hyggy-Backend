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
        public async Task<IEnumerable<WareImageDTO>> GetByStringIds(string stringIds)
        {
            return _mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByStringIds(stringIds));
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
        public async Task<IEnumerable<WareImageDTO>> GetByQuery(WareImageQueryBLL query)
        {
            return _mapper.Map<IEnumerable<WareImageDTO>>(await Database.WareImages.GetByQuery(_mapper.Map<WareImageQueryDAL>(query)));
        }
        public async Task<WareImageDTO> Create(WareImageDTO wareImage)
        {
            var existedWare = await Database.Wares.GetById(wareImage.WareId);
            if (existedWare == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareImage.WareId.ToString());
            }

            var wareImageDAL = new WareImage
            {
                Path = wareImage.Path,
                Ware = existedWare
            };

            await Database.WareImages.Create(wareImageDAL);
            await Database.Save();
            return _mapper.Map<WareImageDTO>(wareImageDAL);

        }
        public async Task<WareImageDTO> Update(WareImageDTO wareImage)
        {
            var existedWare = await Database.Wares.GetById(wareImage.WareId);
            if (existedWare == null)
            {
                throw new ValidationException("Товару з таким Id не існує!", wareImage.WareId.ToString());
            }

            var existedWareImage = await Database.WareImages.GetById(wareImage.Id);
            if (existedWareImage == null)
            {
                throw new ValidationException("Зображення товару з таким Id не існує!", wareImage.Id.ToString());
            }

            existedWareImage.Path = wareImage.Path;
            existedWareImage.Ware = existedWare;

            //var wareImageDAL = _mapper.Map<WareImage>(wareImage);
            Database.WareImages.Update(existedWareImage);
            await Database.Save();           
            return _mapper.Map<WareImageDTO>(existedWareImage);
        }
        public async Task<WareImageDTO> Delete(long id)
        {
            var existedWareImage = await Database.WareImages.GetById(id);
            if (existedWareImage == null)
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
