using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareStatusQueryDAL
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public long? Id { get; set; }
        public long? WareId { get; set; }
        public long? WareArticle { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
    }
}
