namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ja : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "LastHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "LastHours", c => c.Double(nullable: false));
        }
    }
}
