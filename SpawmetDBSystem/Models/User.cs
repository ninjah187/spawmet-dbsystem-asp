using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpawmetDBSystem.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}