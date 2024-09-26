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
        IMapper _mapper;
        public WareCategory1Service(IUnitOfWork uow)
        {
            Database = uow;
        }

       

        public async Task<WareCategory1DTO?> GetById(long id)
        {
            WareCategory1 wareCategory1 = await Database.Categories1.GetById(id);
            if (wareCategory1 == null)
            {
                return null;
            }
            
            return _mapper.Map<WareCategory1DTO>(wareCategory1);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetPagedCategories(pageNumber, pageSize);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByNameSubstring(string nameSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByNameSubstring(nameSubstring);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByJSONStructureFilePathSubstring(JSONStructureFilePathSubstring);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory2Id(long id)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory2Id(id);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory2NameSubstring(string WareCategory2NameSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory2NameSubstring(WareCategory2NameSubstring);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory3Id(long id)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory3Id(id);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring)
        {
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByWareCategory3NameSubstring(WareCategory3NameSubstring);
            
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<IEnumerable<WareCategory1DTO>> GetByQuery(WareCategory1QueryBLL query)
        {
            WareCategory1QueryDAL queryDAL = _mapper.Map<WareCategory1QueryDAL>(query);
            IEnumerable<WareCategory1> wareCategory1s = await Database.Categories1.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<WareCategory1DTO>>(wareCategory1s);
        }
        public async Task<WareCategory1DTO> Create(WareCategory1DTO category1DTO)
        {
            var wareCategory1 = _mapper.Map<WareCategory1DTO, WareCategory1>(category1DTO);
            await Database.Categories1.Create(wareCategory1);
            await Database.Save();
            var returnedCategory = await Database.Categories1.GetById(wareCategory1.Id);
            return _mapper.Map<WareCategory1, WareCategory1DTO>(returnedCategory);
        }
        public async Task<WareCategory1DTO> Update(WareCategory1DTO category1DTO) 
        {
            var wareCategory1 = _mapper.Map<WareCategory1DTO, WareCategory1>(category1DTO);
            Database.Categories1.Update(wareCategory1);
            await Database.Save();
            var returnedCategory = await Database.Categories1.GetById(wareCategory1.Id);
            return _mapper.Map<WareCategory1, WareCategory1DTO>(returnedCategory);
        }
        public async Task<WareCategory1DTO> Delete(long id)
        {
            var wareCategory1 = await Database.Categories1.GetById(id);
            await Database.Categories1.Delete(id);
            await Database.Save();
            return _mapper.Map<WareCategory1, WareCategory1DTO>(wareCategory1);
        }

    }
}
