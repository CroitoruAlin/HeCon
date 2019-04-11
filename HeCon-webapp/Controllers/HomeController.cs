using HeCon_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeCon_webapp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            PythonCaller pythonCaller = PythonCaller.getPythonCaller();
            string result = pythonCaller.callPython("../HeCon-ml/image.jpeg");
            ViewBag.result = result;
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