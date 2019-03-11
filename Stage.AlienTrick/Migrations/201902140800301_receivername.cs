namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receivername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ReceiverName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ReceiverName");
        }
    }
}
