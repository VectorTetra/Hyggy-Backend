using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareCategory1Repository
    {
        Task<WareCategory1?> GetById(long id);
        Task<IEnumerable<WareCategory1>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareCategory1>> GetByJSONStructureFilePathSubstring(string JSONStructureFilePathSubstring);
        Task<IEnumerable<WareCategory1>> GetByWareCategory2Id(long id);
        Task<IEnumerable<WareCategory1>> GetByWareCategory2NameSubstring(string WareCategory2NameSubstring);
        Task<IEnumerable<WareCategory1>> GetByWareCategory3Id(long id);
        Task<IEnumerable<WareCategory1>> GetByWareCategory3NameSubstring(string WareCategory3NameSubstring);        
        Task<IEnumerable<WareCategory1>> GetByQuery(WareCategory1QueryDAL query);
        Task Create(WareCategory1 order);
        void Update(WareCategory1 order);
        Task Delete(long id);
    }
}
