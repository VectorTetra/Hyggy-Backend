namespace HyggyBackend.BLL.DTO
{
    public class WareImageDTO
    {
        public long Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public long WareId { get; set; }
        public string? StringIds { get; set; }
    }
}
