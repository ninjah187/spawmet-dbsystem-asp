using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SpawmetDBSystem.Models;

namespace SpawmetDBSystem.Controllers
{
    public class UsersController : Controller
    {
        private SpawmetDBContext dbContext = new SpawmetDBContext();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(dbContext.Users);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginResult(string login, string password)
        {
            var user = dbContext.Users.Single(u => u.Login == login);
            if (user != null)
            {
                if (Tools.VerifyMd5Hash(password, user.Password) == true)
                {
                    Session["User"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Create()
        {
            var userGroups = dbContext.UserGroups.ToList();
            return View(userGroups);
        }

        [HttpPost]
        public ActionResult Create(string login, string password, string groupName)
        {
            /*var user = dbContext.Users.Single(u => u.Login == login);
            if (user == null)
            {
                var group = dbContext.UserGroups.Single(ug => ug.Name == groupName);
                user = new User()
                {
                    Login = login,
                    Password = Tools.GetMd5Hash(password),
                    UserGroup = group,
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                throw new UserAlreadyExistsException();
            }*/
            User user;
            try
            {
                user = dbContext.Users.Single(u => u.Login == login);
            }
            catch (InvalidOperationException exception) //occurs when there's no such user in database
            {
                var group = dbContext.UserGroups.Single(ug => ug.Name == groupName);
                user = new User()
                {
                    Login = login,
                    Password = Tools.GetMd5Hash(password),
                    UserGroup = group,
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            //if there's user with such login
            throw new UserAlreadyExistsException();
        }

        public ActionResult Delete(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            var deleteUsers = new List<User>();
            for (int i = 0; i < deleteIds.Length; i++)
            {
                //int id = int.Parse(deleteIds[i]);
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteUser = dbContext.Users.Single(u => u.ID == id);
                    deleteUsers.Add(deleteUser);
                }
                else
                {
                    //throw new InvalidOperationException("Błąd w parsowaniu.");
                }
            }
            return View(deleteUsers);
        }

        public ActionResult DeleteResult(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            for (int i = 0; i < deleteIds.Length; i++)
            {
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteUser = dbContext.Users.Single(u => u.ID == id);
                    dbContext.Users.Remove(deleteUser);
                }
                else
                {
                    //throw new InvalidOperationException("Błąd w parsowaniu.");
                }
            }
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var user = dbContext.Users.Single(u => u.ID == id);
            return View(user);
        }

        public ActionResult EditResult(int id, string login, string password, string groupName)
        {
            //TODO: check uniqueness of login
            var user = dbContext.Users.Single(u => u.ID == id);
            user.Login = login;
            user.Password = Tools.GetMd5Hash(password);
            user.UserGroup = dbContext.UserGroups.Single(ug => ug.Name == groupName);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException()
            : base("Taki użytkownik już istnieje.")
        {
        }
    }
}
