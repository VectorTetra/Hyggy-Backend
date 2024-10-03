using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;
using HyggyBackend.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Services
{
    public class ProffessionService : IProffessionService
    {
        IUnitOfWork Database;
        IMapper _mapper;

        public ProffessionService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProffessionDTO>> GetAll()
        {
            
            return _mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(await Database.Proffessions.GetAll());
        }

        public async Task<ProffessionDTO?> GetById(long id)
        {
            
            var proffession = await Database.Proffessions.GetById(id);
            if (proffession == null)
            {
                return null;
            }
            return _mapper.Map<Proffession, ProffessionDTO>(proffession);
        }

        public async Task<IEnumerable<ProffessionDTO>> GetByName(string name)
        {
            
            var proffessions = await Database.Proffessions.GetByName(name);
            return _mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(proffessions);
        }

        public async Task<IEnumerable<ProffessionDTO>> GetByEmployeeName(string employeeName)
        {
            
            var proffessions = await Database.Proffessions.GetByEmployeeName(employeeName);
            return _mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(proffessions);
        }

        public async Task<IEnumerable<ProffessionDTO>> GetByEmployeeSurname(string employeeSurname)
        {
            
            var proffessions = await Database.Proffessions.GetByEmployeeSurname(employeeSurname);
            return _mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(proffessions);
        }

        public async Task<IEnumerable<ProffessionDTO>> GetByQuery(ProffessionQueryBLL queryDAL)
        {
            
           
            var query = _mapper.Map<ProffessionQueryBLL, ProffessionQueryDAL>(queryDAL);
            var proffessions = await Database.Proffessions.GetByQuery(query);
            return _mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(proffessions);
        }

        public async Task<ProffessionDTO> Create(ProffessionDTO proffessionDTO)
        {
            var existedNames = await Database.Proffessions.GetByName(proffessionDTO.Name);
            if (existedNames.Any(x => x.Name == proffessionDTO.Name))
            {
                throw new ValidationException("Роль з таким іменем вже існує!", proffessionDTO.Name);
            }
            
            var proffessionDAL = _mapper.Map<ProffessionDTO, Proffession>(proffessionDTO);

            await Database.Proffessions.Create(proffessionDAL);
            await Database.Save();

            proffessionDTO.Id = proffessionDAL.Id;
            return proffessionDTO;
        }

        public async Task<ProffessionDTO> Update(ProffessionDTO proffessionDTO)
        {
            var existingProffession = await Database.Proffessions.GetById(proffessionDTO.Id);
            if (existingProffession == null)
            {
                throw new ValidationException("Роль з таким ID не знайдено!", proffessionDTO.Id.ToString());
            }
            
            var proffessionDAL = _mapper.Map<ProffessionDTO, Proffession>(proffessionDTO);
            Database.Proffessions.Update(proffessionDAL);
            await Database.Save();
            return proffessionDTO;
        }

        public async Task<ProffessionDTO> Delete(long id)
        {
            var existedId = await Database.Proffessions.GetById(id);
            if (existedId == null)
            {
                throw new ValidationException("Ролі з таким ID не існує!", id.ToString());
            }
            
            await Database.Proffessions.Delete(id);
            await Database.Save();
            return _mapper.Map<Proffession, ProffessionDTO>(existedId);
        }
    }
}
