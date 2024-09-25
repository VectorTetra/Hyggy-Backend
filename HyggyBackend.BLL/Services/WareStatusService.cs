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

        public WareStatusService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration WareStatus_WareStatusDTOMapConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WareStatus, WareStatusDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(c => c.Description));

            cfg.CreateMap<WareStatusDTO, WareStatus>()
                .ForMember(c => c.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(d => d.Name))
                .ForMember(c => c.Description, opt => opt.MapFrom(d => d.Description));

        });

        MapperConfiguration WareStatusQueryBLL_WareStatusQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<WareStatusQueryBLL, WareStatusQueryDAL>());

        public async Task<WareStatusDTO?> GetById(long id)
        {
            WareStatus ware = await Database.WareStatuses.GetById(id);
            if (ware == null)
            {
                return null;
            }
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<WareStatusDTO>(ware);
        }
        public async Task<WareStatusDTO?> GetByWareId(long id)
        {
            WareStatus ware = await Database.WareStatuses.GetByWareId(id);
            if (ware == null)
            {
                return null;
            }
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<WareStatusDTO>(ware);
        }
        public async Task<WareStatusDTO?> GetByWareArticle(long article)
        {
            WareStatus ware = await Database.WareStatuses.GetByWareArticle(article);
            if (ware == null)
            {
                return null;
            }
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<WareStatusDTO>(ware);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetPagedWareStatuses(int pageNumber, int pageSize)
        {
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetPagedWareStatuses(pageNumber, pageSize);
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetByNameSubstring(string nameSubstring)
        {
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetByNameSubstring(nameSubstring);
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetByDescriptionSubstring(descriptionSubstring);
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<IEnumerable<WareStatusDTO>> GetByQuery(WareStatusQueryBLL queryBLL)
        {
            IMapper mapper = WareStatusQueryBLL_WareStatusQueryDALMapConfig.CreateMapper();
            WareStatusQueryDAL queryDAL = mapper.Map<WareStatusQueryDAL>(queryBLL);
            IEnumerable<WareStatus> wareStatuses = await Database.WareStatuses.GetByQuery(queryDAL);
            return mapper.Map<IEnumerable<WareStatusDTO>>(wareStatuses);
        }
        public async Task<WareStatusDTO?> Create(WareStatusDTO wareStatusDTO)
        {
            var existedNames = await Database.Wares.GetByNameSubstring(wareStatusDTO.Name);
            if (existedNames.Any(x => x.Name == wareStatusDTO.Name))
            {
                throw new ValidationException("Статус Товару з таким іменем вже існує!", wareStatusDTO.Name);
            }

            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            WareStatus wareStatus = mapper.Map<WareStatus>(wareStatusDTO);
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

            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            WareStatus wareStatus = mapper.Map<WareStatus>(wareStatusDTO);
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
            IMapper mapper = WareStatus_WareStatusDTOMapConfig.CreateMapper();
            return mapper.Map<WareStatusDTO>(wareStatus);
        }

    }
}
