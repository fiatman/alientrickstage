namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hoi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "TaskApproved", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "TaskApproved");
        }
    }
}
