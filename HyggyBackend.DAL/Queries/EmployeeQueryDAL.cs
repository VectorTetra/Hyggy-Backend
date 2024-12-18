using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class EmployeeQueryDAL
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? RoleName{ get; set; }
        public int? PageNumber{get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny { get; set; }
        public string? StringIds { get; set; }
    }
}
