using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpawmetDBSystem.Models
{
    public abstract class MainController : Controller
    {
        protected delegate ActionResult IndexHandler();

        private SpawmetDBContext dbContext = new SpawmetDBContext();

        protected virtual ActionResult Index(IndexHandler indexHandler)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return indexHandler();
        }
    }
}
