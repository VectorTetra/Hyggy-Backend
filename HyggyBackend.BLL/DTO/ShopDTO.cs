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
		public string? PhotoUrl { get; set; } = String.Empty;
		public string? WorkHours { get; set; } = String.Empty;
		public long? AddressId { get; set; }
        public long? StorageId { get; set; }
		public ICollection<long>? OrderIds { get; set; } = new List<long>();
		public ICollection<string>? ShopEmployeeIds { get; set; } = new List<string>();
	}
}
