namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplications", "Vacature_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplications", "Vacature_id");
        }
    }
}
