using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Extensions;
using WebApi_BestPractice.Domain.BaseClasses;
using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var domainAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(domainAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(domainAssembly);
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddPluralizingTableNameConvention();

            modelBuilder.Seed();
        }

    }
}
