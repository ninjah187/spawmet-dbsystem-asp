using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpawmetDBSystem.Models;

namespace SpawmetDBSystem.Controllers
{
    public class ClientsController : Controller
    {
        private SpawmetDBContext dbContext = new SpawmetDBContext();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View(dbContext.Clients);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string city, string phone, string email, string nip,
                                    string province, string address, string postalCode)
        {
            Client client;
            try
            {
                client = dbContext.Clients.Single(c => c.Name == name);
            }
            catch (InvalidOperationException exception)
            {
                client = new Client()
                {
                    Address = address,
                    City = city,
                    Email = email,
                    Name = name,
                    Nip = nip,
                    Phone = phone,
                    PostalCode = postalCode,
                    Province = province,
                };

                dbContext.Clients.Add(client);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            throw new InvalidOperationException("There is element with such name in database.");
        }

        public ActionResult Delete(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            var deleteClients = new List<Client>();
            for (int i = 0; i < deleteIds.Length; i++)
            {
                //int id = int.Parse(deleteIds[i]);
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteClient = dbContext.Clients.Single(c => c.ID == id);
                    deleteClients.Add(deleteClient);
                }
                else
                {

                }
            }
            return View(deleteClients);
        }

        public ActionResult DeleteResult(string deleteString)
        {
            string[] deleteIds = deleteString.Split(',');
            for (int i = 0; i < deleteIds.Length; i++)
            {
                int id;
                if (int.TryParse(deleteIds[i], out id))
                {
                    var deleteClient = dbContext.Clients.Single(p => p.ID == id);
                    dbContext.Clients.Remove(deleteClient);
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
            var client = dbContext.Clients.Single(p => p.ID == id);
            return View(client);
        }

        public ActionResult EditResult(int id, string name, string city, string phone, string email, string nip,
                                    string province, string address, string postalCode)
        {
            //TODO: check uniqueness of login
            var client = dbContext.Clients.Single(p => p.ID == id);
            client.Name = name;
            client.City = city;
            client.Phone = phone;
            client.Email = email;
            client.Nip = nip;
            client.Province = province;
            client.Address = address;
            client.PostalCode = postalCode;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
