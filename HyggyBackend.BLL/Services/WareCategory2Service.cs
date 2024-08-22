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
    public class WareCategory2Service: IWareCategory2Service
    {
        IUnitOfWork Database;
        public WareCategory2Service(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WareCategory2, WareCategory2DTO>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForPath(dst => dst.WareCategory1, opt => opt.MapFrom(c => new WareCategory1DTO
            {
                Id = c.WareCategory1.Id,
                JSONStructureFilePath = c.WareCategory1.JSONStructureFilePath,
                Name = c.WareCategory1.Name
            }))
            .ForMember(dst => dst.WaresCategory3, opt => opt.MapFrom(c => c.WaresCategory3.Select(wc => new WareCategory3DTO
            {
                Id = wc.Id,
                Name = wc.Name,
                JSONStructureFilePath = wc.JSONStructureFilePath,
                WareCategory2 = new WareCategory2DTO
                {
                    Id = wc.WareCategory2.Id,
                    JSONStructureFilePath = wc.WareCategory2.JSONStructureFilePath,
                    Name = wc.WareCategory2.Name,
                    WareCategory1 = new WareCategory1DTO
                    {
                        Id = wc.WareCategory2.WareCategory1.Id,
                        JSONStructureFilePath = wc.WareCategory2.WareCategory1.JSONStructureFilePath,
                        Name = wc.WareCategory2.WareCategory1.Name
                    }
                }
            })));


            cfg.CreateMap<WareCategory2DTO, WareCategory2>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForPath(dst => dst.WareCategory1, opt => opt.MapFrom(c => new WareCategory1
            {
                Id = c.WareCategory1.Id,
                JSONStructureFilePath = c.WareCategory1.JSONStructureFilePath,
                Name = c.WareCategory1.Name
            }))
            .ForMember(dst => dst.WaresCategory3, opt => opt.MapFrom(c => c.WaresCategory3.Select(wc => new WareCategory3
            {
                Id = wc.Id,
                Name = wc.Name,
                JSONStructureFilePath = wc.JSONStructureFilePath,
                WareCategory2 = new WareCategory2
                {
                    Id = wc.WareCategory2.Id,
                    JSONStructureFilePath = wc.WareCategory2.JSONStructureFilePath,
                    Name = wc.WareCategory2.Name,
                    WareCategory1 = new WareCategory1
                    {
                        Id = wc.WareCategory2.WareCategory1.Id,
                        JSONStructureFilePath = wc.WareCategory2.WareCategory1.JSONStructureFilePath,
                        Name = wc.WareCategory2.WareCategory1.Name
                    }
                }
            })));
        });

        MapperConfiguration WareCategory2QueryBLL_WareCategory2QueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<WareCategory2QueryBLL, WareCategory2QueryDAL>());

        public async Task<WareCategory2DTO?> GetById(long id)
        {
            WareCategory2 wareCategory2 = await Database.Categories2.GetById(id);
            if (wareCategory2 == null)
            {
                return null;
            }
            IMapper mapper = config.CreateMapper();
            return mapper.Map<WareCategory2DTO>(wareCategory2);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetPagedCategories(pageNumber, pageSize);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByNameSubstring(string nameSubstring)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByNameSubstring(nameSubstring);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByJSONStructureFilePathSubstring(JSONStructureFilePathSubstring);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory1Id(long id)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByWareCategory1Id(id);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByWareCategory1NameSubstring(WareCategory1NameSubstring);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory3Id(long id)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByWareCategory3Id(id);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByWareCategory3NameSubstring(WareCategory3NameSubstring);
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByQuery(WareCategory2QueryBLL query)
        {
            IMapper mapper = config.CreateMapper();
            var wareCategory2s = await Database.Categories2.GetByQuery(mapper.Map<WareCategory2QueryBLL, WareCategory2QueryDAL>(query));
            return mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<WareCategory2DTO> Create(WareCategory2DTO category2DTO) 
        {
            var mapper = new Mapper(config);
            var wareCategory2 = mapper.Map<WareCategory2DTO, WareCategory2>(category2DTO);
            await Database.Categories2.Create(wareCategory2);
            await Database.Save();
            var returnedCategory = await Database.Categories2.GetById(wareCategory2.Id);
            return mapper.Map<WareCategory2, WareCategory2DTO>(returnedCategory);
        }
        public async Task<WareCategory2DTO> Update(WareCategory2DTO category2DTO) 
        {
            var mapper = new Mapper(config);
            var wareCategory2 = mapper.Map<WareCategory2DTO, WareCategory2>(category2DTO);
            Database.Categories2.Update(wareCategory2);
            await Database.Save();
            var returnedCategory = await Database.Categories2.GetById(wareCategory2.Id);
            return mapper.Map<WareCategory2, WareCategory2DTO>(returnedCategory);
        }
        public async Task<WareCategory2DTO> Delete(long id)
        {
            var mapper = new Mapper(config);
            var wareCategory2 = await Database.Categories2.GetById(id);
            await Database.Categories2.Delete(id);
            await Database.Save();
            return mapper.Map<WareCategory2, WareCategory2DTO>(wareCategory2);
        }
    }
}
