using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


/*
 * Jordan Riley
 * 4-22-2018
 * HomeController class
 * Directs flow of home view route
 * 
 */


namespace Benchmark.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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