namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timespan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Time", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {  
            DropColumn("dbo.Appointments", "Time");
        }
    }
}
