using System.ComponentModel.DataAnnotations.Schema;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Entities
{
    public class Shop
	{
        public long Id { get; set; }
        public string PhotoUrl { get; set; } = String.Empty;
        public string WorkHours { get; set; } = String.Empty;
        public virtual Address Address { get; set; }
        public virtual Storage Storage { get; set; }
		public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();	
		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
       


        public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var otherTour = (Shop)obj;
			return Id == otherTour.Id;
		}
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
