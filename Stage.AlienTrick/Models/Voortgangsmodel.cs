using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Stage.AlienTrick.PortalEntities;

namespace Stage.AlienTrick.Models
{
    public partial class Voortgangsmodel
    {
        public Student student { get; set; }
        public double AmountOfHourstoComplete { get; set; }
        public double AmountofbookedHours { get; set; }
        public double ProgressPercentage { get; set; }

        public int HoursAccepted { get; set; }

    }
}