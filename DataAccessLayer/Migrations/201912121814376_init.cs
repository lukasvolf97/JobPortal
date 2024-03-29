namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 64),
                        Email = c.String(),
                        PasswordSalt = c.String(maxLength: 100),
                        PasswordHash = c.String(maxLength: 100),
                        Roles = c.String(),
                        FirstName = c.String(maxLength: 64),
                        LastName = c.String(maxLength: 64),
                        Name = c.String(maxLength: 64),
                        Description = c.String(maxLength: 512),
                        Location = c.String(maxLength: 64),
                        PhoneNumber = c.String(maxLength: 15),
                        TitlesBeforeName = c.String(maxLength: 32),
                        TitlesAfterName = c.String(maxLength: 32),
                        FirstName1 = c.String(maxLength: 64),
                        LastName1 = c.String(maxLength: 64),
                        MobilePhoneNumber = c.String(),
                        Address = c.String(maxLength: 1024),
                        BirthDate = c.DateTime(),
                        HighestEducation = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationStatus = c.Int(nullable: false),
                        JobOfferId = c.Guid(),
                        JobseekerId = c.Guid(),
                        CompanyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CompanyId)
                .ForeignKey("dbo.JobOffers", t => t.JobOfferId)
                .ForeignKey("dbo.Users", t => t.JobseekerId)
                .Index(t => t.JobOfferId)
                .Index(t => t.JobseekerId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.JobOffers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(nullable: false, maxLength: 512),
                        Salary = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(nullable: false, maxLength: 64),
                        CompanyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplications", "JobseekerId", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "JobOfferId", "dbo.JobOffers");
            DropForeignKey("dbo.JobOffers", "CompanyId", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "CompanyId", "dbo.Users");
            DropIndex("dbo.JobOffers", new[] { "CompanyId" });
            DropIndex("dbo.JobApplications", new[] { "CompanyId" });
            DropIndex("dbo.JobApplications", new[] { "JobseekerId" });
            DropIndex("dbo.JobApplications", new[] { "JobOfferId" });
            DropTable("dbo.JobOffers");
            DropTable("dbo.JobApplications");
            DropTable("dbo.Users");
        }
    }
}
