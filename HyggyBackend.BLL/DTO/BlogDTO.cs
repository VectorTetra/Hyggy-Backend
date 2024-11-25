namespace HyggyBackend.BLL.DTO
{
    public class BlogDTO
    {
        public long Id { get; set; }
        public string? BlogTitle { get; set; } = string.Empty; // Назва статті блогу
        public string? Keywords { get; set; } = string.Empty; // Ключові слова для статті блогу, які використовуються для пошуку
                                                              // наприклад, у форматі "слово1|слово2|слово3"
        public string? FilePath { get; set; } = string.Empty; // Шлях до файлу із структурою статті блогу (файл повинен знаходитись у папці wwwroot)
        public string? PreviewImagePath { get; set; } = string.Empty; // Шлях до файлу із структурою статті блогу (файл повинен знаходитись у папці wwwroot)
        public long? BlogCategory2Id { get; set; }
        public long? BlogCategory1Id { get; set; }
        public string? BlogCategory2Name { get; set; }
        public string? BlogCategory1Name { get; set; }
    }
}
