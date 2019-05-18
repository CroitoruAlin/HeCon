using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    //[Authorize(Roles = "User")] 
    public class PatientProfileController : Controller
    {

        private ApplicationDbContext db = ApplicationDbContext.Create();

        public ActionResult New()
        {
            PatientProfile patient = new PatientProfile();
            patient.UserId = User.Identity.GetUserId();
            return View(patient);

        }

        [HttpPost]
        public ActionResult New(PatientProfile patient)
        {
            try
            {
                db.PatientsProfiles.Add(patient);
                db.SaveChanges();
                TempData["message"] = "Profilul a fost adaugat!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Show()
        {
            PatientProfile profile = db.PatientsProfiles.Find(User.Identity.GetUserId());
            ViewBag.profile = profile;

            // ViewBag.esteAdmin = User.IsInRole("Administrator");
            // ViewBag.utilizatorCurent = User.Identity.GetUserId();
            return View(profile);

        }
    }
}
