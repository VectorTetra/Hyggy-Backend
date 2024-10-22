using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Queries
{
    public class WareItemQueryDAL
    {
        public long? Id { get; set; }

        public long? Article { get; set; }
        public long? WareId { get; set; }
        public string? WareName { get; set; }
        public string? WareDescription { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinDiscount { get; set; }
        public float? MaxDiscount { get; set; }
        public long? StatusId { get; set; }
        public long? WareCategory3Id { get; set; }
        public long? WareCategory2Id { get; set; }
        public long? WareCategory1Id { get; set; }
        public long? WareImageId { get; set; }
        public long? PriceHistoryId { get; set; }
        public long? OrderItemId { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StorageId { get; set; }
        public long? ShopId { get; set; }

        public long? MinQuantity { get; set; }
        public long? MaxQuantity { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny {get;set;}
    }
}
