using HyggyBackend.DAL.EF;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HyggyBackend.DAL.Queries;
using HyggyBackend.DAL.Entities.Employes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace HyggyBackend.DAL.Repositories
{
    public class ProffessionRepository : IProffessionRepository
    {
        private readonly HyggyContext _context;

        public ProffessionRepository(HyggyContext context)
        {
            _context = context;
        }

        public async Task<Proffession?> GetById(long id)
        {
            return await _context.Proffessions.FindAsync(id);
        }

        public async Task<IEnumerable<Proffession>> GetAll()
        {
            return await _context.Proffessions.ToListAsync();
        }

        public async Task<IEnumerable<Proffession>> GetByName(string name)
        {
            return await _context.Proffessions.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Proffession>> GetByEmployeeName(string employeeName)
        {
            //return await _context.Proffessions
            //    .Where(x => x.Employes.Any(x => x.Name.Contains(employeeName))).ToListAsync();
            return null;
        }

        public async Task<IEnumerable<Proffession>> GetByEmployeeSurname(string employeeSurname)
        {
            //return await _context.Proffessions
            //    .Where(x => x.Employes.Any(x => x.Name.Contains(employeeSurname))).ToListAsync();
            return null;
        }

        public async Task<IEnumerable<Proffession>> GetByQuery(ProffessionQueryDAL queryDAL)
        {
            var proffessionCollections = new List<IEnumerable<Proffession>>();

            if (queryDAL.Id != null)
            {
                proffessionCollections.Add(new List<Proffession> { await GetById(queryDAL.Id.Value) });
            }

            if (queryDAL.Name != null)
            {
                proffessionCollections.Add(await GetByName(queryDAL.Name));
            }

            if (!proffessionCollections.Any())
            {
                return new List<Proffession>();
            }

            return proffessionCollections.Aggregate((prev, next) => prev.Intersect(next).ToList());
        }

        public async Task Create(Proffession proffession)
        {
            await _context.Proffessions.AddAsync(proffession);
        }

        public void Update(Proffession proffession)
        {
            _context.Entry(proffession).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var proffession = await GetById(id);
            if (proffession != null)
            {
                _context.Proffessions.Remove(proffession);
            }
        }
    }
}
