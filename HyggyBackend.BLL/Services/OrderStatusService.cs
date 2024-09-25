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
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderStatusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderStatusDTO?> GetById(long id)
        {
            var orderStatus = await _unitOfWork.OrderStatuses.GetById(id);
            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }

        public async Task<IEnumerable<OrderStatusDTO>> GetByNameSubstring(string nameSubstring)
        {
            var orderStatuses = await _unitOfWork.OrderStatuses.GetByNameSubstring(nameSubstring);
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(orderStatuses);
        }

        public async Task<IEnumerable<OrderStatusDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var orderStatuses = await _unitOfWork.OrderStatuses.GetByDescriptionSubstring(descriptionSubstring);
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(orderStatuses);
        }

        public async Task<IEnumerable<OrderStatusDTO>> GetByQuery(OrderStatusQueryBLL query)
        {
            var queryDAL = _mapper.Map<OrderStatusQueryDAL>(query);
            var orderStatuses = await _unitOfWork.OrderStatuses.GetByQuery(queryDAL);
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(orderStatuses);
        }

        public async Task<OrderStatusDTO> Create(OrderStatusDTO orderStatusDTO)
        {
            var orderStatus = _mapper.Map<OrderStatus>(orderStatusDTO);
            await _unitOfWork.OrderStatuses.Create(orderStatus);
            await _unitOfWork.Save();
            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }

        public async Task<OrderStatusDTO> Update(OrderStatusDTO orderStatusDTO)
        {
            var existingOrderStatus = await _unitOfWork.OrderStatuses.GetById(orderStatusDTO.Id);
            if (existingOrderStatus == null)
            {
                throw new ValidationException("OrderStatus не знайдено", "");
            }

            var orderStatus = _mapper.Map<OrderStatus>(orderStatusDTO);
            _unitOfWork.OrderStatuses.Update(orderStatus);
            await _unitOfWork.Save();
            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }

        public async Task<OrderStatusDTO> Delete(long id)
        {
            var orderStatus = await _unitOfWork.OrderStatuses.GetById(id);
            if (orderStatus == null)
            {
                throw new ValidationException("OrderStatus не знайдено", "");
            }

            await _unitOfWork.OrderStatuses.Delete(id);
            await _unitOfWork.Save();
            return _mapper.Map<OrderStatusDTO>(orderStatus);
        }
    }
}
