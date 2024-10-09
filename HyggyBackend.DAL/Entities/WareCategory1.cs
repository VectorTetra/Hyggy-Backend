namespace HyggyBackend.DAL.Entities
{
    public class WareCategory1
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<WareCategory2> WaresCategory2 { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (WareCategory1)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
