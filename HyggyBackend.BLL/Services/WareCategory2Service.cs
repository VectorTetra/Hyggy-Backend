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
    public class WareCategory2Service: IWareCategory2Service
    {
        IUnitOfWork Database;
        IMapper _mapper;
        public WareCategory2Service(IUnitOfWork uow,IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
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

        public async Task<IEnumerable<WareCategory2DTO>> GetByStringIds(string stringIds)
        {
            var wareCategory2s = await Database.Categories2.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<WareCategory2>, IEnumerable<WareCategory2DTO>>(wareCategory2s);
        }
        public async Task<IEnumerable<WareCategory2DTO>> GetByNameSubstring(string nameSubstring)
        {
            
            var wareCategory2s = await Database.Categories2.GetByNameSubstring(nameSubstring);
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
            if(category2DTO.WareCategory1Id == null)
            {
                throw new ValidationException("Не вказано WareCategory2.WareCategory1Id", "");
            }
            if (category2DTO.Name == null)
            {
                throw new ValidationException("Не вказано WareCategory2.Name", "");
            }
            var existingCategoryName = await Database.Categories2.GetByNameSubstring(category2DTO.Name);
            if (existingCategoryName.Any(x => x.Name == category2DTO.Name))
            {
                throw new ValidationException("WareCategory2 з такою назвою вже існує", "");
            }
            var existedCategory1 = await Database.Categories1.GetById(category2DTO.WareCategory1Id.Value);
            if (existedCategory1 == null)
            {
                throw new ValidationException("WareCategory1 з таким ідентифікатором не існує", "");
            }
            var wareCategory2 = new WareCategory2
            {
                Name = category2DTO.Name,
                WareCategory1 = existedCategory1,
                WaresCategory3 = new List<WareCategory3>()
            };

            await Database.Categories2.Create(wareCategory2);
            await Database.Save();
            var returnedCategory = await Database.Categories2.GetById(wareCategory2.Id);
            return _mapper.Map<WareCategory2, WareCategory2DTO>(returnedCategory);
        }
        public async Task<WareCategory2DTO> Update(WareCategory2DTO category2DTO) 
        {
            var existingCategory2 = await Database.Categories2.GetById(category2DTO.Id);
            if (existingCategory2 == null)
            {
                throw new ValidationException($"WareCategory2 з id={category2DTO.Id} не знайдено!", "");
            }
            if (category2DTO.WareCategory1Id == null)
            {
                throw new ValidationException("Не вказано WareCategory2.WareCategory1Id", "");
            }
            if (category2DTO.Name == null)
            {
                throw new ValidationException("Не вказано WareCategory2.Name", "");
            }
            var existingCategoryName = await Database.Categories2.GetByNameSubstring(category2DTO.Name);
            if (existingCategoryName.Any(x => x.Name == category2DTO.Name))
            {
                throw new ValidationException("WareCategory2 з такою назвою вже існує", "");
            }
            var existedCategory1 = await Database.Categories1.GetById(category2DTO.WareCategory1Id.Value);
            if (existedCategory1 == null)
            {
                throw new ValidationException("WareCategory1 з таким ідентифікатором не існує", "");
            }

            existingCategory2.WaresCategory3.Clear();
            await foreach (var category3 in Database.Categories3.GetByIdsAsync(category2DTO.WaresCategory3Ids))
            {
                if (category3 == null)
                {
                    throw new ValidationException($"Одна з WareCategory3 не знайдена!", "");
                }
                existingCategory2.WaresCategory3.Add(category3);
            }
            existingCategory2.Name = category2DTO.Name;
            existingCategory2.WareCategory1 = existedCategory1;
            

            Database.Categories2.Update(existingCategory2);
            await Database.Save();
            var returnedCategory = await Database.Categories2.GetById(existingCategory2.Id);
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
