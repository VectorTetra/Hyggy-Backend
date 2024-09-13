using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
	public class AuthenticationDto
	{
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
