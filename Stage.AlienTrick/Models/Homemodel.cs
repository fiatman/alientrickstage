using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Homemodel
    {
        public List<Vacature> vacatures { get; set; }
        public string CandidateMailadress { get; set; }
        public string CandidateName { get; set; }
        public string CandidateLastName { get; set; }
        public int Candidatephonenumber { get; set; }
        public DateTime Applicationdate { get; set; }
        public string Enclosureurl { get; set; }
        public int VacatureID { get; set; }
    }
}