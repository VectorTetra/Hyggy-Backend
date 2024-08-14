using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Interfaces
{
    public interface IProffessionRepository
    {
        IEnumerable<Proffession> GetAllProffessions();
        Proffession GetProffessionById(long id);
        void AddProffession(Proffession proffession);
        void UpdateProffession(Proffession proffession);
        void DeleteProffession(long id);
    }
}
