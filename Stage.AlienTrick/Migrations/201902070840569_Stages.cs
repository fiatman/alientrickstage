namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.student_ID)
                .Index(t => t.student_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stages", "student_ID", "dbo.Students");
            DropIndex("dbo.Stages", new[] { "student_ID" });
            DropTable("dbo.Stages");
        }
    }
}
