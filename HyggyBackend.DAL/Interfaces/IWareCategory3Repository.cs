﻿using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareCategory3Repository
    {
        Task<WareCategory3?> GetById(long id);

        Task<IEnumerable<WareCategory3>> GetPagedCategories(int pageNumber, int pageSize);
        Task<IEnumerable<WareCategory3>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareCategory3>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareCategory3>> GetByWareCategory1Id(long id);
        Task<IEnumerable<WareCategory3>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring);
        Task<IEnumerable<WareCategory3>> GetByWareCategory2Id(long id);
        Task<IEnumerable<WareCategory3>> GetByWareCategory2NameSubstring(string WareCategory3NameSubstring); 
        Task<IEnumerable<WareCategory3>> GetByWareId(long id);
        Task<IEnumerable<WareCategory3>> GetByWareArticle(long Article);
        Task<IEnumerable<WareCategory3>> GetByWareNameSubstring(string WareNameSubstring);
        Task<IEnumerable<WareCategory3>> GetByWareDescriptionSubstring(string WareDescriptionSubstring);
        Task<IEnumerable<WareCategory3>> GetByQuery(WareCategory3QueryDAL query);
        IAsyncEnumerable<WareCategory3> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WareCategory3 order);
        void Update(WareCategory3 order);
        Task Delete(long id);
    }
}
