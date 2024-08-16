using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyggyBackend.DAL.Entities.Employes;

namespace HyggyBackend.DAL.Entities
{
    public class Proffession
    {
        public long Id { get; set; }
        public string Name { get; set; } // Імя ролі співробітника

        public virtual ICollection<Employee>? Employes { get; set; } // Співробітники

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (Proffession)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
