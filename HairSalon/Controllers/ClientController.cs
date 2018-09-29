using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/client")]
        public ActionResult Index()
        {
          return View(Client.GetAll());
        }

        [HttpPost("/client/new")]
        public ActionResult CreateClient(string clientName, string stylistId)
        {
            new Client(clientName, int.Parse(stylistId)).Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/client/{clientId}/delete")]
        public ActionResult DeleteClient(int clientId)
        {
            Client foundClient = Client.Find(clientId);
            foundClient.Delete();
            return RedirectToAction("Index");
        }

        [HttpGet("/client/{clientId}/update")]
        public ActionResult UpdateClientForm(int clientId)
        {
            Client foundClient = Client.Find(clientId);
            return View("Update", foundClient);
        }

        [HttpPost("/client/{clientId}/update")]
        public ActionResult UpdateClient(int clientId, string newName, string stylistId)
        {
            Client foundClient = Client.Find(clientId);
            foundClient.Update(newName, int.Parse(stylistId));
            return RedirectToAction("UpdateClientForm", new {clientId = foundClient.Id});
        }

        // [HttpPost("/client/{clientId}/update/stylist")]
        // public ActionResult UpdateStylist(int clientId, string stylist)
        // {
        //     Stylist.AddClient(int.Parse(stylist), clientId);
        //     return RedirectToAction("UpdateClientForm", new {clientId = clientId});
        // }
        //
        // [HttpGet("/client/{clientId}/remove/{stylistId}")]
        // public ActionResult DeleteClient(int clientId, int stylistId)
        // {
        //     Stylist.RemoveClient(stylistId, clientId);
        //     return RedirectToAction("UpdateClientForm", new {clientId = clientId});
        // }
    }
}
