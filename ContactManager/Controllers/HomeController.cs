using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactManager.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // ViewBag.Message = "A simple contact manager! Share your contact info.";
            ViewBag.Message = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please feel free to contact me with any questions.";

            return View();
        }
    }
}