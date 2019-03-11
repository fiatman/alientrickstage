namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static Stage.AlienTrick.PortalEntities;

    public partial class Appointments
    {
        public int ID { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? Task_ID { get; set; }

        public virtual Task Tasks { get; set; }
    }
}
