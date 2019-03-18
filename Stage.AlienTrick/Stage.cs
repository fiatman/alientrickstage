namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stage()
        {
            Students = new HashSet<Student>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string StageName { get; set; }

        public DateTime Beginperiod { get; set; }

        public DateTime Endperiod { get; set; }

        public string StageDescription { get; set; }

        [Required]
        [StringLength(255)]
        public string NeededEducation { get; set; }

        public int? VacatureID { get; set; }

        public int StudentID { get; set; }

        public virtual Vacature Vacature { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
        public object Student { get; internal set; }
    }
}
