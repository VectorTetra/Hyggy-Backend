using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class StorageDTO
    {
        public long Id { get; set; }
        public long? ShopId { get; set; }
        public long? AddressId { get; set; }
    }
}
