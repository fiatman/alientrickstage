using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Persoonlijkmodel
    {
        public double voortgang { get; set; }
        public Student student { get; set; }
        public ICollection <Task> task { get; set; }

    }
}