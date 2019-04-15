namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplications", "ApplyAnswered", c => c.Byte(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplications", "ApplyAnswered");
        }
    }
}
