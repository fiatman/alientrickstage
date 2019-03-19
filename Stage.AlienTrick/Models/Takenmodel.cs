using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Takenmodel
    {
        public Appointment appointment { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public Student student { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TimeSpan Time { get; set; }
        public string Type { get; set; }
        public int Rating { get; set; }
        public string SchoolOrWork { get; set; }
        public int Status { get; set; }
        public byte Taskcomplete { get; set; }
        public DateTime BeginDate { get; set; }
        public Appointment Appointment { get; set; }
        public byte taskApproved { get; set; }
    }
}