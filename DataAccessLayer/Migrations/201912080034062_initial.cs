namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 64),
                        LastName = c.String(maxLength: 64),
                        UserName = c.String(maxLength: 64),
                        Email = c.String(),
                        PasswordSalt = c.String(maxLength: 100),
                        PasswordHash = c.String(maxLength: 100),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 64),
                        Description = c.String(maxLength: 512),
                        Location = c.String(maxLength: 64),
                        PhoneNumber = c.String(maxLength: 15),
                        UserName = c.String(maxLength: 64),
                        Email = c.String(),
                        PasswordSalt = c.String(maxLength: 100),
                        PasswordHash = c.String(maxLength: 100),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobOffers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 64),
                        Description = c.String(maxLength: 512),
                        Location = c.String(maxLength: 64),
                        CompanyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationStatus = c.Int(nullable: false),
                        JobOfferId = c.Guid(nullable: false),
                        JobseekerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobOffers", t => t.JobOfferId, cascadeDelete: true)
                .ForeignKey("dbo.Jobseekers", t => t.JobseekerId, cascadeDelete: true)
                .Index(t => t.JobOfferId)
                .Index(t => t.JobseekerId);
            
            CreateTable(
                "dbo.Jobseekers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TitlesBeforeName = c.String(maxLength: 32),
                        TitlesAfterName = c.String(maxLength: 32),
                        FirstName = c.String(maxLength: 64),
                        LastName = c.String(maxLength: 64),
                        MobilePhoneNumber = c.String(),
                        Address = c.String(maxLength: 1024),
                        BirthDate = c.DateTime(nullable: false),
                        HighestEducation = c.Int(nullable: false),
                        UserName = c.String(maxLength: 64),
                        Email = c.String(),
                        PasswordSalt = c.String(maxLength: 100),
                        PasswordHash = c.String(maxLength: 100),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplications", "JobseekerId", "dbo.Jobseekers");
            DropForeignKey("dbo.JobApplications", "JobOfferId", "dbo.JobOffers");
            DropForeignKey("dbo.JobOffers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.JobApplications", new[] { "JobseekerId" });
            DropIndex("dbo.JobApplications", new[] { "JobOfferId" });
            DropIndex("dbo.JobOffers", new[] { "CompanyId" });
            DropTable("dbo.Jobseekers");
            DropTable("dbo.JobApplications");
            DropTable("dbo.JobOffers");
            DropTable("dbo.Companies");
            DropTable("dbo.Admins");
        }
    }
}
