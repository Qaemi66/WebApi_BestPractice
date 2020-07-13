using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using WebApi_BestPractice.Domain.BaseClasses;

namespace WebApi_BestPractice.Domain.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            this.IsActive = true;
        }

        public string FullName { get; set; }
        public int Age { get; set; }
        public Enums.GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }
        
        public ICollection<Post> Posts { get; set; }
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
