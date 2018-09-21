using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpPost("/stylist/new")]
        public ActionResult Create(string stylistName)
        {
            Stylist newStylist = new Stylist(stylistName);
            newStylist.Save();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/stylist/{stylistId}")]
        public ActionResult Details(int stylistId)
        {
            return View(Stylist.Find(stylistId));
        }
    }
}
