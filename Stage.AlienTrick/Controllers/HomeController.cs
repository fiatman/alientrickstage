using Stage.AlienTrick.Attributes;
using Stage.AlienTrick.Models;
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
            Homemodel homemodel = new Homemodel();
            List<Vacature> vacatures = new List<Vacature>();
            homemodel.vacatures = vacatures;
            vacatures = (db.Vacatures).ToList();
            homemodel.vacatures = vacatures.ToList();
            return View(homemodel);
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

        [Rights(AllowAdmins = true)]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Applyforjob(int? id, int vcid, Student student, JobApplication jobApplication, Sollicitatiemodel sollicitatiemodel)
        {
            return View(sollicitatiemodel);
        }


        [Rights(AllowAdmins = true)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Applyforjob(int? id, int vcid, JobApplication jobApplication, Sollicitatiemodel sollicitatiemodel)
        {
            jobApplication.CandidateName = sollicitatiemodel.CandidateName;
            jobApplication.CandidateLastName = sollicitatiemodel.CandidateLastName;
            jobApplication.CandidateMailadress = sollicitatiemodel.CandidateMailadress;
            jobApplication.ApplicationDate = DateTime.Now;
            jobApplication.CandidatePhoneNumber = sollicitatiemodel.Candidatephonenumber;
            jobApplication.Enclosureurl = sollicitatiemodel.Enclosureurl;
            jobApplication.Vacature_id = vcid;

            db.JobApplications.Add(jobApplication);
            db.SaveChanges();


            return RedirectToAction("index");
        }
    }
}