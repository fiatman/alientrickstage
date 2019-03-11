namespace Stage.AlienTrick
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<JobApplications> JobApplications { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Stages> Stages { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Vacatures> Vacatures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stages>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Stages)
                .HasForeignKey(e => e.Stage_ID);

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Messages)
                .WithOptional(e => e.Students)
                .HasForeignKey(e => e.Receiver_ID);

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Messages1)
                .WithOptional(e => e.Students1)
                .HasForeignKey(e => e.Sender_ID);

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.Students)
                .HasForeignKey(e => e.Student_ID);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Tasks)
                .HasForeignKey(e => e.Task_ID);

            modelBuilder.Entity<Vacatures>()
                .HasMany(e => e.Stages)
                .WithOptional(e => e.Vacatures)
                .HasForeignKey(e => e.VacatureID)
                .WillCascadeOnDelete();
        }
    }
}
