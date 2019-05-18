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
            PatientProfile profile = db.PatientsProfiles.Find(User.Identity.GetUserId());

            if (profile != null) /// are fisa completata
            {
                ViewBag.AccessPage = "Show";
            }
            else
            {
                ViewBag.AccessPage = "New";
            }
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
    }
}