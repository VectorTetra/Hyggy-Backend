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
        public ShopDTO? Shop { get; set; }
        public AddressDTO? Address { get; set; }
    }
}
