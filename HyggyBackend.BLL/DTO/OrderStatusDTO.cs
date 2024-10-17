using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class OrderStatusDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public ICollection<long> OrderIds { get; set; } = new List<long>();
        public string? StringIds { get; set; }
    }
}
