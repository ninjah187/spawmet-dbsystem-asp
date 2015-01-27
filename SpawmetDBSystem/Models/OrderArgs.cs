using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpawmetDBSystem.Models
{
    public class OrderArgs
    {
        public List<Client> Clients { get; set; }
        public List<Machine> Machines { get; set; }
    }
}