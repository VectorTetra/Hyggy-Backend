using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class WarePriceHistoryQueryBLL
    {
        public long? Id { get; set; }
        public long? WareId { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
