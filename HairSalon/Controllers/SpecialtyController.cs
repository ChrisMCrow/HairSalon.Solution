using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/specialty")]
        public ActionResult Index()
        {
          return View(Specialty.GetAll());
        }

        [HttpPost("/specialty/new")]
        public ActionResult CreateSpecialty(string description)
        {
            new Specialty(description).Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/specialty/{specialtyId}/delete")]
        public ActionResult DeleteSpecialty(int specialtyId)
        {
            Specialty foundSpecialty = Specialty.Find(specialtyId);
            foundSpecialty.Delete();
            return RedirectToAction("Index");
        }

        [HttpGet("/specialty/{specialtyId}/update")]
        public ActionResult UpdateSpecialtyForm(int specialtyId)
        {
            Specialty foundSpecialty = Specialty.Find(specialtyId);
            return View("Update", foundSpecialty);
        }

        [HttpPost("/specialty/{specialtyId}/update")]
        public ActionResult UpdateSpecialty(int specialtyId, string newName)
        {
            Specialty foundSpecialty = Specialty.Find(specialtyId);
            foundSpecialty.Update(newName);
            return RedirectToAction("UpdateSpecialtyForm", new {specialtyId = foundSpecialty.Id});
        }

        [HttpPost("/specialty/{specialtyId}/update/stylist")]
        public ActionResult UpdateStylist(int specialtyId, string stylist)
        {
            Stylist.AddSpecialty(int.Parse(stylist), specialtyId);
            return RedirectToAction("UpdateSpecialtyForm", new {specialtyId = specialtyId});
        }

        [HttpGet("/specialty/{specialtyId}/remove/{stylistId}")]
        public ActionResult DeleteSpecialty(int specialtyId, int stylistId)
        {
            Stylist.RemoveSpecialty(stylistId, specialtyId);
            return RedirectToAction("UpdateSpecialtyForm", new {specialtyId = specialtyId});
        }
    }
}
