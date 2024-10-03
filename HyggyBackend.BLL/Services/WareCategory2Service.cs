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
        IMapper _mapper;
        public WareCategory2Service(IUnitOfWork uow)
        {
            Database = uow;
        }


        public async Task<WareCategory2DTO?> GetById(long id)
        {
            WareCategory2 wareCategory2 = await Database.Categories2.GetById(id);
            if (wareCategory2 == null)
            {
                return null;
            }
            
            return _mapper.Map<WareCategory2DTO>(wareCategory2);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {
            
            var wareCategory2s = await Database.Categories2.GetPagedCategories(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByNameSubstring(string nameSubstring)
        {
            
            var wareCategory2s = await Database.Categories2.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            
            var wareCategory2s = await Database.Categories2.GetByJSONStructureFilePathSubstring(JSONStructureFilePathSubstring);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory1Id(long id)
        {
            
            var wareCategory2s = await Database.Categories2.GetByWareCategory1Id(id);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring)
        {
            
            var wareCategory2s = await Database.Categories2.GetByWareCategory1NameSubstring(WareCategory1NameSubstring);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory3Id(long id)
        {
            
            var wareCategory2s = await Database.Categories2.GetByWareCategory3Id(id);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring)
        {
            
            var wareCategory2s = await Database.Categories2.GetByWareCategory3NameSubstring(WareCategory3NameSubstring);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByQuery(WareCategory2QueryBLL query)
        {
            
            var wareCategory2s = await Database.Categories2.GetByQuery(_mapper.Map<WareCategory2QueryBLL, WareCategory2QueryDAL>(query));
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<WareCategory2DTO> Create(WareCategory2DTO category2DTO) 
        {
            var wareCategory2 = _mapper.Map<WareCategory2DTO, WareCategory2>(category2DTO);
            await Database.Categories2.Create(wareCategory2);
            await Database.Save();
            var returnedCategory = await Database.Categories2.GetById(wareCategory2.Id);
            return _mapper.Map<WareCategory2, WareCategory2DTO>(returnedCategory);
        }
        public async Task<WareCategory2DTO> Update(WareCategory2DTO category2DTO) 
        {
            var wareCategory2 = _mapper.Map<WareCategory2DTO, WareCategory2>(category2DTO);
            Database.Categories2.Update(wareCategory2);
            await Database.Save();
            var returnedCategory = await Database.Categories2.GetById(wareCategory2.Id);
            return _mapper.Map<WareCategory2, WareCategory2DTO>(returnedCategory);
        }
        public async Task<WareCategory2DTO> Delete(long id)
        {
            var wareCategory2 = await Database.Categories2.GetById(id);
            await Database.Categories2.Delete(id);
            await Database.Save();
            return _mapper.Map<WareCategory2, WareCategory2DTO>(wareCategory2);
        }
    }
}
