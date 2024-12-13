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
        public long? DeliveryAddressId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
        public long? StatusId { get; set; }
        public long? ShopId { get; set; }
        public long? DeliveryTypeId { get; set; }
        public string? CustomerId { get; set; }
        public float? TotalPrice { get; set; }
        public ICollection<long>? OrderItemIds { get; set; } = new List<long>();
        public ICollection<OrderItemDTO>? OrderItems { get; set; } = new List<OrderItemDTO>();
        public CustomerDTO? Customer { get; set; } 
        public ShopDTO? Shop { get; set; } 
        public OrderStatusDTO? Status { get; set; }
        public AddressDTO? DeliveryAddress { get; set; }
        public OrderDeliveryTypeDTO? DeliveryType { get; set; }
    }
}
