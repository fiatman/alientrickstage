namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datums : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplications", "Meetingdate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplications", "Meetingdate");
        }
    }
}
