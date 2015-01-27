using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpawmetDBSystem.Models;

namespace SpawmetDBSystem.Controllers
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
            else
            {
                return indexHandler();
            }
        }

        public abstract ActionResult Create();

        public virtual ActionResult Delete<T>(string deleteString, T item)
        {
            string[] deleteIds = deleteString.Split(',');
            throw new Exception();
        }

    }
}
