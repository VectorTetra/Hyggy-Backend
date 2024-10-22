using AutoMapper;
using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Infrastructure;
using HyggyBackend.BLL.Interfaces;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Entities;
using HyggyBackend.DAL.Interfaces;
using HyggyBackend.DAL.Queries;

namespace HyggyBackend.BLL.Services
{
    public class WarePriceHistoryService : IWarePriceHistoryService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;

        public WarePriceHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WarePriceHistoryDTO?> GetById(long id)
        {
            var warePriceHistory = await Database.WarePriceHistories.GetById(id);
            return _mapper.Map<WarePriceHistoryDTO>(warePriceHistory);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetPaged(int pageNumber, int pageSize)
        {
            var warePriceHistories = await Database.WarePriceHistories.GetPaged(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByStringIds(string stringIds)
        {
            var warePriceHistories = await Database.WarePriceHistories.GetByStringIds(stringIds);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByWareId(long wareId)
        {
            var warePriceHistories = await Database.WarePriceHistories.GetByWareId(wareId);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByPriceRange(float minPrice, float maxPrice)
        {
            var warePriceHistories = await Database.WarePriceHistories.GetByPriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var warePriceHistories = await Database.WarePriceHistories.GetByDateRange(startDate, endDate);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByQuery(WarePriceHistoryQueryBLL query)
        {
            var queryDAL = _mapper.Map<WarePriceHistoryQueryDAL>(query);
            var warePriceHistories = await Database.WarePriceHistories.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<WarePriceHistoryDTO> Create(WarePriceHistoryDTO warePriceHistoryDTO)
        {
            var existedWare = await Database.Wares.GetById(warePriceHistoryDTO.WareId.Value);
            if (existedWare == null)
            {
                throw new ValidationException($"Товар з вказаним Id {warePriceHistoryDTO.WareId} не знайдено!", "");
            }
            if (!warePriceHistoryDTO.EffectiveDate.HasValue)
            {
                throw new ValidationException($"Не вказано дату початку дії WarePriceHistory!", "");
            }
            if (warePriceHistoryDTO.Price == null)
            {
                throw new ValidationException($"Не вказано ціну товару для WarePriceHistory!", "");
            }
            if (warePriceHistoryDTO.Price.Value < 0)
            {
                throw new ValidationException($"Ціна товару не може бути менше 0!", "");
            }
            var prHis = new WarePriceHistory
            {
                Ware = existedWare,
                Price = warePriceHistoryDTO.Price.Value,
                EffectiveDate = warePriceHistoryDTO.EffectiveDate.Value
            };

            await Database.WarePriceHistories.Create(prHis);
            await Database.Save();
            return _mapper.Map<WarePriceHistoryDTO>(prHis);
        }

        public async Task<WarePriceHistoryDTO> Update(WarePriceHistoryDTO warePriceHistoryDTO)
        {
            var existingWarePriceHistory = await Database.WarePriceHistories.GetById(warePriceHistoryDTO.Id);
            if (existingWarePriceHistory == null)
            {
                throw new ValidationException($"WarePriceHistory з таким Id {warePriceHistoryDTO.Id} не знайдено!", "");
            }
            var existedWare = await Database.Wares.GetById(warePriceHistoryDTO.WareId.Value);
            if (existedWare == null)
            {
                throw new ValidationException($"Товар з вказаним Id {warePriceHistoryDTO.WareId} не знайдено!", "");
            }
            if (!warePriceHistoryDTO.EffectiveDate.HasValue)
            {
                throw new ValidationException($"Не вказано дату початку дії WarePriceHistory!", "");
            }
            if (warePriceHistoryDTO.Price == null)
            {
                throw new ValidationException($"Не вказано ціну товару для WarePriceHistory!", "");
            }
            if (warePriceHistoryDTO.Price.Value < 0)
            {
                throw new ValidationException($"Ціна товару не може бути менше 0!", "");
            }

            existingWarePriceHistory.Ware = existedWare;
            existingWarePriceHistory.Price = warePriceHistoryDTO.Price.Value;
            existingWarePriceHistory.EffectiveDate = warePriceHistoryDTO.EffectiveDate.Value;

            Database.WarePriceHistories.Update(existingWarePriceHistory);
            await Database.Save();
            return _mapper.Map<WarePriceHistoryDTO>(existingWarePriceHistory);
        }

        public async Task<WarePriceHistoryDTO> Delete(long id)
        {
            var warePriceHistory = await Database.WarePriceHistories.GetById(id);
            if (warePriceHistory == null)
            {
                throw new ValidationException("WarePriceHistory не знайдено", "");
            }

            await Database.WarePriceHistories.Delete(id);
            await Database.Save();
            return _mapper.Map<WarePriceHistoryDTO>(warePriceHistory);
        }
    }
}
