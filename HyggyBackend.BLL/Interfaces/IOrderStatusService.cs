using HyggyBackend.BLL.DTO;
using HyggyBackend.BLL.Queries;
using HyggyBackend.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IOrderStatusService
    {
        Task<OrderStatusDTO?> GetById(long id);
        Task<IEnumerable<OrderStatusDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<OrderStatusDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<OrderStatusDTO>> GetByStringIds(string stringIds);
        Task<IEnumerable<OrderStatusDTO>> GetPaged(int pageNumber, int pageSize);
        Task<IEnumerable<OrderStatusDTO>> GetByOrderId(long orderId);
        Task<IEnumerable<OrderStatusDTO>> GetByQuery(OrderStatusQueryBLL query);
        Task<OrderStatusDTO> Create(OrderStatusDTO orderStatusDTO);
        Task<OrderStatusDTO> Update(OrderStatusDTO orderStatusDTO);
        Task<OrderStatusDTO> Delete(long id);
    }
}
