namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vacatures
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacatures()
        {
            Stages = new HashSet<Stages>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string StageTitle { get; set; }

        public int AmountofHours { get; set; }

        public int AmountofStudents { get; set; }

        [StringLength(255)]
        public string StageDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stages> Stages { get; set; }
    }
}
