using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareCategory2DTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string JSONStructureFilePath { get; set; }
        public WareCategory1DTO WareCategory1 { get; set; }
        public ICollection<WareCategory3DTO> WaresCategory3 { get; set; }
    }
}
