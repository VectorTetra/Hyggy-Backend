using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class WareCategory2
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string JSONStructureFilePath { get; set; }
        public virtual WareCategory1 WareCategory1 { get; set; }
        public virtual ICollection<WareCategory3> WaresCategory3 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (WareCategory2)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
