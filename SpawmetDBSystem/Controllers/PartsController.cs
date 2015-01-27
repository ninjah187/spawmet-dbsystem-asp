using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpawmetDBSystem.Models;

namespace SpawmetDBSystem.Controllers
{
    public class PartsController : Controller
    {
        private SpawmetDBContext dbContext = new SpawmetDBContext();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View(dbContext.Parts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string amount)
        {
            Part part;
            try
            {
                part = dbContext.Parts.Single(p => p.Name == name);
            }
            catch (InvalidOperationException exception)
            {
                int _amount;
                if (int.TryParse(amount, out _amount) == true)
                {
                    part = new Part()
                    {
                        Name = name,
                        Amount = _amount,
                    };
                    dbContext.Parts.Add(part);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    
                }
                
            }
            throw new InvalidOperationException("There is element with such name in database.");
        }

        public ActionResult Delete(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            var deleteParts = new List<Part>();
            for (int i = 0; i < deleteIds.Length; i++)
            {
                //int id = int.Parse(deleteIds[i]);
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deletePart = dbContext.Parts.Single(p => p.ID == id);
                    deleteParts.Add(deletePart);
                }
                else
                {
                    
                }
            }
            return View(deleteParts);
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
    }
}
