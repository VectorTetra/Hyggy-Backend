namespace HyggyBackend.DAL.Entities
{
    public class WareItem
    {
        public long Id { get; set; }

        public long WareId { get; set; }
        public virtual Ware Ware { get; set; }

        public long StorageId { get; set; }
        public virtual Storage Storage { get; set; }

        public long Quantity { get; set; }
    }
}
