namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Task_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tasks", t => t.Task_ID)
                .Index(t => t.Task_ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false, maxLength: 255),
                        Taskdescription = c.String(nullable: false, maxLength: 255),
                        Type = c.String(nullable: false, maxLength: 255),
                        Rating = c.Int(nullable: false),
                        SchoolOrWork = c.String(nullable: false, maxLength: 255),
                        Status = c.String(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.Student_ID)
                .Index(t => t.Student_ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 255),
                        Lastname = c.String(nullable: false, maxLength: 255),
                        Age = c.Int(nullable: false),
                        City = c.String(nullable: false, maxLength: 255),
                        School = c.String(maxLength: 255),
                        Nationality = c.String(nullable: false, maxLength: 255),
                        Studentnumber = c.String(nullable: false, maxLength: 255),
                        Compensation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        StudentStatus = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        Stage_ID = c.Int(),
                        AmountOfhoursToComplete = c.Double(nullable: false),
                        AmountofbookedHours = c.Double(nullable: false),
                        HoursAccepted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stages", t => t.Stage_ID)
                .Index(t => t.Stage_ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        TimeSent = c.DateTime(),
                        Receiver_ID = c.Int(),
                        Sender_ID = c.Int(),
                        SenderName = c.String(),
                        ReceiverName = c.String(),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Students", t => t.Receiver_ID)
                .ForeignKey("dbo.Students", t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.Sender_ID);
            
            CreateTable(
                "dbo.Stages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StageName = c.String(maxLength: 255),
                        Beginperiod = c.DateTime(nullable: false),
                        Endperiod = c.DateTime(nullable: false),
                        StageDescription = c.String(),
                        NeededEducation = c.String(nullable: false, maxLength: 255),
                        VacatureID = c.Int(),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Vacatures", t => t.VacatureID, cascadeDelete: true)
                .Index(t => t.VacatureID);
            
            CreateTable(
                "dbo.Vacatures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StageTitle = c.String(nullable: false, maxLength: 255),
                        AmountofHours = c.Int(nullable: false),
                        AmountofStudents = c.Int(nullable: false),
                        StageDescription = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.__MigrationHistory",
                c => new
                    {
                        MigrationId = c.String(nullable: false, maxLength: 150),
                        ContextKey = c.String(nullable: false, maxLength: 300),
                        Model = c.Binary(nullable: false),
                        ProductVersion = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.MigrationId, t.ContextKey });
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationDate = c.DateTime(nullable: false),
                        CandidateMailadress = c.String(),
                        CandidateName = c.String(),
                        CandidatePhoneNumber = c.Int(nullable: false),
                        CandidateLastName = c.String(),
                        Enclosureurl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Stages", "VacatureID", "dbo.Vacatures");
            DropForeignKey("dbo.Students", "Stage_ID", "dbo.Stages");
            DropForeignKey("dbo.Messages", "Sender_ID", "dbo.Students");
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.Students");
            DropForeignKey("dbo.Appointments", "Task_ID", "dbo.Tasks");
            DropIndex("dbo.Stages", new[] { "VacatureID" });
            DropIndex("dbo.Messages", new[] { "Sender_ID" });
            DropIndex("dbo.Messages", new[] { "Receiver_ID" });
            DropIndex("dbo.Students", new[] { "Stage_ID" });
            DropIndex("dbo.Tasks", new[] { "Student_ID" });
            DropIndex("dbo.Appointments", new[] { "Task_ID" });
            DropTable("dbo.JobApplications");
            DropTable("dbo.__MigrationHistory");
            DropTable("dbo.Vacatures");
            DropTable("dbo.Stages");
            DropTable("dbo.Messages");
            DropTable("dbo.Students");
            DropTable("dbo.Tasks");
            DropTable("dbo.Appointments");
        }
    }
}
