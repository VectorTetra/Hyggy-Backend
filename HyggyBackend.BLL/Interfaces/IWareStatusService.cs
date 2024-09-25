using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareStatusService
    {
        Task<WareStatusDTO?> GetById(long id);
        Task<WareStatusDTO?> GetByWareId(long id);
        Task<WareStatusDTO?> GetByWareArticle(long article);
        Task<IEnumerable<WareStatusDTO>> GetPagedWareStatuses(int pageNumber, int pageSize);
        Task<IEnumerable<WareStatusDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareStatusDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<WareStatusDTO>> GetByQuery(WareStatusQueryBLL queryBLL);
        Task<WareStatusDTO?> Create(WareStatusDTO wareDTO);
        Task<WareStatusDTO?> Update(WareStatusDTO wareDTO);
        Task<WareStatusDTO?> Delete(long id);
    }
}
