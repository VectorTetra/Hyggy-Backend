using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareDTO
    {
        public long Id { get; set; }
        public long? Article { get; set; }
        public long? WareCategory3Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public float? Discount { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StatusId { get; set; }
        public ICollection<long>? ImageIds { get; set; } = new List<long>();
        public ICollection<long>? PriceHistoryIds { get; set; } = new List<long>();

        //public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
