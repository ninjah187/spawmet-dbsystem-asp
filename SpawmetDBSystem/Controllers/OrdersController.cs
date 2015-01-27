using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpawmetDBSystem.Models;

namespace SpawmetDBSystem.Controllers
{
    public class OrdersController : Controller
    {
        private SpawmetDBContext dbContext = new SpawmetDBContext();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View(dbContext.Orders);
        }

        public ActionResult Create()
        {
            return View(new OrderArgs()
            {
                Clients = dbContext.Clients.ToList(),
                Machines = dbContext.Machines.ToList(),
            });
        }

        [HttpPost]
        public ActionResult Create(string clientName, string machineName, string status,
                                    string startDate, string sendDate, string remarks)
        {
            var order = new Order()
            {
                Client = dbContext.Clients.Single(c => c.Name == clientName),
                Machine = dbContext.Machines.Single(m => m.Name == machineName),
                Remarks = remarks,
                SendDate = sendDate.ToDateTime(),
                StartDate = startDate.ToDateTime(),
                Status = status,
            };
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string deleteString)
        {
            if (((User) Session["User"]).UserGroup.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            string[] deleteIds = deleteString.Split(',');
            var deleteOrders = new List<Order>();
            for (int i = 0; i < deleteIds.Length; i++)
            {
                //int id = int.Parse(deleteIds[i]);
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteOrder = dbContext.Orders.Single(o => o.ID == id);
                    deleteOrders.Add(deleteOrder);
                }
                else
                {
                    //throw new InvalidOperationException("Błąd w parsowaniu.");
                }
            }
            return View(deleteOrders);
        }
    }
}
