using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WarePriceHistoryDTO
    {
        public long Id { get; set; }
        public long? WareId { get; set; }
        public float? Price { get; set; }
        public DateTime? EffectiveDate { get; set; } // Дата початку дії ціни

        //public ICollection<OrderItem> OrderItems { get; set; }
    }
}
