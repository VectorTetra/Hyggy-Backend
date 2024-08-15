using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareCategory3DTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string JSONStructureFilePath { get; set; }
        public WareCategory2DTO WareCategory2 { get; set; }
        //public ICollection<WareDTO> Wares { get; set; }
    }
}
