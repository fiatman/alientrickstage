namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class windowsauthentication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WindowsUsersAndRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WindowsUserAccount = c.String(),
                        IsStudent = c.Byte(nullable: true),
                        IsAdmin = c.Byte(nullable: true),
                        IsSupervisor = c.Byte(nullable: true),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Students", "WindowsUsersAndRoles_ID", c => c.Int());
            CreateIndex("dbo.Students", "WindowsUsersAndRoles_ID");
            AddForeignKey("dbo.Students", "WindowsUsersAndRoles_ID", "dbo.WindowsUsersAndRoles", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "WindowsUsersAndRoles_ID", "dbo.WindowsUsersAndRoles");
            DropIndex("dbo.Students", new[] { "WindowsUsersAndRoles_ID" });
            DropColumn("dbo.Students", "WindowsUsersAndRoles_ID");
            DropTable("dbo.WindowsUsersAndRoles");
        }
    }
}
