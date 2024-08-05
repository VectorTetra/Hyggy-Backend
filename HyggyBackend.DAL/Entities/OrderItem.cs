using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual Ware Ware { get; set; }
        // Ідентифікатор запису у таблиці з історією цін
        public long PriceHistoryId { get; set; }
        public virtual WarePriceHistory PriceHistory { get; set; }

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
