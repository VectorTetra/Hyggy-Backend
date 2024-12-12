using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class OrderDeliveryTypeDTO
    {
        public long Id { get; set; }
        public virtual ICollection<long>? OrderIds { get; set; } = new List<long>();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public int? MinDeliveryTimeInDays { get; set; }
        public int? MaxDeliveryTimeInDays { get; set; }
    }
}
