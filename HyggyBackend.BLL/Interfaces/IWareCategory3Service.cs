using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IWareCategory3Service
    {
        Task<WareCategory3DTO?> GetById(long id);
        Task<IEnumerable<WareCategory3DTO>> GetPagedCategories(int pageNumber, int pageSize);
        Task<IEnumerable<WareCategory3DTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<WareCategory3DTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<WareCategory3DTO>> GetByWareCategory1Id(long id);
        Task<IEnumerable<WareCategory3DTO>> GetByWareCategory1NameSubstring(string WareCategory1NameSubstring);
        Task<IEnumerable<WareCategory3DTO>> GetByWareCategory2Id(long id);
        Task<IEnumerable<WareCategory3DTO>> GetByWareCategory2NameSubstring(string WareCategory3NameSubstring);
        Task<IEnumerable<WareCategory3DTO>> GetByWareId(long id);
        Task<IEnumerable<WareCategory3DTO>> GetByWareArticle(long Article);
        Task<IEnumerable<WareCategory3DTO>> GetByWareNameSubstring(string WareNameSubstring);
        Task<IEnumerable<WareCategory3DTO>> GetByWareDescriptionSubstring(string WareDescriptionSubstring);
        Task<IEnumerable<WareCategory3DTO>> GetByQuery(WareCategory3QueryBLL query);
        Task<WareCategory3DTO> Create(WareCategory3DTO order);
        Task<WareCategory3DTO> Update(WareCategory3DTO order);
        Task<WareCategory3DTO> Delete(long id);
    }
}
