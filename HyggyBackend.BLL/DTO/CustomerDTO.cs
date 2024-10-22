using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class CustomerDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Token { get; set; }
        public ICollection<long>? OrderIds { get; set; } = new List<long>();
        public ICollection<long>? FavoriteWareIds { get; set; } = new List<long>();

        public double? ExecutedOrdersSum { get; set; }
        public double? ExecutedOrdersAvg { get; set; }
    }
}
