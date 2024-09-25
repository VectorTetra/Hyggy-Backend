using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class CustomerQueryBLL
    {
        public long? Id;
        public string? Name;
        public string? Surname;
        public string? Email;
        public string? Phone;
        public long? OrderId;
        public int? PageNumber;
        public int? PageSize;
    }
}
