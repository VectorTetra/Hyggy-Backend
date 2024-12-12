namespace HyggyBackend.DAL.Entities
{
    public class OrderDeliveryType
    {
        public long Id { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int MinDeliveryTimeInDays { get; set; }
        public int MaxDeliveryTimeInDays { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var otherTour = (OrderDeliveryType)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
