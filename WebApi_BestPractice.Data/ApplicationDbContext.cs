using Microsoft.EntityFrameworkCore;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Domain.BaseClasses;

namespace WebApi_BestPractice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var domainAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(domainAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(domainAssembly);
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }
    }
}
