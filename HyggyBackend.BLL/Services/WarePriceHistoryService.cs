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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WarePriceHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WarePriceHistoryDTO?> GetById(long id)
        {
            var warePriceHistory = await _unitOfWork.WarePriceHistories.GetById(id);
            return _mapper.Map<WarePriceHistoryDTO>(warePriceHistory);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByWareId(long wareId)
        {
            var warePriceHistories = await _unitOfWork.WarePriceHistories.GetByWareId(wareId);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByPriceRange(float minPrice, float maxPrice)
        {
            var warePriceHistories = await _unitOfWork.WarePriceHistories.GetByPriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var warePriceHistories = await _unitOfWork.WarePriceHistories.GetByDateRange(startDate, endDate);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<IEnumerable<WarePriceHistoryDTO>> GetByQuery(WarePriceHistoryQueryBLL query)
        {
            var queryDAL = _mapper.Map<WarePriceHistoryQueryDAL>(query);
            var warePriceHistories = await _unitOfWork.WarePriceHistories.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<WarePriceHistoryDTO>>(warePriceHistories);
        }

        public async Task<WarePriceHistoryDTO> Create(WarePriceHistoryDTO warePriceHistoryDTO)
        {
            var warePriceHistory = _mapper.Map<WarePriceHistory>(warePriceHistoryDTO);
            await _unitOfWork.WarePriceHistories.Create(warePriceHistory);
            await _unitOfWork.Save();
            return _mapper.Map<WarePriceHistoryDTO>(warePriceHistory);
        }

        public async Task<WarePriceHistoryDTO> Update(WarePriceHistoryDTO warePriceHistoryDTO)
        {
            var existingWarePriceHistory = await _unitOfWork.WarePriceHistories.GetById(warePriceHistoryDTO.Id);
            if (existingWarePriceHistory == null)
            {
                throw new ValidationException("WarePriceHistory не знайдено", "");
            }

            var warePriceHistory = _mapper.Map<WarePriceHistory>(warePriceHistoryDTO);
            _unitOfWork.WarePriceHistories.Update(warePriceHistory);
            await _unitOfWork.Save();
            return _mapper.Map<WarePriceHistoryDTO>(warePriceHistory);
        }

        public async Task<WarePriceHistoryDTO> Delete(long id)
        {
            var warePriceHistory = await _unitOfWork.WarePriceHistories.GetById(id);
            if (warePriceHistory == null)
            {
                throw new ValidationException("WarePriceHistory не знайдено", "");
            }

            await _unitOfWork.WarePriceHistories.Delete(id);
            await _unitOfWork.Save();
            return _mapper.Map<WarePriceHistoryDTO>(warePriceHistory);
        }
    }
}
