using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpPost("/stylist/new")]
        public ActionResult CreateStylist(string stylistName)
        {
            new Stylist(stylistName).Save();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/stylist/{stylistId}")]
        public ActionResult Details(int stylistId)
        {
            return View(Stylist.Find(stylistId));
        }

        [HttpPost("/stylist/{stylistId}/newClient")]
        public ActionResult CreateClient(string clientName, int stylistId)
        {
            new Client(clientName, stylistId).Save();
            return View("Details", Stylist.Find(stylistId));
        }

        [HttpPost("/stylist/{stylistId}/delete")]
        public ActionResult Delete(int stylistId)
        {
            Stylist.Find(stylistId).Delete();
            return RedirectToAction("Index", "Home");
        }
    }
}
