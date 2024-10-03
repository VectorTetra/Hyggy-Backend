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
        public string? CustomerId { get; set; }
        public ICollection<long>? OrderItemIds { get; set; }
    }
}
