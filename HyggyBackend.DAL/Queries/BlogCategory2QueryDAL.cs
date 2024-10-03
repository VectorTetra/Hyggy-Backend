﻿namespace HyggyBackend.DAL.Queries
{
    public class BlogCategory2QueryDAL
    {
        public long? Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? Keyword { get; set; }
        public string? FilePath { get; set; }
        public string? PreviewImagePath { get; set; }
        public long? BlogCategory1Id { get; set; }
        public string? BlogCategory1Name { get; set; }
        public long? BlogId { get; set; }
        public string? BlogCategory2Name { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
