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
        IMapper _mapper;
        public WareCategory3Service(IUnitOfWork uow,IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<WareCategory3DTO?> GetById(long id){
            
            var wareCategory3 = await Database.Categories3.GetById(id);
            return _mapper.Map<WareCategory3, WareCategory3DTO>(wareCategory3);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {
            
            var wareCategory3s = await Database.Categories3.GetPagedCategories(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByNameSubstring(string nameSubstring){

            
            var wareCategory3s = await Database.Categories3.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            
            var wareCategory3s = await Database.Categories3.GetByJSONStructureFilePathSubstring(JSONStructureFilePathSubstring);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory1Id(long id)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareCategory1Id(id);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareCategory1NameSubstring(WareCategory1NameSubstring);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory2Id(long id)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareCategory2Id(id);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareCategory2NameSubstring(string WareCategory3NameSubstring)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareCategory2NameSubstring(WareCategory3NameSubstring);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareId(long id)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareId(id);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareArticle(long Article)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareArticle(Article);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareNameSubstring(string WareNameSubstring)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareNameSubstring(WareNameSubstring);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByWareDescriptionSubstring(string WareDescriptionSubstring)
        {
            
            var wareCategory3s = await Database.Categories3.GetByWareDescriptionSubstring(WareDescriptionSubstring);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByQuery(WareCategory3QueryBLL query)
        {
            
            var queryDAL = _mapper.Map<WareCategory3QueryBLL, WareCategory3QueryDAL>(query);
            var wareCategory3s = await Database.Categories3.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<WareCategory3DTO> Create(WareCategory3DTO category3DTO)
        {
            
            var wareCategory3 = _mapper.Map<WareCategory3DTO, WareCategory3>(category3DTO);
            await Database.Categories3.Create(wareCategory3);
            await Database.Save();
            var returnedCategory = await Database.Categories3.GetById(wareCategory3.Id);
            return _mapper.Map<WareCategory3, WareCategory3DTO>(returnedCategory);
        }
        public async Task<WareCategory3DTO> Update(WareCategory3DTO category3DTO)
        {
            
            var wareCategory3 = _mapper.Map<WareCategory3DTO, WareCategory3>(category3DTO);
            Database.Categories3.Update(wareCategory3);
            await Database.Save();
            var returnedCategory = await Database.Categories3.GetById(wareCategory3.Id);
            return _mapper.Map<WareCategory3, WareCategory3DTO>(returnedCategory);
        }
        public async Task<WareCategory3DTO> Delete(long id)
        {
            
            var wareCategory3 = await Database.Categories3.GetById(id);
            await Database.Categories3.Delete(id);
            await Database.Save();
            return _mapper.Map<WareCategory3, WareCategory3DTO>(wareCategory3);
        }
    }
}
