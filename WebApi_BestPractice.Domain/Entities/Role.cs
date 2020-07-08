using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Domain.Etities
{
    public class Role : BaseClasses.BaseEntity {

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
        }
    }
}
