
namespace HyggyBackend.DAL.Queries
{
	public class ShopQueryDAL
	{
		//Адреса розташування магазину
		public long? AddressId;
		public string? Street { get; set; } 
		public string? HouseNumber { get; set; } 
		public string? City { get; set; } 
		public string? State { get; set; } 
		public string? PostalCode { get; set; }

		public double? Latitude { get; set; }
		public double? Longitude { get; set; }

		public long? StorageId { get; set; }
        public long? OrderId { get; set; }
    }
}
