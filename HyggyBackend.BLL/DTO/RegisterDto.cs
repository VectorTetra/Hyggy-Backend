using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
	public class RegisterDto
	{
        public string UserName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
		public string Password { get; set; } = String.Empty;
        public long ShopId { get; set; }
    }
}
