namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versie211 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.Students");
            DropForeignKey("dbo.Messages", "Sender_ID", "dbo.Students");
            DropIndex("dbo.Students", new[] { "Stage_ID" });
            AddColumn("dbo.Stages", "StudentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Tasks", "TaskName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tasks", "Taskdescription", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tasks", "Type", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tasks", "SchoolOrWork", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Students", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Firstname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Students", "Lastname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Students", "City", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Students", "School", c => c.String(maxLength: 255));
            AlterColumn("dbo.Students", "Nationality", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Students", "Studentnumber", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Students", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Stages", "StageName", c => c.String(maxLength: 255));
            AlterColumn("dbo.Stages", "NeededEducation", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Vacatures", "StageTitle", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Vacatures", "StageDescription", c => c.String(maxLength: 255));
            CreateIndex("dbo.Students", "ID");
            AddForeignKey("dbo.Tasks", "Student_ID", "dbo.Students", "ID");
            AddForeignKey("dbo.Messages", "Receiver_ID", "dbo.Students", "ID");
            AddForeignKey("dbo.Messages", "Sender_ID", "dbo.Students", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_ID", "dbo.Students");
            DropForeignKey("dbo.Messages", "Receiver_ID", "dbo.Students");
            DropForeignKey("dbo.Tasks", "Student_ID", "dbo.Students");
            DropIndex("dbo.Students", new[] { "ID" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Vacatures", "StageDescription", c => c.String());
            AlterColumn("dbo.Vacatures", "StageTitle", c => c.String());
            AlterColumn("dbo.Stages", "NeededEducation", c => c.String());
            AlterColumn("dbo.Stages", "StageName", c => c.String());
            AlterColumn("dbo.Students", "ID", c => c.Int());
            AlterColumn("dbo.Students", "Studentnumber", c => c.String());
            AlterColumn("dbo.Students", "Nationality", c => c.String());
            AlterColumn("dbo.Students", "School", c => c.String());
            AlterColumn("dbo.Students", "City", c => c.String());
            AlterColumn("dbo.Students", "Lastname", c => c.String());
            AlterColumn("dbo.Students", "Firstname", c => c.String());
            AlterColumn("dbo.Students", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Tasks", "SchoolOrWork", c => c.String());
            AlterColumn("dbo.Tasks", "Type", c => c.String());
            AlterColumn("dbo.Tasks", "Taskdescription", c => c.String());
            AlterColumn("dbo.Tasks", "TaskName", c => c.String());
            DropColumn("dbo.Stages", "StudentID");
            AddPrimaryKey("dbo.Students", "ID");
            RenameColumn(table: "dbo.Students", name: "ID", newName: "Stage_ID");
            AddColumn("dbo.Students", "ID", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Students", "Stage_ID");
            AddForeignKey("dbo.Messages", "Sender_ID", "dbo.Students", "ID");
            AddForeignKey("dbo.Messages", "Receiver_ID", "dbo.Students", "ID");
            AddForeignKey("dbo.Tasks", "Student_ID", "dbo.Students", "ID");
        }
    }
}
