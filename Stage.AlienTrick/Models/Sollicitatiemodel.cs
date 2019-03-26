using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Sollicitatiemodel
    {
        public string CandidateMailadress { get; set; }
        public string CandidateName { get; set; }
        public string CandidateLastName { get; set; }
        public int Candidatephonenumber { get; set; }
        public DateTime Applicationdate { get; set; }
        public Vacature vacature { get; set; }
        public string Enclosureurl { get; set; }
    }
}