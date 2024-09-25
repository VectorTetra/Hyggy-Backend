using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class CustomerQueryDAL
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
