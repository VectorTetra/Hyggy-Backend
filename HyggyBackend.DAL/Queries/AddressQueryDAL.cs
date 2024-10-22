using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class AddressQueryDAL
    {
        public long? Id { get; set; }

        // Адреса доставки, розділена на компоненти
        public string? Street { get; set; } // Назва вулиці
        public string? HouseNumber { get; set; } // Номер будинку
        public string? City { get; set; } // Місто
        public string? State { get; set; } // Область або штат
        public string? PostalCode { get; set; } // Поштовий індекс

        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        //Адреса магазину
        public long? ShopId { get; set; }
        //Адреса складу
        public long? StorageId { get; set; }
        public long? OrderId { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; } 
        public string? Sorting { get; set; }
        public string? QueryAny {get;set;}
    }
}
