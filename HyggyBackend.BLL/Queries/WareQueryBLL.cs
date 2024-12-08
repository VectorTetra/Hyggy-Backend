using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class WareQueryBLL
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public long? Id { get; set; }
        public long? Article { get; set; }
        public long? Category1Id { get; set; }
        public long? Category2Id { get; set; }
        public long? Category3Id { get; set; }
        public string? NameSubstring { get; set; }
        public string? DescriptionSubstring { get; set; }
        public string? Category1NameSubstring { get; set; }
        public string? Category2NameSubstring { get; set; }
        public string? Category3NameSubstring { get; set; }
        public long? TrademarkId { get; set; }
        public string? TrademarkNameSubstring { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinDiscount { get; set; }
        public float? MaxDiscount { get; set; }
        public bool? IsDeliveryAvailable { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDescription { get; set; }
        public string? CustomerId { get; set; }
        public string? ImagePath { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? StringTrademarkIds { get; set; }
        public string? StringStatusIds { get; set; }
        public string? StringCategory1Ids { get; set; }
        public string? StringCategory2Ids { get; set; }
        public string? StringCategory3Ids { get; set; }
        public string? DTOType { get; set; } = "WareDTO";
        public string? QueryAny {get;set;}
    }
}
