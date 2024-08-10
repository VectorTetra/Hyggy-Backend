using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
	public class AddressDTO
	{
		public long Id;

		public string Street { get; set; } = String.Empty;
		public string HouseNumber { get; set; } = String.Empty;
		public string City { get; set; } = String.Empty;
		public string State { get; set; } = String.Empty;
		public string PostalCode { get; set; } = String.Empty;



		public double? Latitude { get; set; }
		public double? Longitude { get; set; }


		public virtual ICollection<OrderDTO> Orders { get; set; } = new List<OrderDTO>();	

		public virtual ShopDTO? Shop { get; set; }
	}
}
