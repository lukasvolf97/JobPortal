using DataAccessLayer.Config;
using DataAccessLayer.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class JobPortalDbContext : DbContext
    {

        public JobPortalDbContext() : base(EntityFrameworkInstaller.ConnectionString)
        {
            //Database.SetInitializer(new DatabaseInitializer());
            _ = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public JobPortalDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Jobseeker> Jobseekers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
