
namespace HyggyBackend.DAL.Queries
{
	public class ShopQueryDAL
	{
        public long? Id { get; set; }
        //Адреса розташування магазину
        public long? AddressId;
		public string? Street { get; set; } 
		public string? Name { get; set; } 
		public string? HouseNumber { get; set; } 
		public string? City { get; set; } 
		public string? State { get; set; } 
		public string? PostalCode { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public long? StorageId { get; set; }
        public long? OrderId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? NearestCount { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
