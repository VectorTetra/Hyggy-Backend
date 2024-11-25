using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareCategory1DTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public ICollection<long>? WaresCategory2Ids { get; set; } = new List<long>();
        public ICollection<WareCategory2DTO>? WaresCategories2 { get; set; } = new List<WareCategory2DTO>();
    }
}
