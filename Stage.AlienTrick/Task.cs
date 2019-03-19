namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string TaskName { get; set; }

        [Required]
        [StringLength(255)]
        public string Taskdescription { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        public byte Taskcomplete { get; set; }

        public int Rating { get; set; }

        [Required]
        [StringLength(255)]
        public string SchoolOrWork { get; set; }

        public string Status { get; set; }

        public int? Student_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual Student Student { get; set; }
    }
}
