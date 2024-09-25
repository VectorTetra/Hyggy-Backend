using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public AddressDTO? DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public OrderStatusDTO? Status { get; set; }
        public ShopDTO? Shop { get; set; }
        public CustomerDTO? Customer { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
