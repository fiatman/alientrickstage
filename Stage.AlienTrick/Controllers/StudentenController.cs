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
            var studentenCollection = db.Students.AsQueryable();

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
        public ActionResult Create([Bind(Include = "Firstname,Lastname,Age,City,School,Nationality,Studentnumber,Compensation,Description,Progress, AmountOfhoursToComplete, AmountofbookedHours")] Student students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(new Student {
                    Firstname = students.Firstname,
                    Lastname = students.Lastname,
                    Age = students.Age,
                    City = students.City,
                    School = students.School,
                    Nationality = students.Nationality,
                    Studentnumber = students.Studentnumber,
                    Compensation = students.Compensation,
                    Description = students.Description,
                    StudentStatus = 0,
                    Progress = 0,
                    AmountofbookedHours = 0,
                    AmountOfhoursToComplete = students.AmountOfhoursToComplete


                });
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        // Uren Goedkeuren
        [HttpGet]
        public ActionResult AcceptHours(int? id, Models.Voortgangsmodel voortgangsmodel)
        {
            var studentaccepthours = db.Students.Where(s => s.ID == id).FirstOrDefault();
            voortgangsmodel.AmountofbookedHours = studentaccepthours.AmountofbookedHours;
            voortgangsmodel.AmountOfHourstoComplete = studentaccepthours.AmountOfhoursToComplete;
            voortgangsmodel.student = studentaccepthours;
            voortgangsmodel.HoursAccepted = studentaccepthours.StudentStatus;
            return View(voortgangsmodel);
        }
        [HttpPost]
        public ActionResult AcceptHours(int? id, Models.Voortgangsmodel voortgangsmodel, Student studenthoursaccept)
        {
            var studentaccepthours = db.Students.Where(s => s.ID == id).FirstOrDefault();
            if (studentaccepthours.StudentStatus == 0)
            {
                studentaccepthours.StudentStatus = 1;
                db.SaveChanges();
            }
            else
            {
                if (studentaccepthours.AmountofbookedHours >= studentaccepthours.AmountOfhoursToComplete)
                {
                    TempData["msg"] = "<script>alert('Deze student heef zijn stageuren voltooid!');</script>";
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Er zijn geen uren meer gelogd sinds laatste goedkeuring!');</script>";
                    return RedirectToAction("index");
                }
            }

            return RedirectToAction("Index");
        }
    
        //Edit
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit ([Bind(Include ="ID, Firstname, Lastname, Age, City, School, Nationality, Studentnumber, Compensation, Description, Progress, AmountOfhoursToComplete, AmountofbookedHours")] Student student)
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
            if (student.AmountOfhoursToComplete == student.AmountofbookedHours)
            {
                TempData["msg"] = "<script>alert('Je hebt jou stageuren behaald! ');</script>";
            }

            return View(voortgangsmodel);
            
        }

        private double GetStudentProcessPercentage(Student s)
        {
            return 100.00 / s.AmountOfhoursToComplete * s.AmountofbookedHours;
        }


        public ActionResult AddHours(int? id, Student studentstage )
        {
            
            var student = db.Students.Where(s => s.ID == id).FirstOrDefault();
            if (studentstage.Stage != null || student.StudentStatus != 0)
            {
                if (student.AmountOfhoursToComplete == student.AmountofbookedHours)
                {
                    TempData["msg"] = "<script>alert('Je hebt jou stageuren behaald! ');</script>";
                    return RedirectToAction("index");
                }
                else
                {
                  
                    Models.Voortgangsmodel voortgang = new Models.Voortgangsmodel();
                    voortgang.AmountofbookedHours = student.AmountofbookedHours;
                    voortgang.AmountOfHourstoComplete = student.AmountOfhoursToComplete;
                    voortgang.student = student;
                    return View(voortgang);
                }
                
            }
            else
            {
                TempData["msg"] = "<script>alert('Student is niet gekoppeld aan stage, of de laatst ingevoerde uren zijn nog niet goedgekeurd!');</script>";
                return RedirectToAction("index");

            }
        }
    
        [HttpPost]
        public ActionResult AddHours(Models.Voortgangsmodel v)
        {
            var HoursAccepted = 0;
            var student = db.Students.Where(s => s.ID == v.student.ID).FirstOrDefault();

            double CalculateHours = student.AmountofbookedHours + v.AmountofbookedHours;

            if (student.AmountOfhoursToComplete == student.AmountofbookedHours)
            {
                TempData["msg"] = "<script>alert('Jouw stage zit erop! je hebt alle uren gedraaid die je moest draaien!');</script>";
                return RedirectToAction("index");
            }
            else
            {
                    student.AmountofbookedHours = CalculateHours;
                    student.StudentStatus = HoursAccepted;
                    db.SaveChanges();
            }


            return RedirectToAction("GetProgress", new
            {
                student.ID,
                student.AmountofbookedHours,
                student.AmountOfhoursToComplete
            });
        }


        public ActionResult PlanMeeting(string searchString2 , Models.Takenmodel takenmodel)
        {
                var studentens = db.Students.ToList();
                if (!string.IsNullOrWhiteSpace(searchString2))
                {
                    studentens = studentens
                        .Where(s => s.Lastname.ToLower().Contains(searchString2.ToLower()))
                        .ToList();
                }

            var result = new Models.Takenmodel();
                result.Students = studentens;
                return View(result);
            
        }
        [HttpGet]
        public ActionResult CreateMeeting(int? id, Models.Takenmodel takenmodel)
        {
            return View(takenmodel);
        }

        [HttpPost]
        public ActionResult CreateMeeting(int? id , Models.Takenmodel takenmodel, Student student)
        {
            var stm = db.Students.Where(s => s.ID == id).FirstOrDefault();
            Task task = new Task();

            if (takenmodel.Type == "Afspraak")
            {
                Appointment appointment = new Appointment();
                appointment.BeginDate = takenmodel.BeginDate;
                task.SchoolOrWork = takenmodel.SchoolOrWork;
                task.Student_ID = id;
                task.TaskName = takenmodel.TaskName;
                task.Taskdescription = takenmodel.TaskDescription;
                task.Type = takenmodel.Type;
                task.Rating = takenmodel.Rating;
                task.TaskApproved = 0;
                db.SaveChanges();

                var apmt = db.Appointments.Where(d => d.Task_ID == d.Task.ID).FirstOrDefault();
                apmt.Task_ID = task.ID;
                db.SaveChanges();
                return RedirectToAction("index");

            }
            else
            {
                task.SchoolOrWork = takenmodel.SchoolOrWork;
                task.Student_ID = id;
                task.TaskName = takenmodel.TaskName;
                task.Taskdescription = takenmodel.TaskDescription;
                task.Type = takenmodel.Type;
                task.Rating = takenmodel.Rating;
                task.TaskApproved = 0;



                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("index");
            }

        }

        [HttpGet]
        public ActionResult MeetingCompleted(int? id , Models.Takenmodel takenmodel , string SearchString)
        {
           
            var TaskCollection = db.Tasks.AsQueryable().Where(d => d.Student_ID == id);

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                TaskCollection = TaskCollection.Where(A => A.TaskName.ToLower().Contains(SearchString.ToLower()));
            }

            return View(TaskCollection.ToArray());


        }
        [HttpGet]
        public ActionResult Completethemeeting(int? id)
        {
             Models.Takenmodel takenmodel = new Models.Takenmodel();
             var taak = db.Tasks.Where(d => d.ID == id).FirstOrDefault();
             var studentstask = db.Students.Where(t => t.ID == taak.Student_ID).FirstOrDefault();
            takenmodel.student = studentstask;
            takenmodel.TaskName = taak.TaskName;
            takenmodel.TaskDescription = taak.Taskdescription;
            takenmodel.Type = taak.Type;
            return View(takenmodel);
        }
        [HttpPost]
        public ActionResult Completethemeeting(int? id , Models.Takenmodel takenmodel , Student student)
        {
            var sdt = db.Tasks.Where(d => d.Student_ID == id).FirstOrDefault();
            if (sdt == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                if(sdt.Type == "Afspraak")
                {
                    sdt.TaskApproved = 2;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('Omdat dit een afspraak was, is deze meteen goedgekeurd!');</script>";
                    return RedirectToAction("index");
                }
                if(sdt.TaskApproved == 1)
                {
                    TempData["msg"] = "<script>alert('Deze taak is al afgerond!');</script>";
                    return RedirectToAction("index");
                }
                if(sdt.TaskApproved == 2)
                {
                    TempData["msg"] = "<script>alert('Jouw taak is al goedgekeurd');</script>";
                    return RedirectToAction("index");
                }
                else
                {
                    sdt.TaskApproved = 1;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('Jouw taak is ingeleverd, nu wachten op goedkeuring');</script>";
                    return RedirectToAction("index");
                }
            }
        }

        [HttpPost]
        public ActionResult Approvetask(int? id , Models.Takenmodel takenmodel, Task task)
        {
            Task studenttask = db.Tasks.Where(d => d.Student_ID == id).FirstOrDefault();
            studenttask.TaskApproved = 2;
            db.SaveChanges();
            return RedirectToAction("index");

            
        }
    }
     
}
        
 

