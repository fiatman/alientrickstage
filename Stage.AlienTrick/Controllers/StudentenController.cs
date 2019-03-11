using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static Stage.AlienTrick.PortalEntities;

namespace Stage.AlienTrick.Controllers
{
    public class StudentenController : Controller
    {
        private PortalEntities db = new PortalEntities();
        // GET: Studenten
        public ActionResult Index(string searchString)
        {
            var studentenCollection = db.Studenten.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                studentenCollection = studentenCollection.Where(A => A.Lastname.ToLower().Contains(searchString.ToLower()));
            }

            return View(studentenCollection.ToArray());
        }
        //create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Firstname,Lastname,Age,City,School,Nationality,Studentnumber,Compensation,Description,StudentStatus,Progress, AmountOfhoursToComplete, AmountofbookedHours")] Student students)
        {
           
            
                students.AmountofbookedHours = 0;
            

            if (ModelState.IsValid)
            {
                db.Studenten.Add(new Student {
                    Firstname = students.Firstname,
                    Lastname = students.Lastname,
                    Age = students.Age,
                    City = students.City,
                    School = students.School,
                    Nationality = students.Nationality,
                    Studentnumber = students.Studentnumber,
                    Compensation = students.Compensation,
                    Description = students.Description,
                    StudentStatus = students.StudentStatus,
                    Progress = students.Progress,
                    AmountofbookedHours = students.AmountofbookedHours,
                    AmountOfhoursToComplete = students.AmountOfhoursToComplete

                    
                });
                db.SaveChanges();
                return View("index");
            }
            return View();
        }

        //Edit
        public ActionResult Edit(int? ID)
        {
            if(ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Studenten.Find(ID);
            if (ID == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit ([Bind(Include ="ID, Firstname, Lastname, Age, City, School, Nationality, Studentnumber, Compensation, Description, StudentStatus, Progress, AmountOfhoursToComplete, AmountofbookedHours")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return View("index");
            }

            return View(student);
        }

        public ActionResult GetProgress(Student student, Models.Voortgangsmodel voortgangsmodel)
        {

            voortgangsmodel.ProgressPercentage = GetStudentProcessPercentage(student);
            voortgangsmodel.student = student;


                return View(voortgangsmodel);
            
        }

        private double GetStudentProcessPercentage(Student s)
        {
            return 100.00 / s.AmountOfhoursToComplete * s.AmountofbookedHours;
        }


        public ActionResult AddHours(int? id)
        {
            Models.Voortgangsmodel voortgang = new Models.Voortgangsmodel();
            var student = db.Studenten.Where(s => s.ID == id).FirstOrDefault();
            voortgang.AmountofbookedHours = student.AmountofbookedHours;
            voortgang.AmountOfHourstoComplete = student.AmountOfhoursToComplete;
            voortgang.student = student;
            return View(voortgang);
        }
    
        [HttpPost]
        public ActionResult AddHours(Models.Voortgangsmodel v)
        {
            var student = db.Studenten.Where(s => s.ID == v.student.ID).FirstOrDefault();

            double CalculateHours = student.AmountofbookedHours + v.AmountofbookedHours;

            student.AmountofbookedHours = CalculateHours;
            db.SaveChanges();
            


            return RedirectToAction("GetProgress", new
            {
                student.ID,
                student.AmountofbookedHours,
                student.AmountOfhoursToComplete
            });
        }
        [HttpPost]
        public ActionResult PlanMeeting([Bind(Include = "TaskName,Taskdescription,Type,Rating,SchoolOrWork,Status")]Task TaskOrMeeting)
        {

            if (ModelState.IsValid)
            {
                db.Tasks.Add(TaskOrMeeting);
                db.SaveChanges();
                return View("index");
            }
            return View();

        }
    }
     
    }
        
 

