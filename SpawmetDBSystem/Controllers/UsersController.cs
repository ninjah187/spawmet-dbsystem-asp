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
    }
}
