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
        }
    }
}