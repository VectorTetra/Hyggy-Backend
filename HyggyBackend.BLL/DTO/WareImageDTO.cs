using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareImageDTO
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public Ware Ware { get; set; }
    }
}
