namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versie2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stages", "student_ID", "dbo.Students");
            DropIndex("dbo.Stages", new[] { "student_ID" });
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
                        TaskName = c.String(),
                        Taskdescription = c.String(),
                        Type = c.String(),
                        Rating = c.Int(nullable: false),
                        SchoolOrWork = c.String(),
                        Status = c.String(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.Student_ID)
                .Index(t => t.Student_ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        MessageText = c.String(),
                        TimeSent = c.DateTime(),
                        Receiver_ID = c.Int(),
                        Sender_ID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Students", t => t.Receiver_ID)
                .ForeignKey("dbo.Students", t => t.Sender_ID)
                .Index(t => t.Receiver_ID)
                .Index(t => t.Sender_ID);
            
            CreateTable(
                "dbo.Vacatures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StageTitle = c.String(),
                        AmountofHours = c.Int(nullable: false),
                        AmountofStudents = c.Int(nullable: false),
                        StageDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Stages", "StageName", c => c.String());
            AddColumn("dbo.Stages", "Beginperiod", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stages", "Endperiod", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stages", "StageDescription", c => c.String());
            AddColumn("dbo.Stages", "NeededEducation", c => c.String());
            AddColumn("dbo.Students", "Firstname", c => c.String());
            AddColumn("dbo.Students", "Lastname", c => c.String());
            AddColumn("dbo.Students", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "City", c => c.String());
            AddColumn("dbo.Students", "School", c => c.String());
            AddColumn("dbo.Students", "Nationality", c => c.String());
            AddColumn("dbo.Students", "Studentnumber", c => c.String());
            AddColumn("dbo.Students", "Compensation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Students", "Description", c => c.String());
            AddColumn("dbo.Students", "StudentStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Progress", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Stage_ID", c => c.Int());
            CreateIndex("dbo.Students", "Stage_ID");
            AddForeignKey("dbo.Students", "Stage_ID", "dbo.Stages", "ID");
            DropColumn("dbo.Stages", "student_ID");
            DropColumn("dbo.Students", "Voornaam");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Voornaam", c => c.String());
            AddColumn("dbo.Stages", "student_ID", c => c.Int());
            DropForeignKey("dbo.Messages", "Sender_ID", "dbo.Students");
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.Students");
            DropForeignKey("dbo.Appointments", "Task_ID", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Students", "Stage_ID", "dbo.Stages");
            DropIndex("dbo.Messages", new[] { "Sender_ID" });
            DropIndex("dbo.Messages", new[] { "Receiver_ID" });
            DropIndex("dbo.Students", new[] { "Stage_ID" });
            DropIndex("dbo.Tasks", new[] { "Student_ID" });
            DropIndex("dbo.Appointments", new[] { "Task_ID" });
            DropColumn("dbo.Students", "Stage_ID");
            DropColumn("dbo.Students", "Progress");
            DropColumn("dbo.Students", "StudentStatus");
            DropColumn("dbo.Students", "Description");
            DropColumn("dbo.Students", "Compensation");
            DropColumn("dbo.Students", "Studentnumber");
            DropColumn("dbo.Students", "Nationality");
            DropColumn("dbo.Students", "School");
            DropColumn("dbo.Students", "City");
            DropColumn("dbo.Students", "Age");
            DropColumn("dbo.Students", "Lastname");
            DropColumn("dbo.Students", "Firstname");
            DropColumn("dbo.Stages", "NeededEducation");
            DropColumn("dbo.Stages", "StageDescription");
            DropColumn("dbo.Stages", "Endperiod");
            DropColumn("dbo.Stages", "Beginperiod");
            DropColumn("dbo.Stages", "StageName");
            DropTable("dbo.Vacatures");
            DropTable("dbo.Messages");
            DropTable("dbo.Tasks");
            DropTable("dbo.Appointments");
            CreateIndex("dbo.Stages", "student_ID");
            AddForeignKey("dbo.Stages", "student_ID", "dbo.Students", "ID");
        }
    }
}
