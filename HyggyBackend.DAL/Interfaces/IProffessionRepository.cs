using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IProffessionRepository
    {
        Task<Proffession?> GetById(long id);

        Task<IEnumerable<Proffession>> GetAll();
        Task<IEnumerable<Proffession>> GetByName(string name);
        Task<IEnumerable<Proffession>> GetByQuery(ProffessionQueryDAL proffessionDAL);

        Task Create(Proffession proffession);
        void Update(Proffession proffession);
        Task Delete(long id);
    }
}
