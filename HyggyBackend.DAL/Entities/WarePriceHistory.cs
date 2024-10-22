using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HyggyBackend.DAL.Entities
{
    // Клас для зберігання історії цін на товари, дозволить користувачам переглядати вартість своїх замовлень,
    // які вони робили раніше, навіть якщо ціна на товар змінилася
    public class WarePriceHistory
    {
        public long Id { get; set; }
        public long WareId { get; set; }
        public virtual Ware Ware { get; set; }
        public float Price { get; set; }
        public DateTime EffectiveDate { get; set; } // Дата початку дії ціни

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (WarePriceHistory)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}