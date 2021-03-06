﻿using HeCon_webapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    public class XRayController : Controller
    {
        private ApplicationDbContext db;
        private PythonCaller pythonCaller;
        private IPathProvider pathProvider;
        public XRayController()
        {
            db = ApplicationDbContext.Create();
            pythonCaller = PythonCaller.getPythonCaller();
            pathProvider = new ServerPathProvider();
        }

        public XRayController(ApplicationDbContext applicationDb,IPathProvider ipathProvider)
        {
            db = applicationDb;
            pythonCaller = PythonCaller.getPythonCaller();
            pathProvider = ipathProvider;
        }


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
            fileName = Path.Combine(pathProvider.MapPath("~/UploadedImages/"), fileName);
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

        [Authorize(Roles = "User,Doctor,Administrator")]
        public ActionResult Show()
        {
            var userId = User.Identity.GetUserId();
            var xrays = db.XRays.Where(u => u.UserId == userId).Select(gr => gr);
            var UserName = db.Users.Where(u => u.Id == userId).Select(gr => gr.UserName).FirstOrDefault();

            //luam comentarile ptr stirea resp

            var comments1 = from n in db.Comment select n;
            List<Comment> comments = comments1.ToList();
            ViewBag.Comments = comments;

            ViewBag.XraysList = xrays;
            ViewBag.UserIdCurrent = userId;
            ViewBag.UserName = UserName;
            return View();
        }

        [Authorize(Roles = "Doctor,Administrator")]
        public ActionResult ShowPatientXray(string id)
        {
            var xrays = db.XRays.Where(u => u.UserId == id && u.PermissionToDoctor == 1).Select(gr => gr);
            var UserName = db.Users.Where(u => u.Id == id).Select(gr => gr.UserName).FirstOrDefault();
            ViewBag.XraysList = xrays;
            ViewBag.UserIdCurrent = User.Identity.GetUserId();
            ViewBag.UserName = UserName;

            var comments1 = from n in db.Comment select n;
            List<Comment> comments = comments1.ToList();
            ViewBag.Comments = comments;
            return View("../XRay/Show");
        }

        [Authorize(Roles = "User,Doctor,Administrator")]
        public ActionResult ModifyPermission(int id) {
            XRay xray = db.XRays.Find(id);
            xray.PermissionToDoctor = 1-xray.PermissionToDoctor;
            db.SaveChanges();
            return RedirectToAction("Show","XRay");
        }
    }
}