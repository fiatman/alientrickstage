namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Messages = new HashSet<Message>();
            Messages1 = new HashSet<Message>();
            Tasks = new HashSet<Task>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(255)]
        public string Lastname { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string School { get; set; }

        [Required]
        [StringLength(255)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(255)]
        public string Studentnumber { get; set; }

        public decimal Compensation { get; set; }

        public string Description { get; set; }

        public int StudentStatus { get; set; }

        public int Progress { get; set; }

        public int? Stage_ID { get; set; }

        public double AmountOfhoursToComplete { get; set; }

        public double AmountofbookedHours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages1 { get; set; }

        public virtual Stage Stage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
