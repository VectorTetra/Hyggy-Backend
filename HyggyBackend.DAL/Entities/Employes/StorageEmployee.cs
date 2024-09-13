
namespace HyggyBackend.DAL.Entities.Employes
{
	public class StorageEmployee : Employee
	{
		 public virtual MainStorage MainStorage { get; set; }
		 //public virtual Storage Storage { get; set; }
	}
}
