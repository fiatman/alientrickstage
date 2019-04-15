namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vacature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacature()
        {
            Stages = new HashSet<Stage>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string StageTitle { get; set; }

        public int AmountofHours { get; set; }

        public int AmountofStudents { get; set; }

        [StringLength(255)]
        public string StageDescription { get; set; }
        public Student student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stage> Stages { get; set; }
        public string url { get; set; }
    }
}
