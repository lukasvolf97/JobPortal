using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<JobPortalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JobPortalDbContext context)
        {
            var Job1 = new JobOffer
            {
                Id = Guid.Parse("aa05dc64-5c17-40ce-a916-175165b9b91f"),
                Name = "Job1",
                Salary = 200,
                Location = "UK",
                Description = "description"
                //JobApplications = new List<JobApplication>()
            };

            var Job2 = new JobOffer
            {
                Id = Guid.Parse("aa05dc64-5d17-40ce-a916-175165b9b91f"),
                Name = "Job2",
                Salary = 1000,
                Location = "US",
                Description = "description"
                // JobApplications = new List<JobApplication>()
            };

            var User1 = new Jobseeker
            {
                Id = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b90f"),
                FirstName = "Jozef",
                LastName = "ASD"
            };

            var User2 = new Jobseeker
            {
                Id = Guid.Parse("aa07dc64-5c07-40fe-a916-175165b9b90f"),
                FirstName = "Peter",
                LastName = "ASD",
                HighestEducation = EducationType.Graduation,
                //JobApplications = new List<JobApplication>()
            };

            var User3 = new Jobseeker
            {
                Id = Guid.Parse("aa01dc64-5c07-40fe-a916-175165b9b90f"),
                FirstName = "Juraj",
                LastName = "ASD",
                Email = "mail@.com",
                //JobApplications = new List<JobApplication>()
            };

            var Comp1 = new Company
            {
                Id = Guid.Parse("aa05dc64-5c07-40fe-a916-175165b9b91f"),
                Name = "first",
                Description = "ASD",
                PhoneNumber = "0908754122",
                //JobOffers = new List<JobOffer> { Job1 },
                //JobApplications = new List<JobApplication>()
            };

            var Comp2 = new Company
            {
                Id = Guid.Parse("aa05dc64-5c07-40ce-a916-175165b9b91f"),
                Name = "second",
                Description = "ASDED",
                Location = "UK",
                PhoneNumber = "0906445442",
                //JobOffers = new List<JobOffer> { Job2 }
            };

            var JobApplication1 = new JobApplication
            {
                Id = Guid.Parse("aa05dc64-5c27-40ce-a916-175165b9b91f"),
                JobOffer = Job1,
                //Company = Comp1,
                //Jobseeker = User2,
                ApplicationStatus = Enums.ApplicationStatus.Undecided
            };


            var JobApplication2 = new JobApplication
            {
                Id = Guid.Parse("aa05dc84-5c27-40ce-a916-175165b9b91f"),
                JobOffer = Job1,
                //Company = Comp1,
                //Jobseeker = User3,
                ApplicationStatus = Enums.ApplicationStatus.Declined
            };

            var JobApplication3 = new JobApplication
            {
                Id = Guid.Parse("ab05dc64-5c27-40ce-a916-175165b9b91f"),
                JobOffer = Job1,
                //Company = Comp1,
                //Jobseeker = User2,
                ApplicationStatus = Enums.ApplicationStatus.Undecided
            };

            var Admin = new Admin
            {
                Id = Guid.Parse("2dc37ea7-9572-4699-9c7e-12d55bf50ee2"),
                UserName = "admin",
                PasswordHash = "7S0oJnS8oX0J/18c+koqOwmHFgk=",
                PasswordSalt = "ySe0ygqn9Mes9KFr2uxXdw==",
                Roles = "Admin"
            };

            context.Admins.Add(Admin);


            //Comp1.JobApplications.Add(JobApplication1);
            //Comp1.JobApplications.Add(JobApplication2);

            //User2.JobApplications.Add(JobApplication1);
            //User3.JobApplications.Add(JobApplication2);

            //Job1.JobApplications.Add(JobApplication1);
            //Job1.JobApplications.Add(JobApplication2);

            context.JobApplications.Add(JobApplication1);
            context.JobApplications.Add(JobApplication2);

            context.Companies.Add(Comp1);
            context.Companies.Add(Comp2);

            context.Jobseekers.Add(User1);
            context.Jobseekers.Add(User2);
            context.Jobseekers.Add(User3);

            context.JobOffers.Add(Job1);
            context.JobOffers.Add(Job2);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
