using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IWareStatusRepository
    {
        Task<WareStatus?> GetById(long id);
        Task<WareStatus?> GetByWareId(long id);
        Task<WareStatus?> GetByWareArticle(long article);
        Task<IEnumerable<WareStatus>> GetPagedWareStatuses(int pageNumber, int pageSize);
        Task<IEnumerable<WareStatus>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareStatus>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareStatus>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<WareStatus>> GetByQuery(WareStatusQueryDAL queryDAL);
        IAsyncEnumerable<WareStatus> GetByIdsAsync(IEnumerable<long> ids);
        Task Create(WareStatus ware);
        void Update(WareStatus ware);
        Task Delete(long id);
    }
}
