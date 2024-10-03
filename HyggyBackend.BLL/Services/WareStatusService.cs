using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Services
{
    public class WareStatusService : IWareStatusService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public WareStatusService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<WareStatusDTO?> GetById(long id)
        {
            WareStatus ware = await Database.WareStatuses.GetById(id);
            if (ware == null)
            {
                return null;
            }
            
            return _mapper.Map<WareStatusDTO>(ware);
        }
        public async Task<WareStatusDTO?> GetByWareId(long id)
        {
            WareStatus ware = await Database.WareStatuses.GetByWareId(id);
            if (ware == null)
            {
                return null;
            }
            
            return _mapper.Map<WareStatusDTO>(ware);
        }
        public async Task<WareStatusDTO?> GetByWareArticle(long article)
        {
            WareStatus ware = await Database.WareStatuses.GetByWareArticle(article);
            if (ware == null)
            {
                return null;
            }
            
            return _mapper.Map<WareStatusDTO>(ware);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetPagedWareStatuses(int pageNumber, int pageSize)
        {
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetPagedWareStatuses(pageNumber, pageSize);
            
            return _mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetByNameSubstring(string nameSubstring)
        {
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetByNameSubstring(nameSubstring);
            
            return _mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetByDescriptionSubstring(descriptionSubstring);
            
            return _mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetByQuery(WareStatusQueryBLL queryBLL)
        {
            WareStatusQueryDAL queryDAL = _mapper.Map<WareStatusQueryDAL>(queryBLL);
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<WareStatusDTO?> Create(WareStatusDTO wareStatusDTO)
        {
            var existedNames = await Database.Wares.GetByNameSubstring(wareStatusDTO.Name);
            if (existedNames.Any(x => x.Name == wareStatusDTO.Name))
            {
                throw new ValidationException("Статус Товару з таким іменем вже існує!", wareStatusDTO.Name);
            }

            
            WareStatus wareStatus = _mapper.Map<WareStatus>(wareStatusDTO);
            await Database.WareStatuses.Create(wareStatus);
            await Database.Save();

            var returnedDTO = await GetById(wareStatus.Id);
            return returnedDTO;

        }
        public async Task<WareStatusDTO?> Update(WareStatusDTO wareStatusDTO) 
        {
            var existedNames = await Database.Wares.GetByNameSubstring(wareStatusDTO.Name);
            if (existedNames.Any(x => (x.Name == wareStatusDTO.Name && x.Id != wareStatusDTO.Id)))
            {
                throw new ValidationException("Статус Товару з таким іменем вже існує!", wareStatusDTO.Name);
            }

            
            WareStatus wareStatus = _mapper.Map<WareStatus>(wareStatusDTO);
            Database.WareStatuses.Update(wareStatus);
            await Database.Save();

            var returnedDTO = await GetById(wareStatus.Id);
            return returnedDTO;
        }
        public async Task<WareStatusDTO?> Delete(long id)
        {
            WareStatus wareStatus = await Database.WareStatuses.GetById(id);
            if (wareStatus == null)
            {
                throw new ValidationException("Статус Товару з таким id не знадено!", id.ToString());
            }
            await Database.WareStatuses.Delete(id);
            await Database.Save();
            
            return _mapper.Map<WareStatusDTO>(wareStatus);
        }

    }
}
