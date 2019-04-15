namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ding1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "WindowsUsersAndRoles_ID", "dbo.WindowsUsersAndRoles");
            DropIndex("dbo.Students", new[] { "WindowsUsersAndRoles_ID" });
            AddColumn("dbo.Students", "Windowsuseraccount", c => c.String());
            DropColumn("dbo.Students", "WindowsUsersAndRoles_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "WindowsUsersAndRoles_ID", c => c.Int());
            DropColumn("dbo.Students", "Windowsuseraccount");
            CreateIndex("dbo.Students", "WindowsUsersAndRoles_ID");
            AddForeignKey("dbo.Students", "WindowsUsersAndRoles_ID", "dbo.WindowsUsersAndRoles", "ID");
        }
    }
}
