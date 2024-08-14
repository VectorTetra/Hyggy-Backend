using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class ShopDTO
    {
        public long Id { get; set; }
        public string PhotoUrl { get; set; }
        public string? WorkHours { get; set; }
        public StorageDTO? Storage { get; set; }
    }
}
