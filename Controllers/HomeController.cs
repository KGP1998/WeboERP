using DairyWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
   
    public class HomeController : Controller
    {
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize("User")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}