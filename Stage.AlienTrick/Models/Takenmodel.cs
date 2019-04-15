using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Takenmodel
    {
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
        [Column(TypeName = "datetime")]
        public DateTime BeginDate { get; set; }
        public byte taskApproved { get; set; }
        public string Stagebegeleider { get; set; }
    }
}