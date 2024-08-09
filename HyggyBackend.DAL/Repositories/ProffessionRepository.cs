using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Repositories
{
    public class ProffessionRepository : IProffessionRepository
    {
        private readonly HyggyContext _context;

        public ProffessionRepository(HyggyContext context)
        {
            _context = context;
        }

        public IEnumerable<Proffession> GetAllProffessions()
        {
            return _context.Proffessions.ToList();
        }

        public Proffession GetProffessionById(long id)
        {
            return _context.Proffessions.Find(id);
        }

        public void AddProffession(Proffession proffession)
        {
            _context.Proffessions.Add(proffession);
            _context.SaveChanges();
        }

        public void UpdateProffession(Proffession proffession)
        {
            _context.Proffessions.Update(proffession);
            _context.SaveChanges();
        }

        public void DeleteProffession(long id)
        {
            var proffession = _context.Proffessions.Find(id);
            if (proffession != null)
            {
                _context.Proffessions.Remove(proffession);
                _context.SaveChanges();
            }
        }
    }
}
