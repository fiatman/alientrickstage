using Stage.AlienTrick.Attributes;
using Stage.AlienTrick.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpPost]
        public ActionResult Applyforjob(JobApplication jobApplication, Homemodel homemodel , IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }
            }
            jobApplication.CandidateName = homemodel.CandidateName;
            jobApplication.CandidateLastName = homemodel.CandidateLastName;
            jobApplication.CandidateMailadress = homemodel.CandidateMailadress;
            jobApplication.ApplicationDate = DateTime.Now;
            jobApplication.CandidatePhoneNumber = homemodel.Candidatephonenumber;
            jobApplication.Enclosureurl = homemodel.Enclosureurl;
            jobApplication.Vacature_id = homemodel.VacatureID;

            db.JobApplications.Add(jobApplication);
            db.SaveChanges();


            return RedirectToAction("index");
        }
    }
}