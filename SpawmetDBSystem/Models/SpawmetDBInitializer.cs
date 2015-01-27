using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.EnterpriseServices;

namespace SpawmetDBSystem.Models
{
    public class SpawmetDBInitializer : DropCreateDatabaseAlways<SpawmetDBContext>
    {
        private static readonly Random random = new Random();

        protected override void Seed(SpawmetDBContext context)
        {
            var userGroups = new List<UserGroup>()
            {
                new UserGroup() { Name = "Admin" },
                new UserGroup() { Name = "Boss", },
                new UserGroup() { Name = "Worker" },
            };
            userGroups.ForEach(ug => context.UserGroups.Add(ug));
            context.SaveChanges();

            var adminUserGroup = userGroups.Single(ug => ug.Name == "Admin");
            var bossUserGroup = userGroups.Single(ug => ug.Name == "Boss");
            var workerUserGroup = userGroups.Single(ug => ug.Name == "Worker");
            var users = new List<User>()
            {
                new User() { Login = "Ninja", Password = Tools.GetMd5Hash("pwd"), UserGroup = adminUserGroup },
            };
            for (int i = 0; i < 5; i++)
            {
                users.Add(new User()
                {
                    Login = "Boss " + (i + 1).ToString(),
                    Password = Tools.GetMd5Hash("pwd" + (i + 1).ToString()),
                    UserGroup = bossUserGroup,
                });
            }
            for (int i = 0; i < 50; i++)
            {
                users.Add(new User()
                {
                    Login = "Worker " + (i + 1).ToString(),
                    Password = Tools.GetMd5Hash("pwd" + (i + 1).ToString()),
                    UserGroup =  workerUserGroup,
                });
            }
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var parts = new List<Part>();
            for (int i = 0; i < 55; i++)
            {
                parts.Add(new Part()
                {
                    Name = "part" + (i + 1).ToString(),
                    Amount = random.Next(1001),
                });
            }
            parts.ForEach(p => context.Parts.Add(p));
            context.SaveChanges();

            var clients = new List<Client>()
            {
                new Client()
                {
                    Address = "Bojowników o wolność i demokracje 15",
                    City = "Sępólno",
                    Email = "wojcik.ernest@o2.pl",
                    Name = "Agro pomorze",
                    Nip = "",
                    Phone = "500216719",
                    PostalCode = "26-330 Żarnów",
                    Province = "Wielkopolskie",
                },
                new Client()
                {
                    Address = "Targowa 9",
                    City = "Gorzów Wlkp.",
                    Email = "m.placzek@euro-broker.pl",
                    Name = "EURO-BROKER",
                    Nip = "",
                    Phone = "784045855, 734128072",
                    PostalCode = "66-400",
                    Province = "Lubuskie",
                },
            };
            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var machines = new List<Machine>();
            for (int i = 0; i < 50; i++)
            {
                machines.Add(new Machine()
                {
                    Name = "Machine " + (i + 1).ToString(),
                    Price = random.Next(100000).ToString(),
                });
            }
            machines.ForEach(m => context.Machines.Add(m));
            context.SaveChanges();

            var orders = new List<Order>()
            {
                new Order()
                {
                    Client = context.Clients.Find(1),
                    Machine = context.Machines.Find(20),
                    Remarks = "Dodatkowe uwagi.",
                    SendDate = new DateTime(2015, 1, 1),
                    StartDate = DateTime.Now,
                    Status = "Nowe",
                },
            };
            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();
        }
    }
}