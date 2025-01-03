﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class WareCategory3QueryBLL
    {
        public long? Id { get; set; }
        public string? NameSubstring { get; set; }
        public long? WareCategory1Id { get; set; }
        public string? WareCategory1NameSubstring { get; set; }
        public long? WareCategory2Id { get; set; }
        public string? WareCategory2NameSubstring { get; set; }
        public long? WareId { get; set; }
        public string? WareNameSubstring { get; set; }
        public long? WareArticle { get; set; }
        public string? WareDescriptionSubstring { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public string? QueryAny {get;set;}
    }
}
