using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.DAL.Entities
{
    public class WareReview
    {
        public long Id { get; set; }
        //public long WareId { get; set; }
        public virtual Ware Ware { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
        public string CustomerName { get; set; }
        public string? AuthorizedCustomerId { get; set; }
        public string Email { get; set; }
        public short Rating { get; set; }
        public DateTime Date { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (WareReview)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
