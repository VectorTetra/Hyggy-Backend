using System.ComponentModel.DataAnnotations.Schema;

namespace HyggyBackend.DAL.Entities
{
    public class Storage
	{
		public long Id { get; set; }

		public virtual Shop? Shop { get; set; }

        public long? AddressId { get; set; }  // Додаємо зовнішній ключ для Address
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; }
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
