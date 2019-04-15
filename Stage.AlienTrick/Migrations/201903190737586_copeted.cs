namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class copeted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Taskcompleted", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Taskcompleted");
        }
    }
}
