namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ding2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Stagebegeleider", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Stagebegeleider");
        }
    }
}
