namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dinges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Taskcomplete", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Taskcomplete");
        }
    }
}
