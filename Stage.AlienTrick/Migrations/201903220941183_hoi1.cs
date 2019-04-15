namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hoi1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "BeginDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "BeginDate");
        }
    }
}
