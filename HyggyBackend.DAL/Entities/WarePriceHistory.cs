using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.DAL.Entities
{
    // Клас для зберігання історії цін на товари, дозволить користувачам переглядати вартість своїх замовлень,
    // які вони робили раніше, навіть якщо ціна на товар змінилася
    public class WarePriceHistory
    {
        public long Id { get; set; }
        public virtual Ware Ware { get; set; }
        public float Price { get; set; }
        public DateTime EffectiveDate { get; set; } // Дата початку дії ціни

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // Як цим користуватися під час замовлення?
        //public void AddOrderItem(Order order, Ware ware, int count)
        //{
        //    // Отримайте поточну ціну з історії
        //    var currentPriceRecord = _context.WarePriceHistories
        //        .Where(p => p.WareId == ware.Id && p.EffectiveDate <= DateTime.Now)
        //        .OrderByDescending(p => p.EffectiveDate)
        //        .FirstOrDefault();

        //    var orderItem = new OrderItem
        //    {
        //        Order = order,
        //        Ware = ware,
        //        Count = count,
        //        PriceHistoryId = currentPriceRecord.Id
        //    };

        //    order.OrderItems.Add(orderItem);
        //}
    }
}