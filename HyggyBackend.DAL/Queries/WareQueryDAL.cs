﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareQueryDAL
    {
        public long? Category1Id { get; set; }
        public long? Category2Id { get; set; }
        public long? Category3Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
        public string? Category1NameSubstring { get; set; }
        public string? Category2NameSubstring { get; set; }
        public string? Category3NameSubstring { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinDiscount { get; set; }
        public float? MaxDiscount { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public string? ImagePath { get; set; }
    }
}
