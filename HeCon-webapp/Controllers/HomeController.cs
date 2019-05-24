using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ShowUsers()
        {

            return RedirectToAction("Index", "Users");
        }

        [Authorize(Roles = "User,Doctor,Administrator")]
        public ActionResult ShowXRays()
        {
            return RedirectToAction("Show", "XRay");
        }


        [Authorize(Roles = "User,Doctor,Administrator")]
        public ActionResult ShowMyProfile()
        {
            
            ViewBag.IsDoctor = User.IsInRole("Doctor");
            ViewBag.IsPatient = User.IsInRole("User");

           
            
            if (ViewBag.IsDoctor == true)
            {
                DoctorProfile profile = db.DoctorsProfiles.Find(User.Identity.GetUserId());
                if (profile != null)  /// are fisa completata
                    return RedirectToAction("Show", "DoctorProfile");
                else
                    return RedirectToAction("New", "DoctorProfile");
            }
            else
            if (ViewBag.IsPatient == true)
            {
                PatientProfile profile = db.PatientsProfiles.Find(User.Identity.GetUserId());
                if (profile != null)
                    return RedirectToAction("Show", "PatientProfile");
                else
                    return RedirectToAction("New", "PatientProfile");
            }

            else return RedirectToAction("Index", "Home");

        }

    }
}