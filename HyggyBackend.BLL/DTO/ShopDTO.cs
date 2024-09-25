using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class ShopDTO
    {
		public long Id { get; set; }
		public string PhotoUrl { get; set; } = String.Empty;
		public string WorkHours { get; set; } = String.Empty;
		public AddressDTO? Address { get; set; } = new AddressDTO();
        public StorageDTO? Storage { get; set; } = new StorageDTO();
		public ICollection<OrderDTO>? Orders { get; set; } = new List<OrderDTO>();
	}
}
