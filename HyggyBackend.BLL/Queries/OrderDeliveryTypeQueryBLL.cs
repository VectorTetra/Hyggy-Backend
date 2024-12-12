namespace HyggyBackend.BLL.Queries
{
    public class OrderDeliveryTypeQueryBLL
    {
        public long? Id { get; set; }
        public long? OrderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public int? MinDeliveryTimeInDays { get; set; }
        public int? MaxDeliveryTimeInDays { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? QueryAny { get; set; }
    }
}
