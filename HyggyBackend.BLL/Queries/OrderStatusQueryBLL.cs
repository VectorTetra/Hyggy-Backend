using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class OrderStatusQueryBLL
    {
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
    }
}
