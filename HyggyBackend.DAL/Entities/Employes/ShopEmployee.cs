namespace HyggyBackend.DAL.Entities.Employes
{
	public class ShopEmployee : Employee
	{
        public virtual Shop Shop { get; set; }
    }
}
