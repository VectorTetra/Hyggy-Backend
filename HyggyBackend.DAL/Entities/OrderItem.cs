using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public long? WareId { get; set; }
        [ForeignKey("WareId")]
        public int OrderCount { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Ware? Ware { get; set; }
        public virtual WarePriceHistory? PriceHistory { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (OrderItem)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
