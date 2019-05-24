using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class XRayController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        private PythonCaller pythonCaller = PythonCaller.getPythonCaller();

        // GET: Image
        [HttpGet]
        //[AllowAnonymous]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        public ActionResult New(XRay imageModel)
        {

            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yy_mm_ss_fff") + extension;
            imageModel.ImagePath = "~/UploadedImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/UploadedImages/"), fileName);
            imageModel.UserId = User.Identity.GetUserId();
            imageModel.ImageFile.SaveAs(fileName);      
            Prediction result = pythonCaller.callPython(fileName);
            ModelState.Clear();
            imageModel.Prediction = result;
            imageModel.PermissionToDoctor = 0;
            ViewBag.result = result;
            db.XRays.Add(imageModel);
            db.SaveChanges();
            return View();
        }

        public ActionResult Show()
        {
            var userId = User.Identity.GetUserId();
            var xrays = db.XRays.Where(u => u.UserId == userId).Select(gr => gr);

            ViewBag.XraysList = xrays;
            return View();
        }
    }
}