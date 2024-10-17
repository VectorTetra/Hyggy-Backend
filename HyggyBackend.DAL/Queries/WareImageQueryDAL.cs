namespace HyggyBackend.DAL.Queries
{
    public class WareImageQueryDAL
    {
        public long? Id { get; set; }
        public string? Path { get; set; }
        public long? WareId { get; set; }
        public long? WareArticle { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
    }
}
