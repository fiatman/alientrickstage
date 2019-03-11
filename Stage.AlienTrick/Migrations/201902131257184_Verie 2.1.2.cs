namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verie212 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "SenderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "SenderName");
        }
    }
}
