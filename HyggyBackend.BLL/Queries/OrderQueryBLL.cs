using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class OrderQueryBLL
    {
        //public string? DeliveryAddress { get; set; }

        public long? Id { get; set; }
        public long? AddressId { get; set; }

        // Адреса доставки, розділена на компоненти
        public string? Street { get; set; } // Назва вулиці
        public string? HouseNumber { get; set; } // Номер будинку
        public string? City { get; set; } // Місто
        public string? State { get; set; } // Область або штат
        public string? PostalCode { get; set; } // Поштовий індекс

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Дані про клієнта

        public DateTime? MinOrderDate { get; set; }
        public DateTime? MaxOrderDate { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public long? DeliveryTypeId { get; set; }
        public string? DeliveryTypeName { get; set; }
        public string? DeliveryTypeDescription { get; set; }
        public float? MinDeliveryTypePrice { get; set; }
        public float? MaxDeliveryTypePrice { get; set; }
        public int? MinDeliveryTimeInDays { get; set; }
        public int? MaxDeliveryTimeInDays { get; set; }
        public long? OrderItemId { get; set; }
        public long? WareId { get; set; }
        public long? WarePriceHistoryId { get; set; }
        public string CustomerId { get; set; }
        public long? ShopId { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? QueryAny { get; set; }
    }
}
