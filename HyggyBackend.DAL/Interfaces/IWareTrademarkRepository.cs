﻿using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareTrademarkRepository
    {
        Task<WareTrademark?> GetById(long id);
        Task<IEnumerable<WareTrademark>> GetByName(string nameSubstr);
        Task<WareTrademark?> GetByWareId(long id);
        Task<IEnumerable<WareTrademark>> GetPagedWareTrademarks(int pageNumber, int pageSize);
        Task<IEnumerable<WareTrademark>> GetByQuery(WareTrademarkQueryDAL query);
        Task Add(WareTrademark wareTrademark);
        void Update(WareTrademark wareTrademark);
        Task Delete(long id);
    }
}