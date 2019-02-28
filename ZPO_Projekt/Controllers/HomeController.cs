using ZPO_Projekt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZPO_Projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _log;

        public HomeController(ILogger log)
        {
            _log = log;
        }

        public ActionResult Index()
        {
            _log.Log(LogLevel.Info, "Inside Home/Index");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Technologies that have been used to make this page:";

            _log.Log(LogLevel.Info, "Inside Home/About");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "The author of this page is:";

            _log.Log(LogLevel.Info, "Inside Home/Contact");

            return View();
        }
    }
}