namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Versie21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stages", "Vacature_ID", c => c.Int());
            CreateIndex("dbo.Stages", "Vacature_ID");
            AddForeignKey("dbo.Stages", "Vacature_ID", "dbo.Vacatures", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stages", "Vacature_ID", "dbo.Vacatures");
            DropIndex("dbo.Stages", new[] { "Vacature_ID" });
            DropColumn("dbo.Stages", "Vacature_ID");
        }
    }
}
