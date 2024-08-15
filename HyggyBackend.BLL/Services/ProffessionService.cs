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

        MapperConfiguration Proffession_ProffessionDTOMapConfig = new MapperConfiguration(cfg => 
        cfg.CreateMap<Proffession, ProffessionDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        );

        MapperConfiguration ProffessionQueryBLL_ProffessionQueryDALMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProffessionQueryBLL, ProffessionQueryDAL>());

        public ProffessionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<IEnumerable<ProffessionDTO>> GetAll()
        {
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            return mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(await Database.Proffessions.GetAll());
        }

        public async Task<ProffessionDTO?> GetById(long id)
        {
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            var proffession = await Database.Proffessions.GetById(id);
            if (proffession == null)
            {
                return null;
            }
            return mapper.Map<Proffession, ProffessionDTO>(proffession);
        }

        public async Task<IEnumerable<ProffessionDTO>> GetByName(string name)
        {
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            var proffessions = await Database.Proffessions.GetByName(name);
            return mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(proffessions);
        }

        public async Task<IEnumerable<ProffessionDTO>> GetByQuery(ProffessionQueryBLL queryDAL)
        {
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            var queryMapper = new Mapper(ProffessionQueryBLL_ProffessionQueryDALMapConfig);
            var query = queryMapper.Map<ProffessionQueryBLL, ProffessionQueryDAL>(queryDAL);
            var proffessions = await Database.Proffessions.GetByQuery(query);
            return mapper.Map<IEnumerable<Proffession>, IEnumerable<ProffessionDTO>>(proffessions);
        }

        public async Task<ProffessionDTO> Create(ProffessionDTO proffessionDTO)
        {
            var existedNames = await Database.Wares.GetByNameSubstring(proffessionDTO.Name);
            if (existedNames.Any(x => x.Name == proffessionDTO.Name))
            {
                throw new ValidationException("Роль з таким іменем вже існує!", proffessionDTO.Name);
            }
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            var proffessionDAL = mapper.Map<ProffessionDTO, Proffession>(proffessionDTO);

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
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            var proffessionDAL = mapper.Map<ProffessionDTO, Proffession>(proffessionDTO);
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
            var mapper = new Mapper(Proffession_ProffessionDTOMapConfig);
            await Database.Proffessions.Delete(id);
            await Database.Save();
            return mapper.Map<Proffession, ProffessionDTO>(existedId);
        }
    }
}
