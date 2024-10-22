using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class BlogCategory2QueryBLL
    {
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogCategory1Id { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogId { get; set; }
        public string? BlogCategory2Name { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny {get;set;}
    }
}
