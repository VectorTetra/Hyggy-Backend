﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.Queries
{
    public class StorageQueryBLL
    {
        public long? AddressId { get; set; }
        public long? Id { get; set; }
        public long? ShopId { get; set; }
        public long? WareItemId { get; set; }
        public string? StorageEmployeeId { get; set; }
        public string? ShopEmployeeId { get; set; }
        public bool? IsGlobal { get; set; }
        public string? StringIds { get; set; }
        public string? Sorting { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? QueryAny {get;set;}
    }
}
