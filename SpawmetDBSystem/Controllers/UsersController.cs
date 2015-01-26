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
            return View(dbContext.Users);
        }

    }
}
