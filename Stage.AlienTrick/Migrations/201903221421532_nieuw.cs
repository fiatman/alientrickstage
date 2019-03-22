namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nieuw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vacatures", "student_ID", c => c.Int());
            CreateIndex("dbo.Vacatures", "student_ID");
            AddForeignKey("dbo.Vacatures", "student_ID", "dbo.Students", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacatures", "student_ID", "dbo.Students");
            DropIndex("dbo.Vacatures", new[] { "student_ID" });
            DropColumn("dbo.Vacatures", "student_ID");
        }
    }
}
