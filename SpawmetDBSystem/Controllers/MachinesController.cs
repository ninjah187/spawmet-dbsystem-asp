using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpawmetDBSystem.Models;

namespace SpawmetDBSystem.Controllers
{
    public class MachinesController : Controller
    {
        private SpawmetDBContext dbContext = new SpawmetDBContext();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View(dbContext.Machines);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string price)
        {
            Machine machine;
            try
            {
                machine = dbContext.Machines.Single(p => p.Name == name);
            }
            catch (InvalidOperationException exception)
            {
                /*int _amount;
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

                }*/
                machine = new Machine()
                {
                    Name = name,
                    Price = price,
                };
                dbContext.Machines.Add(machine);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            throw new InvalidOperationException("There is element with such name in database.");
        }

        public ActionResult Delete(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            var deleteMachines = new List<Machine>();
            for (int i = 0; i < deleteIds.Length; i++)
            {
                //int id = int.Parse(deleteIds[i]);
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteMachine = dbContext.Machines.Single(p => p.ID == id);
                    deleteMachines.Add(deleteMachine);
                }
                else
                {

                }
            }
            return View(deleteMachines);
        }

        public ActionResult DeleteResult(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            for (int i = 0; i < deleteIds.Length; i++)
            {
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteMachine = dbContext.Machines.Single(p => p.ID == id);
                    dbContext.Machines.Remove(deleteMachine);
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
            var machine = dbContext.Machines.Single(p => p.ID == id);
            return View(machine);
        }

        public ActionResult EditResult(int id, string name, string price)
        {
            //TODO: check uniqueness of login
            var machine = dbContext.Machines.Single(p => p.ID == id);
            machine.Name = name;
            machine.Price = price;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
