using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IProffessionService
    {
        Task<ProffessionDTO?> GetById(long id);

        Task<IEnumerable<ProffessionDTO>> GetAll();
        Task<IEnumerable<ProffessionDTO>> GetByName(string name);
        Task<IEnumerable<ProffessionDTO>> GetByQuery(ProffessionQueryBLL queryDAL);

        Task<ProffessionDTO> Create(ProffessionDTO proffessionDTO);
        Task<ProffessionDTO> Update(ProffessionDTO proffessionDTO);
        Task<ProffessionDTO> Delete(long id);
    }
}
