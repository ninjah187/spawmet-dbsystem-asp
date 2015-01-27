using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpawmetDBSystem.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? SendDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Machine Machine { get; set; }
    }
}