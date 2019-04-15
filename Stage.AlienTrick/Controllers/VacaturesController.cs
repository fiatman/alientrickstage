﻿using Stage.AlienTrick.Attributes;
using Stage.AlienTrick.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Stage.AlienTrick.PortalEntities;

namespace Stage.AlienTrick.Controllers
{
    public class VacaturesController : Controller
    {
        
        private PortalEntities db = new PortalEntities();
        // GET: Vacatures
        [AllowAnonymous]
        public ActionResult Index(string searchString)
        {
            var vacature = db.Vacatures.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                vacature = vacature
                    .Where(s => 
                        s.StageTitle.Contains(searchString) ||
                        s.StageDescription.Contains(searchString))
                    .ToList();
            }
            return View(vacature);
        }
        //Create
        [Rights(AllowAdmins = true)]
        public ActionResult Create()
        {
            return View();
        }

        //Post Create
        [Rights(AllowAdmins = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "StageTitle,ID,AmountofHours,AmountofStudents,StageDescription,url")] Vacature vacature)
        {
            if (ModelState.IsValid)
            {
                db.Vacatures.Add(vacature);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View("index");
        }

        //Edit
        [Rights(AllowAdmins = true)]
        public ActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacature vacature = db.Vacatures.Find(ID);
                if(vacature == null)
            {
                return HttpNotFound();
            }
            return View(vacature);
        }
        //Edit post
        [Rights(AllowAdmins = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StageTitle,AmountofHours,AmountofStudents,StageDescription,url")] Vacature vacature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(vacature);
        }

        //Delete
        [Rights(AllowAdmins = true)]

        public ActionResult Delete(int? ID)
        {
            if(ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacature vacature = db.Vacatures.Find(ID);
            if(vacature == null)
            {
                return HttpNotFound();
            }
            return View(vacature);
        }
        [Rights(AllowAdmins = true)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? ID)
        {
            Vacature vacature = db.Vacatures.Find(ID);
            db.Vacatures.Remove(vacature);
            db.SaveChanges();
            return RedirectToAction("index");

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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }
}