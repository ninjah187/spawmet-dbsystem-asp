using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SpawmetDBSystem.Models
{
    public class SpawmetDBContext : DbContext
    {
        public SpawmetDBContext()
            : base("name=SpawmetDBContext")
        {
        }

        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Part> Parts { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}