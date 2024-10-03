
using System.ComponentModel.DataAnnotations.Schema;

namespace HyggyBackend.DAL.Entities.Employes
{
	public class StorageEmployee : Employee
	{
		//public long MainStorageId { get; set; }
		
		//public virtual MainStorage MainStorage { get; set; }
		public long StorageId { get; set; }
		public virtual Storage Storage { get; set; }
	}
}
