using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{

    public class Address
    {
        public long Id { get; set; }
        public string Street { get; set; } // Вулиця
        public string HomeNumber { get; set; } // Номер будинку
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        // Географічні координати
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

}
