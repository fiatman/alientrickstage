using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage.AlienTrick.Controllers
{
    public class HomeController : Controller
    {
        PortalEntities db = new PortalEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Vacature> vacatures = new List<Vacature>();
            vacatures = (db.Vacatures).ToList();
            return View(vacatures);
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