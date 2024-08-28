
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Entities
{
	public class GlobalStorage
	{
        public long Id { get; set; }
		public ICollection<Shop> Shops { get; set; } = new List<Shop>();
		public ICollection<StorageEmployee> Employees { get; set; } = new List<StorageEmployee>();
    }
}
