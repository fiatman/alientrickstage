namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vacatures", "url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vacatures", "url");
        }
    }
}
