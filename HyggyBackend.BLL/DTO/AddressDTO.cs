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
        public long Id {  get; set; }

        // Адреса доставки, розділена на компоненти
        public string Street { get; set; } // Назва вулиці
        public string HouseNumber { get; set; } // Номер будинку
        public string City { get; set; } // Місто
        public string State { get; set; } // Область або штат
        public string PostalCode { get; set; } // Поштовий індекс



        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


		public virtual List<long> OrderIds { get; set; } = new List<long>();	

		//public virtual StorageDTO? Storage { get; set; }
    }
}
