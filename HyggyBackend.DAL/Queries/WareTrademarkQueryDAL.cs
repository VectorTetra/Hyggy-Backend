using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareTrademarkQueryDAL
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public long? WareId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
    }

}
