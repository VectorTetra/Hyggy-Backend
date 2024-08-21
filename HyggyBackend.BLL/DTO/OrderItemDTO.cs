using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class OrderItemDTO
    {
        public long Id { get; set; }
        public int Count { get; set; }
        //public Order Order { get; set; }
        public WareDTO? Ware { get; set; }
        public WarePriceHistoryDTO? PriceHistory { get; set; }
    }
}
