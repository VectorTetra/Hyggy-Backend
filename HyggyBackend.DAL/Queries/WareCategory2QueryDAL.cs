using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareCategory2QueryDAL
    {
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public long? WareCategory1Id { get; set; }
        public string? WareCategory1NameSubstring { get; set; }
        public long? WareCategory3Id { get; set; }
        public string? WareCategory3NameSubstring { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
