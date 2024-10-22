﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class WareTrademarkQueryBLL
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public long? WareId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Sorting { get; set; }
        public string? StringIds { get; set; }
        public string? QueryAny {get;set;}
    }
}