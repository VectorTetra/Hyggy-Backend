using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class OrderItemQueryDAL
    {
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public long? WareId { get; set; }
        public long? PriceHistoryId { get; set; }
        public int? Count { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny {get;set;}
    }
}
