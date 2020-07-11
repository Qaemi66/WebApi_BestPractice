using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net.NetworkInformation;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Extensions;
using WebApi_BestPractice.Domain.BaseClasses;
using WebApi_BestPractice.Domain.Entities;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data
{
    public class ApplicationDbContext : DbContext
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
