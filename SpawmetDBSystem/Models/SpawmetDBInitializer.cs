using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SpawmetDBSystem.Models
{
    public class SpawmetDBInitializer : DropCreateDatabaseAlways<SpawmetDBContext>
    {
        protected override void Seed(SpawmetDBContext context)
        {
            var userGroups = new List<UserGroup>()
            {
                new UserGroup() { Name = "Admin" },
            };
            userGroups.ForEach(ug => context.UserGroups.Add(ug));
            context.SaveChanges();

            var adminUserGroup = userGroups.Single(ug => ug.Name == "Admin");
            var users = new List<User>()
            {
                new User() { Login = "Ninja", Password = Tools.GetMd5Hash("pwd"), UserGroup = adminUserGroup },
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}