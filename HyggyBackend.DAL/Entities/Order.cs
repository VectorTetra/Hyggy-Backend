namespace HyggyBackend.DAL.Entities
{
    public class Order
    {
        public long Id { get; set; }
        //public string DeliveryAddress { get; set; }
        public Address DeliveryAddress { get; set; }

        // Дата замовлення дозволить відсортувати замовлення за датою,
        // а також дізнатися ціну певного товару за певний період у таблиці DateWarePrice 
        public DateTime OrderDate { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Shop Shop { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (Order)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
