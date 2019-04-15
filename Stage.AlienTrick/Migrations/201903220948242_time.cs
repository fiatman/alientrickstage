namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class time : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Time", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Time");
        }
    }
}
