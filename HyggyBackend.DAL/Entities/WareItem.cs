namespace HyggyBackend.DAL.Entities
{
    public class WareItem
    {
        public long Id { get; set; }

        public Ware Ware{ get; set; }

        public Storage Storage { get; set; }

        public long Quantity { get; set; }
    }
}
