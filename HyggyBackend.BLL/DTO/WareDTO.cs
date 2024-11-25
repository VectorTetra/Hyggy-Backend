using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareDTO
    {
        public long Id { get; set; }
        public long? Article { get; set; }
        public long? WareCategory3Id { get; set; }
        public long? TrademarkId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StructureFilePath { get; set; }
        public float? Price { get; set; }
        public float? Discount { get; set; }
        public float? AverageRating { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public ICollection<long>? StatusIds { get; set; } = new List<long>();
        public ICollection<long>? ImageIds { get; set; } = new List<long>();
        public ICollection<long>? PriceHistoryIds { get; set; } = new List<long>();
        public ICollection<long>? WareItemIds { get; set; } = new List<long>();
        public ICollection<long>? OrderItemIds { get; set; } = new List<long>();
        public ICollection<long>? ReviewIds { get; set; } = new List<long>();
        public ICollection<string>? CustomerFavoriteIds { get; set; } = new List<string>();

        // Отримуєтьмя в результаті запиту
        public string? WareCategory3Name { get; set; }
        public long? WareCategory2Id { get; set; }
        public long? WareCategory1Id { get; set; }
        public string? PreviewImagePath { get; set; }
        public float? FinalPrice { get; set; }
        public string? TrademarkName { get; set; }
        public ICollection<string>? StatusNames { get; set; } = new List<string>();
        public ICollection<string>? ImagePaths { get; set; } = new List<string>();
        public ICollection<WareItemDTO>? WareItems { get; set; } = new List<WareItemDTO>();
    }
}
