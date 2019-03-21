namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kadkh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "BeginDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "BeginDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
