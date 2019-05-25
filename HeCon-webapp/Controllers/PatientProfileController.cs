using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    [Authorize(Roles = "User,Doctor,Administrator")] 
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

            if (profile == null)
            {
                return RedirectToAction("New", "PatientProfile");
            }

            return View(profile);

        }


        [Authorize(Roles = "User,Administrator")]
        public ActionResult Edit()
        {

            PatientProfile a = db.PatientsProfiles.Find(User.Identity.GetUserId());
            ViewBag.Article = a;


            return View(a);

        }


        [HttpPut]
        [Authorize(Roles = "User,Administrator")]
        public ActionResult Edit(PatientProfile requestPatientProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PatientProfile a = db.PatientsProfiles.Find(User.Identity.GetUserId());


                    if (TryUpdateModel(a))
                    {
                        a.PatientName = requestPatientProfile.PatientName;
                        a.Age = requestPatientProfile.Age;
                        a.HealthIssues = requestPatientProfile.HealthIssues;
                        a.SurgicalProcedures = requestPatientProfile.SurgicalProcedures;
                        a.FamilyDoctor = requestPatientProfile.FamilyDoctor;

                        db.SaveChanges();
                        TempData["message"] = "Articolul a fost modificat!";
                    }
                    return RedirectToAction("Show", "PatientProfile");



                }
                else
                {
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "User,Administrator")]
        public ActionResult Delete()
        {

            PatientProfile a = db.PatientsProfiles.Find(User.Identity.GetUserId());

            db.PatientsProfiles.Remove(a);
            db.SaveChanges();
            TempData["message"] = "Profilul a fost sters!";
            return RedirectToAction("New", "PatientProfile");

        }
    }
}
