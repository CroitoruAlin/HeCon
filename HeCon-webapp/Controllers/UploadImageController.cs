using HeCon_webapp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    public class UploadImageController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        private PythonCaller pythonCaller = PythonCaller.getPythonCaller();

        // GET: Image
        [HttpGet]
        [AllowAnonymous]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult New(UploadImage imageModel)
        {

            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yy_mm_ss_fff") + extension;
            imageModel.ImagePath = "~/UploadedImages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/UploadedImages/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);
            db.UploadImages.Add(imageModel);
            db.SaveChanges();
            string[] result = pythonCaller.callPython(fileName);
            ModelState.Clear();
            ViewBag.result = result;
            return View();
        }
    }
}