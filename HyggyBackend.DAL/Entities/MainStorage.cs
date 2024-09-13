
using HyggyBackend.DAL.Entities.Employes;
using System.ComponentModel.DataAnnotations.Schema;

namespace HyggyBackend.DAL.Entities
{
	public class MainStorage
	{
        public long Id { get; set; }
        public long? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address? Address { get; set; } 
        public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
		public virtual ICollection<StorageEmployee> Employees { get; set; } = new List<StorageEmployee>();
    }
}
