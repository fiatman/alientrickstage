namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inttodouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "AmountOfhoursToComplete", c => c.Double(nullable: false));
            AlterColumn("dbo.Students", "AmountofbookedHours", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "AmountofbookedHours", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "AmountOfhoursToComplete", c => c.Int(nullable: false));
        }
    }
}
