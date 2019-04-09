namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "LastHours", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "LastHours");
        }
    }
}
