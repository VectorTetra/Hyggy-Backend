using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class Blog
    {
        public long Id { get; set; }
        public string BlogTitle { get; set; } = string.Empty; // Назва статті блогу
        public string? Keywords { get; set; } = string.Empty; // Ключові слова для статті блогу, які використовуються для пошуку
                                                             // наприклад, у форматі "слово1|слово2|слово3"
        public string FilePath { get; set; } = string.Empty; // Шлях до файлу із структурою статті блогу (файл повинен знаходитись у папці wwwroot)
        public string? PreviewImagePath { get; set; } = string.Empty; // Шлях до файлу із структурою статті блогу (файл повинен знаходитись у папці wwwroot)
        public virtual BlogCategory2 BlogCategory2 { get; set; } 
    }
}
