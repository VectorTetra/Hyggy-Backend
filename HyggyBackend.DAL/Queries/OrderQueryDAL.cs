﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class OrderQueryDAL
    {
        public long? DeliveryAddressId { get; set; }
        public string? Street { get; set; } // Вулиця
        public string? HomeNumber { get; set; } // Номер будинку
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? MinOrderDate { get; set; }
        public DateTime? MaxOrderDate { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public long? OrderItemId { get; set; }
        public long? WareId { get; set; }
        public long? WarePriceHistoryId { get; set; }
        public long? CustomerId { get; set; }
        public long? ShopId { get; set; }
    }
}