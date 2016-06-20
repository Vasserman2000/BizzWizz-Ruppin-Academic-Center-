using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizWizProj.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "ClosedShifts");
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}