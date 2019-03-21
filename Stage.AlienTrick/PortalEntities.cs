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

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Vacature> Vacatures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stage>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Stage)
                .HasForeignKey(e => e.Stage_ID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Messages)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Receiver_ID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Messages1)
                .WithOptional(e => e.Student1)
                .HasForeignKey(e => e.Sender_ID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Student_ID);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Task)
                .HasForeignKey(e => e.Task_ID);

            modelBuilder.Entity<Vacature>()
                .HasMany(e => e.Stages)
                .WithOptional(e => e.Vacature)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Appointment>()
                .Property(f => f.BeginDate)
                .HasColumnType("datetime");
        }
    }
}
