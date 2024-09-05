using HyggyBackend.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDTO?> GetById(long id);

        Task<IEnumerable<OrderItemDTO>> GetByOrderId(long orderId);
        Task<IEnumerable<OrderItemDTO>> GetByWareId(long wareId);
        Task<IEnumerable<OrderItemDTO>> GetByPriceHistoryId(long priceHistoryId);
        Task<IEnumerable<OrderItemDTO>> GetByCount(int count);

        Task<OrderItemDTO> Create(OrderItemDTO orderItemDTO);
        Task<OrderItemDTO> Update(OrderItemDTO orderItemDTO);
        Task<OrderItemDTO> Delete(long id);
    }
}
