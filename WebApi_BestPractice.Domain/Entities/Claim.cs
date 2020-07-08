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
    public class Claim : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }

    }

    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(p=> new { p.RoleId, p.UserId});

            builder.HasOne(p => p.Role).WithMany(p => p.Claims).HasForeignKey(p=>p.RoleId);
            builder.HasOne(p => p.User).WithMany(p => p.Claims).HasForeignKey(p => p.UserId);
        }
    }

}
