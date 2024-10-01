namespace HyggyBackend.BLL.DTO
{
    public class BlogCategory2DTO
    {
        public long Id { get; set; }
        public string? Name { get; set; } = string.Empty; // Назва категорії блогу
        public string? PreviewImagePath { get; set; } = string.Empty; // Шлях до фото, яке показується на панелі категорій блогу
        public long? BlogCategory1Id { get; set; }
        public ICollection<long>? BlogIds { get; set; } = new List<long>();
    }
}
