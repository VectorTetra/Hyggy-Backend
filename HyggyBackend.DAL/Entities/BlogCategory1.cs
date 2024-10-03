namespace HyggyBackend.DAL.Entities
{
    public class BlogCategory1
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty; // Назва категорії блогу
        public virtual ICollection<BlogCategory2> BlogCategories2 { get; set; } = new List<BlogCategory2>();
    }
}
