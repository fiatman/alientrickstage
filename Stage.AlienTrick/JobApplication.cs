namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JobApplication
    {
        public int ID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string CandidateMailadress { get; set; }

        public string CandidateName { get; set; }

        public int CandidatePhoneNumber { get; set; }

        public string CandidateLastName { get; set; }

        public string Enclosureurl { get; set; }
    }
}
