using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class WareTrademark
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ware> Wares { get; set; } = new List<Ware>();
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (WareTrademark)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
