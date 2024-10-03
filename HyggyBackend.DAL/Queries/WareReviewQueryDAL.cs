using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareReviewQueryDAL
    {
        public long? Id { get; set; }
        public long? WareId { get; set; }
        public string? Text { get; set; }
        public string? Theme { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
        public short? MaxRating { get; set; }
        public short? MinRating { get; set; }
        public DateTime? MaxDate { get; set; }
        public DateTime? MinDate { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
    }
}
