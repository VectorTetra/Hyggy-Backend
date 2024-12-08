using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareItemDTO
    {
        public long Id { get; set; }
        public long WareId { get; set; }
        public long StorageId { get; set; }
        public long Quantity { get; set; }
        public double TotalSum { get; set; }
        //public WareDTO? Ware { get; set; }
        public StorageDTO? Storage { get; set; }
    }
}
