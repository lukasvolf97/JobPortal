using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace DataAccessLayer.Config
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        //internal const string ConnectionString = "Data source=(localdb)\\MSSQLLocalDB;Database=JobPortalSample;Trusted_Connection=True;MultipleActiveResultSets=true";
        internal const string ConnectionString = "Data source=jobportal-pv179.database.windows.net;Initial Catalog=JobPortalSample; User ID= jobportalAdmin; Password=Pv179JobPortal";
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new JobPortalDbContext())
                    .LifestyleTransient(), 
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityFrameworkQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}
