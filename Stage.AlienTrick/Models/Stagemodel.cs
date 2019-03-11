using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Stagemodel
    {
        public int VacatureID { get; set; }
        public int StudentID { get; set; }
        public IEnumerable<Student> Students{ get; set; }
        public DateTime Beginperiod { get; set; }
        public DateTime Endperiod { get; set; }
        public string NeededEducation { get; set; }
        public string StageDescription { get; set; }
        public string StageName { get; set; }
    }
}