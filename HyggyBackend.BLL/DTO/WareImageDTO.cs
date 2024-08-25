namespace HyggyBackend.BLL.DTO
{
    public class WareImageDTO
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public WareDTO Ware { get; set; }
    }
}
