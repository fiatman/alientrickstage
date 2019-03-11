namespace Stage.AlienTrick.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Verie2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationDate = c.DateTime(nullable: false),
                        CandidateMailadress = c.String(),
                        CandidateName = c.String(),
                        CandidatePhoneNumber = c.Int(nullable: false),
                        CandidateLastName = c.String(),
                        Enclosureurl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobApplications");
        }
    }
}
