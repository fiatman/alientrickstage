namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hoi2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.JobApplications", "Meetingdate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobApplications", "Meetingdate", c => c.DateTime(nullable: false));
        }
    }
}
