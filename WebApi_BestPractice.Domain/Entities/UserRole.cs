using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebApi_BestPractice.Domain.BaseClasses;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Domain.Entities
{
    public class UserRole : IEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
        public User User { get; set; }
        public Role Role { get; set; }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(p=> new { p.RoleId, p.UserId});

            builder.HasOne(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p=>p.RoleId);
            builder.HasOne(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId);
        }
    }

}
