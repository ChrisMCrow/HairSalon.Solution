using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpGet("/stylist")]
        public ActionResult RedirectHome()
        {
            return RedirectToAction("Index", "Home");
        }

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

        [HttpGet("/stylist/{stylistId}/delete")]
        public ActionResult DeleteStylist(int stylistId)
        {
            Stylist foundStylist = Stylist.Find(stylistId);
            foundStylist.Delete();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/stylist/{stylistId}/update")]
        public ActionResult UpdateStylistForm(int stylistId)
        {
            Stylist foundStylist = Stylist.Find(stylistId);
            return View("Update", foundStylist);
        }

        [HttpPost("/stylist/{stylistId}/update")]
        public ActionResult UpdateStylist(int stylistId, string newName)
        {
            Stylist foundStylist = Stylist.Find(stylistId);
            foundStylist.Update(newName);
            return RedirectToAction("UpdateStylistForm", new {stylistId = foundStylist.Id});
        }

        [HttpPost("/stylist/{stylistId}/update/specialty")]
        public ActionResult UpdateSpecialty(int stylistId, string specialty)
        {
            Stylist.AddSpecialty(stylistId, int.Parse(specialty));
            return RedirectToAction("UpdateStylistForm", new {stylistId = stylistId});
        }

        [HttpGet("/stylist/{stylistId}/remove/{specialtyId}")]
        public ActionResult DeleteSpecialty(int stylistId, int specialtyId)
        {
            Stylist.RemoveSpecialty(stylistId, specialtyId);
            return RedirectToAction("UpdateStylistForm", new {stylistId = stylistId});
        }
    }
}
