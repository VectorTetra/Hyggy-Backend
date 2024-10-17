using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareCategory2Repository
    {
        Task<WareCategory2?> GetById(long id);

        Task<IEnumerable<WareCategory2>> GetPagedCategories(int pageNumber, int pageSize);
        Task<IEnumerable<WareCategory2>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareCategory2>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareCategory2>> GetByWareCategory1Id(long id);
        Task<IEnumerable<WareCategory2>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring);
        Task<IEnumerable<WareCategory2>> GetByWareCategory3Id(long id);
        Task<IEnumerable<WareCategory2>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring);
        Task<IEnumerable<WareCategory2>> GetByQuery(WareCategory2QueryDAL query);
        IAsyncEnumerable<WareCategory2> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WareCategory2 order);
        void Update(WareCategory2 order);
        Task Delete(long id);
    }
}
