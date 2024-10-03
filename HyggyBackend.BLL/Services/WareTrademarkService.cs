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
    public class WareTrademarkService : IWareTrademarkService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public WareTrademarkService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<WareTrademarkDTO?> GetById(long id)
        {
            var wareTrademark = await Database.WareTrademarks.GetById(id);
            return _mapper.Map<WareTrademarkDTO>(wareTrademark);
        }
        public async Task<IEnumerable<WareTrademarkDTO>> GetByName(string nameSubstr)
        {
            var wareTrademarks = await Database.WareTrademarks.GetByName(nameSubstr);
            return _mapper.Map<IEnumerable<WareTrademarkDTO>>(wareTrademarks);
        }
        public async Task<WareTrademarkDTO?> GetByWareId(long id)
        {
            var wareTrademark = await Database.WareTrademarks.GetByWareId(id);
            return _mapper.Map<WareTrademarkDTO>(wareTrademark);
        }
        public async Task<IEnumerable<WareTrademarkDTO>> GetPagedWareTrademarks(int pageNumber, int pageSize)
        {
            var wareTrademarks = await Database.WareTrademarks.GetPagedWareTrademarks(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WareTrademarkDTO>>(wareTrademarks);
        }
        public async Task<IEnumerable<WareTrademarkDTO>> GetByQuery(WareTrademarkQueryBLL query)
        {
            var wareTrademarks = await Database.WareTrademarks.GetByQuery(_mapper.Map<WareTrademarkQueryDAL>(query));
            return _mapper.Map<IEnumerable<WareTrademarkDTO>>(wareTrademarks);
        }
        public async Task<WareTrademarkDTO> Add(WareTrademarkDTO wareTrademark)
        {
            var existedName = await Database.WareTrademarks.GetByName(wareTrademark.Name);
            if (existedName.Any(x => x.Name == wareTrademark.Name))
            {
                throw new ValidationException($"Торгова марка з таким іменем вже існує", "");
            }
            var trademark = new WareTrademark
            {
                Name = wareTrademark.Name,
                Wares = new List<Ware>()
            };
            await Database.WareTrademarks.Add(trademark);
            await Database.Save();
            var returnedTrademark = await Database.WareTrademarks.GetById(trademark.Id);
            return _mapper.Map<WareTrademarkDTO>(returnedTrademark);
        }
        public async Task<WareTrademarkDTO> Update(WareTrademarkDTO wareTrademark) 
        {
            var existedName = await Database.WareTrademarks.GetByName(wareTrademark.Name);
            if (existedName.Any())
            {
                throw new ValidationException($"Торгова марка з таким іменем вже існує", "");
            }
            var trademark = await Database.WareTrademarks.GetById(wareTrademark.Id);
            if (trademark == null)
            {
                throw new ValidationException($"Торгова марка з id {wareTrademark.Id} не знайдена", "");
            }
            trademark.Name = wareTrademark.Name;
            trademark.Wares.Clear();
            await foreach (var bloggy in Database.Wares.GetByIdsAsync(wareTrademark.WareIds))
            {
                if (bloggy == null)
                {
                    throw new ValidationException($"Один з товарів не знайдений для копіювання в список товарів торгової марки!", "");
                }
                trademark.Wares.Add(bloggy);
            }
            Database.WareTrademarks.Update(trademark);
            await Database.Save();
            var returnedTrademark = await Database.WareTrademarks.GetById(trademark.Id);
            return _mapper.Map<WareTrademarkDTO>(returnedTrademark);
        }
        public async Task<WareTrademarkDTO> Delete(long id) 
        {
            var trademark = await Database.WareTrademarks.GetById(id);
            if (trademark == null)
            {
                throw new ValidationException($"Торгова марка з id {id} не знайдена", "");
            }
            await Database.WareTrademarks.Delete(id);
            await Database.Save();
            return _mapper.Map<WareTrademarkDTO>(trademark);
        }
    }
}
