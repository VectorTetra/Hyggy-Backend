namespace HyggyBackend.DAL.Entities.Employes
{
    public class ShopEmployee : Employee
    {
        public long ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
