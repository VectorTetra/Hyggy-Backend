using HyggyBackend.DAL.Entities.Employes;
using System.ComponentModel.DataAnnotations.Schema;

namespace HyggyBackend.DAL.Entities
{
    public class Storage
	{
		public long Id { get; set; }
        public long? AddressId { get; set; }  // Додаємо зовнішній ключ для Address
  //      public long? ShopId { get; set; }  // Додаємо зовнішній ключ для Shop
		//[ForeignKey("ShopId")]
		public virtual Shop? Shop { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }
        public virtual ICollection<WareItem> WareItems { get; set; } = new List<WareItem>();
        public virtual ICollection<StorageEmployee> StorageEmployees { get; set; } = new List<StorageEmployee>();

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
