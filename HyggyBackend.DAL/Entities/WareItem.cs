namespace HyggyBackend.DAL.Entities
{
    public class WareItem
    {
        public long Id { get; set; }

        public virtual Ware Ware{ get; set; }

        public virtual Storage Storage { get; set; }

        public long Quantity { get; set; }
    }
}
