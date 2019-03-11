namespace Stage.AlienTrick
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PortalEntities : DbContext
    {
        public PortalEntities()
            : base("name=PortalEntities")
        {
        }

        public virtual DbSet<Student> Studenten { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Vacature> Vacatures { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<Appointment> Appointments { get; set; }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public class Student
        {
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public int Age { get; set; }
            public string City { get; set; }
            public string School { get; set; }
            public string Nationality { get; set; }
            public string Studentnumber { get; set; }
            public decimal Compensation { get; set; }
            public string Description { get; set; }
            public int StudentStatus { get; set; }
            public Stage Stage { get; set; }
            public int Progress { get; set; }
            public double AmountOfhoursToComplete { get; set; }
            public double AmountofbookedHours { get; set; }
        }
        public class Stage
        {
            public int ID { get; set; }
            public string StageName { get; set; }
            public DateTime Beginperiod { get; set; }
            public DateTime Endperiod { get; set; }
            public string StageDescription { get; set; }
            public string NeededEducation { get; set; }
            public Vacature Vacature { get; set; }
            public int VacatureID { get; set; }
            public Student Student { get; set; }
            public int StudentID { get; set; }
        }
        public class Vacature
        {
            public int ID { get; set; }
            public String StageTitle { get; set; }
            public int AmountofHours { get; set; }
            public int AmountofStudents { get; set; }
            public string StageDescription { get; set; }


        }

        public class Task
        {
            public int ID { get; set; }
            public Student Student { get; set; }
            public string TaskName { get; set; }
            public string Taskdescription { get; set; }
            public string Type { get; set; }
            public int Rating { get; set; }
            public string SchoolOrWork { get; set; }
            public string Status { get; set; }
        }

        public class Appointment
        {
            public int ID { get; set; }
            public Task Task { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }

        }

        public class Message
        {
            public int MessageID { get; set; }
            public Student Sender { get; set; }
            public string SenderName { get; set; }
            public Student Receiver { get; set; }
            public string ReceiverName { get; set; }
            public string MessageText { get; set; }
            public DateTime? TimeSent { get; set; }
        }

        public class JobApplication
        {
            public int ID { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string CandidateMailadress { get; set; }
            public string CandidateName { get; set; }
            public int CandidatePhoneNumber { get; set; }
            public string CandidateLastName { get; set; }
            public string Enclosureurl { get; set; }
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stage>()
                    .Property(s => s.StageName)
                    .IsRequired()
                    .HasMaxLength(255);

            modelBuilder.Entity<Stage>()
                .HasRequired(s => s.Student)
                .WithRequiredPrincipal(s => s.Stage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stage>()
                  .Property(s => s.NeededEducation)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Stage>()
                  .Property(s => s.Beginperiod)
                  .IsRequired();

            modelBuilder.Entity<Stage>()
                  .Property(s => s.Endperiod)
                  .IsRequired();

            modelBuilder.Entity<Student>()
                  .Property(s => s.Firstname)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Student>()
                  .Property(s => s.Lastname)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Student>()
                  .Property(s => s.Nationality)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Student>()
                  .Property(s => s.School)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Student>()
                  .Property(s => s.Studentnumber)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Student>()
                  .Property(s => s.Age)
                  .IsRequired();

            modelBuilder.Entity<Student>()
                  .Property(s => s.City)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Student>()
                .Property(s => s.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Vacature>()
                  .Property(s => s.StageTitle)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Vacature>()
                  .Property(s => s.StageDescription)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Vacature>()
                  .Property(s => s.AmountofHours)
                  .IsRequired();

            modelBuilder.Entity<Task>()
                  .Property(s => s.SchoolOrWork)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Task>()
                  .Property(s => s.TaskName)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Task>()
                  .Property(s => s.Type)
                  .IsRequired()
                  .HasMaxLength(255);

            modelBuilder.Entity<Task>()
                  .Property(s => s.Taskdescription)
                  .IsRequired()
                  .HasMaxLength(255);
        }
    }
}
