using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class BlogQueryBLL
    {
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogCategory1Id { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogCategory2Id { get; set; }
        public string? BlogCategory2Name { get; set; }
        public int? PageNumber { get; set; }
        public string? StringIds { get; set; }
        public string? StringBlogCategory1Ids { get; set; }
        public string? StringBlogCategory2Ids { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
    }
}
