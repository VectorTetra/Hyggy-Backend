using AutoMapper;
using HyggyBackend.BLL.DTO;
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
    public class WareCategory1Service : IWareCategory1Service
    {
        IUnitOfWork Database;
        public WareCategory1Service(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WareCategory1, WareCategory1DTO>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForMember("WaresCategory2", opt => opt.MapFrom(c => c.WaresCategory2.Select(wc => new WareCategory2DTO
            {
                Id = wc.Id,
                Name = wc.Name,
                JSONStructureFilePath = wc.JSONStructureFilePath,
                WareCategory1 = new WareCategory1DTO
                {
                    Id = wc.WareCategory1.Id,
                    JSONStructureFilePath = wc.WareCategory1.JSONStructureFilePath,
                    Name = wc.WareCategory1.Name
                }
            })));

            cfg.CreateMap<WareCategory1DTO, WareCategory1>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForMember("WaresCategory2", opt => opt.MapFrom(c => c.WaresCategory2.Select(wc => new WareCategory2
            {
                Id = wc.Id,
                Name = wc.Name,
                JSONStructureFilePath = wc.JSONStructureFilePath,
                WareCategory1 = new WareCategory1
                {
                    Id = wc.WareCategory1.Id,
                    JSONStructureFilePath = wc.WareCategory1.JSONStructureFilePath,
                    Name = wc.WareCategory1.Name
                }
            })));
        });

        MapperConfiguration WareCategory1QueryBLL_WareCategory1QueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<WareCategory1QueryBLL, WareCategory1QueryDAL>());

        public async Task<WareCategory1DTO?> GetById(long id)
        {
            WareCategory1 wareCategory1 = await Database.Categories1.GetById(id);
            if (wareCategory1 == null)
            {
                return null;
            }
            IMapper mapper = config.CreateMapper();
            return mapper.Map<WareCategory1DTO>(wareCategory1);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetPagedCategories(pageNumber, pageSize);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByNameSubstring(string nameSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByNameSubstring(nameSubstring);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByJSONStructureFilePathSubstring(JSONStructureFilePathSubstring);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory2Id(long id)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory2Id(id);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory2NameSubstring(string WareCategory2NameSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory2NameSubstring(WareCategory2NameSubstring);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory3Id(long id)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory3Id(id);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory3NameSubstring(WareCategory3NameSubstring);
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByQuery(WareCategory1QueryBLL query)
        {
            IMapper mapper = WareCategory1QueryBLL_WareCategory1QueryDALMapConfig.CreateMapper();
            WareCategory1QueryDAL queryDAL = mapper.Map<WareCategory1QueryDAL>(query);
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByQuery(queryDAL);
            IMapper mapper2 = config.CreateMapper();
            return mapper2.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<WareCategory1DTO> Create(WareCategory1DTO category1DTO)
        {
            var mapper = new Mapper(config);
            var wareCategory1 = mapper.Map<WareCategory1DTO, WareCategory1>(category1DTO);
            await Database.Categories1.Create(wareCategory1);
            await Database.Save();
            var returnedCategory = await Database.Categories1.GetById(wareCategory1.Id);
            return mapper.Map<WareCategory1, WareCategory1DTO>(returnedCategory);
        }
        public async Task<WareCategory1DTO> Update(WareCategory1DTO category1DTO) 
        {
            var mapper = new Mapper(config);
            var wareCategory1 = mapper.Map<WareCategory1DTO, WareCategory1>(category1DTO);
            Database.Categories1.Update(wareCategory1);
            await Database.Save();
            var returnedCategory = await Database.Categories1.GetById(wareCategory1.Id);
            return mapper.Map<WareCategory1, WareCategory1DTO>(returnedCategory);
        }
        public async Task<WareCategory1DTO> Delete(long id)
        {
            var mapper = new Mapper(config);
            var wareCategory1 = await Database.Categories1.GetById(id);
            await Database.Categories1.Delete(id);
            await Database.Save();
            return mapper.Map<WareCategory1, WareCategory1DTO>(wareCategory1);
        }

    }
}
