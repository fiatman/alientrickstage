namespace Stage.AlienTrick
{
    using System;

    public partial class Appointments
    {
        public int ID { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? Task_ID { get; set; }

        public virtual Task Tasks { get; set; }
    }
}
