using Newtonsoft.Json;
using Stage.AlienTrick.Attributes;
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
using System.IO;
using Stage.AlienTrick.Models;
using System.Web.UI.WebControls;

namespace Stage.AlienTrick.Controllers
{

    public class StudentenController : Controller
    {
        
        private PortalEntities db = new PortalEntities();
        // GET: Studenten
        [Rights(AllowStudents = true, AllowAdmins = true)]
        public ActionResult Index(string searchString)
        {
            var studentenCollection = db.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                studentenCollection = studentenCollection.Where(A => A.Lastname.ToLower().Contains(searchString.ToLower()));
            }

            return View(studentenCollection.ToArray());
        }

        public ActionResult Persoonlijkevoortgang(Persoonlijkmodel persoonlijkmodel , Student student , WindowsUsersAndRoles windows)
        {
            //var studentuser = db.WindowsUsersAndRoles.Where(d => d.WindowsUserAccount == db.Students.Find().Windowsuseraccount);
            var userstudent = db.Students.Where(d => d.Windowsuseraccount.Equals(User.Identity.Name)).FirstOrDefault();
            var Student_ID = db.Tasks.Select(s => s.Student_ID);

            persoonlijkmodel.voortgang = userstudent.AmountOfhoursToComplete / 100 * userstudent.AmountofbookedHours;
            persoonlijkmodel.task = userstudent.Tasks.Where(d => d.Student_ID == userstudent.ID).ToList();
            return View(persoonlijkmodel);
        }
        //create
        [Rights(AllowAdmins = true)]
        public ActionResult Create()
        {
            return View();
        }

        [Rights(AllowAdmins = true)]
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
        [Rights(AllowAdmins = true)]
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
        [Rights(AllowAdmins = true)]
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
        [Rights(AllowAdmins = true)]
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

        [Rights(AllowAdmins = true)]
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

        [Rights(AllowStudents = true, AllowAdmins = true)]
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
        [Rights(AllowStudents = true, AllowAdmins = true)]
        private double GetStudentProcessPercentage(Student s)
        {
            return 100.00 / s.AmountOfhoursToComplete * s.AmountofbookedHours;
        }

        [Rights(AllowStudents = true, AllowAdmins = true)]
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

        [Rights(AllowStudents = true, AllowAdmins = true)]
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

        [Rights (AllowStudents = true , AllowAdmins = true)]
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
        [Rights(AllowStudents = true, AllowAdmins = true)]
        [HttpGet]
        public ActionResult CreateMeeting(int? id, Models.Takenmodel takenmodel)
        {
            return View(takenmodel);
        }

        [Rights(AllowStudents = true, AllowAdmins = true)]
        [HttpPost]
        public ActionResult CreateMeeting(int? id , Models.Takenmodel takenmodel, Student student)
        {
            var stm = db.Students.Where(s => s.ID == id).FirstOrDefault();
            Task task = new Task();

            if(takenmodel.Type == "Verlof")
            {
                Appointment appointment = new Appointment();
                task.Time = takenmodel.Time;
                task.BeginDate = takenmodel.BeginDate;
                task.SchoolOrWork = takenmodel.SchoolOrWork;
                task.Student_ID = id;
                task.TaskName = takenmodel.TaskName;
                task.Taskdescription = takenmodel.TaskDescription;
                task.Type = takenmodel.Type;
                task.Rating = takenmodel.Rating;
                task.TaskApproved = 3;
                
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            if (takenmodel.Type == "Afspraak")
            {

                task.BeginDate = takenmodel.BeginDate;
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

        [Rights(AllowStudents = true, AllowAdmins = true)]
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
        [Rights(AllowAdmins = true)]
        [HttpGet]
        public ActionResult Completethemeeting(int? id)
        {
             Models.Takenmodel takenmodel = new Models.Takenmodel();
             var taak = db.Tasks.Where(d => d.ID == id).FirstOrDefault();
             var studentstask = db.Students.Where(t => t.ID == taak.Student_ID).FirstOrDefault();
            taak.TaskApproved = 2;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [Rights(AllowAdmins = true , AllowStudents = true)]
        public ActionResult Inleveren(int? id)
        {
            Task task = db.Tasks.Where(t => t.ID == id).FirstOrDefault();
            if (task.TaskApproved == 0)
            { 
            task.TaskApproved = 1;
            db.SaveChanges();
            TempData["msg"] = "<script>alert('Jou taak is ingeleverd!');</script>";
            }

            if(task.TaskApproved == 1)
            {
                TempData["msg"] = "<script>alert('Je hebt deze taak al ingeleverd!');</script>";
            }

            if(task.TaskApproved == 3)
            {
                TempData["msg"] = "<script>alert('Omdat dit een afspraak was is deze meteen goedgekeurd!');</script>";
                task.TaskApproved = 2;
                db.SaveChanges();
            }
            return RedirectToAction("persoonlijkevoortgang");
        }
        
       

        [Rights(AllowAdmins = true)]
        [HttpPost]
        public ActionResult Approvetask(int? id , Models.Takenmodel takenmodel, Task task)
        {
            Task studenttask = db.Tasks.Where(d => d.Student_ID == id).FirstOrDefault();
            studenttask.TaskApproved = 2;
            db.SaveChanges();
            return RedirectToAction("index");

            
        }

        [Rights(AllowAdmins = true)]
        [HttpGet]
        public ActionResult Applyrating(int? id , Models.Takenmodel takenmodel , Task task)
        {
            var ttb = db.Tasks.Where(d => d.Student_ID == id).FirstOrDefault();
            takenmodel.TaskName = ttb.TaskName;
            takenmodel.TaskDescription = ttb.Taskdescription;
            takenmodel.taskApproved = ttb.TaskApproved;
            takenmodel.Rating = ttb.Rating;
            takenmodel.Type = ttb.Type;
            return View(takenmodel);
        }

        [Rights(AllowAdmins = true)]
        [HttpPost]
        public ActionResult Applyrating(int? id , [Bind(Include ="Rating")] Task task , Models.Takenmodel takenmodel)
        {
            var ttb = db.Tasks.Where(d => d.Student_ID == id).FirstOrDefault();

            ttb.Rating = takenmodel.Rating;
            db.Entry(ttb).State = EntityState.Modified;
            db.SaveChanges();

            return View("index");

        }

        [Rights(AllowAdmins = true)]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Applyforjob(int? id, int vcid, Student student, JobApplication jobApplication , Sollicitatiemodel sollicitatiemodel)
        {
            return View(sollicitatiemodel);
        }


        [Rights(AllowAdmins = true)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Applyforjob(int? id , int vcid , JobApplication jobApplication , Sollicitatiemodel sollicitatiemodel)
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
        [HttpGet]
        public ActionResult CreateMeetingstudent(int? id , Takenmodel takenmodel)
        {
            return View(takenmodel);
        }
        [HttpPost]
        public ActionResult CreateMeetingstudent(int ID, Takenmodel takenmodel , WindowsUsersAndRoles windows)

        {
            Task task = new Task();
            task.Stagebegeleider = takenmodel.Stagebegeleider;
            task.BeginDate = takenmodel.BeginDate;
            task.TaskName = takenmodel.TaskName;
            task.Taskdescription = takenmodel.TaskDescription;
            task.Type = "Afspraak";
            task.TaskApproved = 0;
            db.SaveChanges();

            var apmt = db.Appointments.Where(d => d.Task_ID == d.Task.ID).FirstOrDefault();
            apmt.Task_ID = task.ID;
            db.SaveChanges();
            return RedirectToAction("Persoonlijkevoortgang");
        }
    }

}
     
        
 

