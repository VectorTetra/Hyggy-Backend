using HyggyBackend.DAL.Entities;

namespace HyggyBackend.BLL.DTO
{
    public class BlogCategory1DTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty; // Назва категорії блогу
        public ICollection<long> BlogCategory2Ids { get; set; } = new List<long>();
        public string? StringIds { get; set; }
    }
}
