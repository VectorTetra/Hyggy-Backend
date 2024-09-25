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
    public class WareCategory3Service : IWareCategory3Service
    {
        IUnitOfWork Database;
        public WareCategory3Service(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WareCategory3 ,WareCategory3DTO >()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForPath(dst=>dst.WareCategory2, opt =>opt.MapFrom(c => new WareCategory2DTO
            {
                Id = c.WareCategory2.Id,
                JSONStructureFilePath = c.WareCategory2.JSONStructureFilePath,
                Name = c.WareCategory2.Name,
                WareCategory1 = new WareCategory1DTO
                {
                    Id = c.WareCategory2.WareCategory1.Id,
                    JSONStructureFilePath = c.WareCategory2.WareCategory1.JSONStructureFilePath,
                    Name = c.WareCategory2.WareCategory1.Name
                }
            }));

            cfg.CreateMap<WareCategory3DTO, WareCategory3>()
            .ForMember("Id", opt => opt.MapFrom(c => c.Id))
            .ForMember("Name", opt => opt.MapFrom(c => c.Name))
            .ForMember("JSONStructureFilePath", opt => opt.MapFrom(c => c.JSONStructureFilePath))
            .ForPath(dst => dst.WareCategory2, opt => opt.MapFrom(c => new WareCategory2
            {
                Id = c.WareCategory2.Id,
                JSONStructureFilePath = c.WareCategory2.JSONStructureFilePath,
                Name = c.WareCategory2.Name,
                WareCategory1 = new WareCategory1
                {
                    Id = c.WareCategory2.WareCategory1.Id,
                    JSONStructureFilePath = c.WareCategory2.WareCategory1.JSONStructureFilePath,
                    Name = c.WareCategory2.WareCategory1.Name
                }
            }));
        });

        MapperConfiguration WareCategory3QueryBLL_WareCategory3QueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<WareCategory3QueryBLL, WareCategory3QueryDAL>());

        public async Task<WareCategory3DTO?> GetById(long id){
            var mapper = new Mapper(config);
            var wareCategory3 = await Database.Categories3.GetById(id);
            return mapper.Map<WareCategory3, WareCategory3DTO>(wareCategory3);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetPagedCategories(pageNumber, pageSize);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByNameSubstring(string nameSubstring){

            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByNameSubstring(nameSubstring);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByJSONStructureFilePathSubstring(JSONStructureFilePathSubstring);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory1Id(long id)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareCategory1Id(id);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareCategory1NameSubstring(WareCategory1NameSubstring);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory2Id(long id)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareCategory2Id(id);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory2NameSubstring(string WareCategory3NameSubstring)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareCategory2NameSubstring(WareCategory3NameSubstring);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareId(long id)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareId(id);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareArticle(long Article)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareArticle(Article);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareNameSubstring(string WareNameSubstring)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareNameSubstring(WareNameSubstring);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareDescriptionSubstring(string WareDescriptionSubstring)
        {
            var mapper = new Mapper(config);
            var wareCategory3s = await Database.Categories3.GetByWareDescriptionSubstring(WareDescriptionSubstring);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByQuery(WareCategory3QueryBLL query)
        {
            var mapper = new Mapper(WareCategory3QueryBLL_WareCategory3QueryDALMapConfig);
            var queryDAL = mapper.Map<WareCategory3QueryBLL, WareCategory3QueryDAL>(query);
            var wareCategory3s = await Database.Categories3.GetByQuery(queryDAL);
            return mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<WareCategory3DTO> Create(WareCategory3DTO category3DTO)
        {
            var mapper = new Mapper(config);
            var wareCategory3 = mapper.Map<WareCategory3DTO, WareCategory3>(category3DTO);
            await Database.Categories3.Create(wareCategory3);
            await Database.Save();
            var returnedCategory = await Database.Categories3.GetById(wareCategory3.Id);
            return mapper.Map<WareCategory3, WareCategory3DTO>(returnedCategory);
        }
        public async Task<WareCategory3DTO> Update(WareCategory3DTO category3DTO)
        {
            var mapper = new Mapper(config);
            var wareCategory3 = mapper.Map<WareCategory3DTO, WareCategory3>(category3DTO);
            Database.Categories3.Update(wareCategory3);
            await Database.Save();
            var returnedCategory = await Database.Categories3.GetById(wareCategory3.Id);
            return mapper.Map<WareCategory3, WareCategory3DTO>(returnedCategory);
        }
        public async Task<WareCategory3DTO> Delete(long id)
        {
            var mapper = new Mapper(config);
            var wareCategory3 = await Database.Categories3.GetById(id);
            await Database.Categories3.Delete(id);
            await Database.Save();
            return mapper.Map<WareCategory3, WareCategory3DTO>(wareCategory3);
        }
    }
}
