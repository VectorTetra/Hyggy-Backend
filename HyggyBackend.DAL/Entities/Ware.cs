using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class Ware
    {
        public long Id { get; set; }
        public long Article { get; set; }
        public virtual WareCategory3 WareCategory3 { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public bool IsDeliveryAvailable { get; set; }
        public virtual WareStatus Status { get; set; }
        public virtual ICollection<WareImage> Images { get; set; } = new List<WareImage>();
        public virtual ICollection<WareItem> WareItems { get; set; } = new List<WareItem>();
        public virtual ICollection<WarePriceHistory> PriceHistories { get; set; } = new List<WarePriceHistory>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Customer> CustomerFavorites { get; set; } = new List<Customer>();
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (Ware)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
