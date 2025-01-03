﻿namespace HyggyBackend.DAL.Queries
{
    public class BlogCategory1QueryDAL
    {
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogId { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogCategory2Id { get; set; }
        public string? BlogCategory2Name { get; set; }
        public string? StringIds { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny {get;set;}
    }
}
