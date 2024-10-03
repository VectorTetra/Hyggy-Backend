using HyggyBackend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyggyBackend.BLL.DTO
{
    public class WareReviewDTO
    {
        public long Id { get; set; }
        //public long WareId { get; set; }
        public long WareId { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public short Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
