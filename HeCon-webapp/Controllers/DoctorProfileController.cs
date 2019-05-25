using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    [Authorize(Roles = "Doctor,Administrator")]
    public class DoctorProfileController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        // GET: DoctorProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            DoctorProfile dr = new DoctorProfile();
            dr.UserId = User.Identity.GetUserId();
            return View(dr);

        }

        [HttpPost]
        public ActionResult New(DoctorProfile dr)
        {
            try
            {
                db.DoctorsProfiles.Add(dr);
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
            DoctorProfile a = db.DoctorsProfiles.Find(User.Identity.GetUserId());
            ViewBag.profile = a;

            return View(a);

        }

        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult Edit()
        {

            DoctorProfile a = db.DoctorsProfiles.Find(User.Identity.GetUserId());
            ViewBag.Article = a;

            
            return View(a);
            
        }


        [HttpPut]
        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult Edit( DoctorProfile requestDoctorProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DoctorProfile a = db.DoctorsProfiles.Find(User.Identity.GetUserId());

                    
                    if (TryUpdateModel(a))
                    {
                        a.DoctorName = requestDoctorProfile.DoctorName;
                        a.Description = requestDoctorProfile.Description;
                        db.SaveChanges();
                        TempData["message"] = "Articolul a fost modificat!";
                    }
                    return RedirectToAction("Show","DoctorProfile");
                  


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
        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult Delete()
        {

            DoctorProfile a = db.DoctorsProfiles.Find(User.Identity.GetUserId());

            db.DoctorsProfiles.Remove(a);
            db.SaveChanges();
            TempData["message"] = "Profilul a fost sters!";
            return RedirectToAction("New","DoctorProfile");

        }

    }
}