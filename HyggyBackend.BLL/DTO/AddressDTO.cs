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

        // Адреса доставки, розділена на компоненти
        public string Street { get; set; } // Назва вулиці
        public string HouseNumber { get; set; } // Номер будинку
        public string City { get; set; } // Місто
        public string State { get; set; } // Область або штат
        public string PostalCode { get; set; } // Поштовий індекс



        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


		public virtual ICollection<OrderDTO> Orders { get; set; } = new List<OrderDTO>();	

		public virtual ShopDTO? Shop { get; set; }
    }
}
