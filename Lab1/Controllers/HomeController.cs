using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.mess = "Hello ASP";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}