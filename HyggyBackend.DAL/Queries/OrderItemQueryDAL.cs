using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class OrderItemQueryDAL
    {
        public long? Id;
        public long? OrderId;
        public long? WareId;
        public int? OrderCount { get; set; }
    }
}
