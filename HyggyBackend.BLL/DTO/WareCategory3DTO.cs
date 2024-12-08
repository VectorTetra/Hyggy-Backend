using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareCategory3DTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? WareCategory2Id { get; set; }
        public string? WareCategory1Name { get; set; }
        public string? WareCategory2Name { get; set; }
        public ICollection<long>? WareIds { get; set; } = new List<long>();
    }
}
