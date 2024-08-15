using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Entities
{
    public class Storage
	{
		public long Id { get; set; }

		public virtual Shop? Shop { get; set; }

		public virtual Address? Address { get; set; }
		public virtual ICollection<Employee> Employee { get; set; } = new List<Employee>();
        public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var otherTour = (Storage)obj;
			return Id == otherTour.Id;
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
