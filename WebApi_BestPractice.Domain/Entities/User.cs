using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Domain.Etities
{
    public class User : BaseClasses.BaseEntity
    {
        public User()
        {
            this.IsActive = true;
        }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public Enums.GenderType Gender { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset LastLoginDate { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Claim> Claims { get; set; }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(500);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(100);
        }
    }
}
