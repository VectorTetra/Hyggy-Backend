using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareCategory2DTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? WareCategory1Id { get; set; }
        public ICollection<long>? WaresCategory3Ids { get; set; } = new List<long>();
        public ICollection<WareCategory3DTO>? WaresCategories3 { get; set; } = new List<WareCategory3DTO>();
        public string? StringIds { get; set; }
    }
}
