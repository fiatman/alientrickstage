using System;
using System.Collections.Generic;
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
        public int Type { get; set; }
        public int Rating { get; set; }
        public int SchoolOrWork { get; set; }
        public int Status { get; set; }
    }
}