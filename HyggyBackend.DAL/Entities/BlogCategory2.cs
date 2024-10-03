namespace HyggyBackend.DAL.Entities
{
    public class BlogCategory2
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty; // Назва категорії блогу
        public string PreviewImagePath { get; set; } = string.Empty; // Шлях до фото, яке показується на панелі категорій блогу
        public virtual BlogCategory1 BlogCategory1 { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
