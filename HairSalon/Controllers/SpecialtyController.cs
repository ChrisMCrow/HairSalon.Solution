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
        public ActionResult CreateSpecialty(string specialtyName)
        {
            new Specialty(specialtyName).Save();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/specialty/{specialtyId}")]
        public ActionResult Details(int specialtyId)
        {
            return View(Specialty.Find(specialtyId));
        }

        [HttpPost("/specialty/{specialtyId}/newClient")]
        public ActionResult CreateClient(string clientName, int specialtyId)
        {
            new Client(clientName, specialtyId).Save();
            return View("Details", Specialty.Find(specialtyId));
        }

        [HttpGet("/specialty/{specialtyId}/delete")]
        public ActionResult DeleteSpecialty(int specialtyId)
        {
            Specialty foundSpecialty = Specialty.Find(specialtyId);
            foundSpecialty.Delete();
            return RedirectToAction("Index", "Home");
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

        [HttpPost("/specialty/{specialtyId}/update/specialty")]
        public ActionResult UpdateSpecialty(int specialtyId, string specialty)
        {
            Specialty.AddSpecialty(specialtyId, int.Parse(specialty));
            return RedirectToAction("UpdateSpecialtyForm", new {specialtyId = specialtyId});
        }

        [HttpGet("/specialty/{specialtyId}/remove/{stylistId}")]
        public ActionResult DeleteSpecialty(int specialtyId, int specialtyId)
        {
            Specialty.RemoveSpecialty(specialtyId, specialtyId);
            return RedirectToAction("UpdateSpecialtyForm", new {specialtyId = specialtyId});
        }
    }
}
