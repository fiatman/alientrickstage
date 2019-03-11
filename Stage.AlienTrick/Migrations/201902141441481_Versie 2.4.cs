namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versie24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "AmountOfhoursToComplete", c => c.Int());
            AddColumn("dbo.Students", "AmountofbookedHours", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "AmountofbookedHours");
            DropColumn("dbo.Students", "AmountOfhoursToComplete");
        }
    }
}
