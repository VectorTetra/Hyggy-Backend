using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class StorageDTO
    {
        public long Id { get; set; }
        public long? ShopId { get; set; }
        public long? AddressId { get; set; }

        // Для результатів Get-запитів
        public string? ShopName { get; set; } // Назва вулиці
        public string? Street { get; set; } // Назва вулиці
        public string? HouseNumber { get; set; } // Номер будинку
        public string? City { get; set; } // Місто
        public string? State { get; set; } // Область або штат
        public string? PostalCode { get; set; } // Поштовий індекс
        public double? Latitude { get; set; } // Географічна широта
        public double? Longitude { get; set; } // Географічна довгота
        public double? StoredWaresSum { get; set; } // Географічна довгота
    }
}
