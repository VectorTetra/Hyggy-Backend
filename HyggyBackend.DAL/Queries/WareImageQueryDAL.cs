using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareImageQueryDAL
    {
        public long? Id { get; set; }
        public string? Path { get; set; }
        public long? WareId { get; set; }
        public long? WareArticle { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
