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
    public class WareCategory3Service : IWareCategory3Service
    {
        IUnitOfWork Database;
        IMapper _mapper;
        public WareCategory3Service(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<WareCategory3DTO?> GetById(long id)
        {

            var wareCategory3 = await Database.Categories3.GetById(id);
            return _mapper.Map<WareCategory3, WareCategory3DTO>(wareCategory3);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetPagedCategories(int pageNumber, int pageSize)
        {

            var wareCategory3s = await Database.Categories3.GetPagedCategories(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WareCategory3>, IEnumerable<WareCategory3DTO>>(wareCategory3s);
        }
        public async Task<IEnumerable<WareCategory3DTO>> GetByNameSubstring(string nameSubstring)
        {


            var wareCategory3s = await Database.Categories3.GetByNameSubstring(nameSubstring);
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
            // Перевірка на null з використанням ArgumentNullException
            var existingName = category3DTO.Name ?? throw new ValidationException("Не вказано WareCategory3.Name", nameof(category3DTO.Name));

            // Перевірка на існування категорії з такою назвою
            var existingCategoryName = await Database.Categories3.GetByNameSubstring(existingName);
            if (existingCategoryName.Any(x => x.Name == existingName))
                throw new ValidationException("WareCategory3 з такою назвою вже існує", "");

            // Перевірка існування WareCategory2Id та відповідної категорії
            var existingCategory2Id = category3DTO.WareCategory2Id ?? throw new ValidationException("Не вказано WareCategory3.WareCategory2Id", nameof(category3DTO.WareCategory2Id));
            var existingCategory2 = await Database.Categories2.GetById(existingCategory2Id)
                ?? throw new ValidationException("WareCategory2 з таким ідентифікатором не існує", "");

            

            // Створення нової категорії
            var wareCategory3 = new WareCategory3
            {
                Name = existingName,
                WareCategory2 = existingCategory2,
                Wares = new List<Ware>()
            };
            await Database.Categories3.Create(wareCategory3);
            await Database.Save();

            // Повернення результату
            var returnedCategory = await Database.Categories3.GetById(wareCategory3.Id);
            return _mapper.Map<WareCategory3, WareCategory3DTO>(returnedCategory);
        }

        public async Task<WareCategory3DTO> Update(WareCategory3DTO category3DTO)
        {

            // Перевірка на null з використанням ArgumentNullException
            var existingCategory3 = await Database.Categories3.GetById(category3DTO.Id)
                ?? throw new ValidationException($"WareCategory3 з id={category3DTO.Id} не знайдено", "");

            // Перевірка на null з використанням ArgumentNullException
            var existingName = category3DTO.Name ?? throw new ValidationException("Не вказано WareCategory3.Name", nameof(category3DTO.Name));

            // Перевірка на існування категорії з такою назвою
            var existingCategoryName = await Database.Categories3.GetByNameSubstring(existingName);
            if (existingCategoryName.Any(x => x.Name == existingName && x.Id != category3DTO.Id))
                throw new ValidationException($"WareCategory3 з такою назвою вже існує", "");

            // Перевірка існування WareCategory2Id та відповідної категорії
            var existingCategory2Id = category3DTO.WareCategory2Id ?? throw new ValidationException("Не вказано WareCategory3.WareCategory2Id", nameof(category3DTO.WareCategory2Id));
            var existingCategory2 = await Database.Categories2.GetById(existingCategory2Id)
                ?? throw new ValidationException("WareCategory2 з таким ідентифікатором не існує", "");

           

            // Оновлення категорії
            existingCategory3.Wares.Clear();
            await foreach (var ware in Database.Wares.GetByIdsAsync(category3DTO.WareIds))
            {
                if (ware == null)
                {
                    throw new ValidationException("Один з Ware не знайдено!", "");
                }
                existingCategory3.Wares.Add(ware);
            }
            existingCategory3.Name = existingName;
            existingCategory3.WareCategory2 = existingCategory2;

            Database.Categories3.Update(existingCategory3);
            await Database.Save();

            // Повернення результату
            var returnedCategory = await Database.Categories3.GetById(existingCategory3.Id);
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
