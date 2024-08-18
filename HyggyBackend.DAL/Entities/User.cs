using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
