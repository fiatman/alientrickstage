﻿using Stage.AlienTrick.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Stage.AlienTrick.Controllers
{
    public class StagesController : Controller
    {


        private PortalEntities db = new PortalEntities();

        //Get students
        public ActionResult Studentslist(Models.Stagemodel stagemodel, string searchString2)
        {
            var studentens = db.Students.ToList();
            if (!string.IsNullOrWhiteSpace(searchString2))
            {
                studentens = studentens
                    .Where(s => s.Lastname.ToLower().Contains(searchString2.ToLower()))
                    .ToList();
            }

            var result = new Stagemodel();
            result.Students = studentens;
            result.VacatureID = stagemodel.VacatureID;
            return View(result);
        }
        //GET VACATURES

        public ActionResult vacatureindex(string searchString1)
        {
            var vacature = db.Vacatures.ToList();
            if (!string.IsNullOrWhiteSpace(searchString1))
            {
                vacature = vacature.Where(v => v.StageTitle.ToLower().Contains(searchString1.ToLower())).ToList();
            }
            return View(vacature);
        }

        // GET: Stages
        public ActionResult Index(string searchString)
        {

            var stagescollections = db.Stages.ToList();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                stagescollections = db.Stages.ToList().Where(s => s.StageName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(stagescollections);
        }

        //create stages

        [HttpGet]
        public ActionResult Create(Stagemodel stagemodel)
        {
            return View(stagemodel);
        }

        [HttpPost]
        public ActionResult CreateStage(Stagemodel stagemodel)
        {
            //save
            
                var students = db.Students.FirstOrDefault(d => d.ID == stagemodel.StudentID);
                var CreateStage = new Stage();
                CreateStage.Beginperiod = stagemodel.Beginperiod;
                CreateStage.Endperiod = stagemodel.Endperiod;
                students.Stage_ID = stagemodel.VacatureID;
                CreateStage.StudentID = stagemodel.StudentID;
                CreateStage.StageName = stagemodel.StageName;
                CreateStage.VacatureID = stagemodel.VacatureID;
                CreateStage.StageDescription = stagemodel.StageDescription;
                CreateStage.NeededEducation = stagemodel.NeededEducation;

                db.Stages.Add(CreateStage);
                db.SaveChanges();
                
            
            return RedirectToAction("Index");
        }

        //edit

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage stage = db.Stages.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StageName,Beginperiod,Endperiod,StageDescription,NeededEducation,Vacature")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return View("index");

            }
            return View("index");
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