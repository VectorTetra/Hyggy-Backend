using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class WareCategory1QueryBLL
    {
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public long? WareCategory2Id { get; set; }
        public string? WareCategory2NameSubstring { get; set; }
        public long? WareCategory3Id { get; set; }
        public string? WareCategory3NameSubstring { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
