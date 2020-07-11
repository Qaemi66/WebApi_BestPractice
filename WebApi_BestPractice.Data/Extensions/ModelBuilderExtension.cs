using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Domain.Entities;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = 1,
                UserName = "Admin",
                FullName = "ایمان قائمی",
                Gender = Domain.Enums.GenderType.Male,
                PasswordHash = SecurityHelper.GetSha256Hash("12345"),
                Age = 33
            };

            var role = new Role()
            {
                Id = 1,
                Name = "Admin",
                Description = "مدیر سایت"
            };

            var userRole = new UserRole()
            {
                RoleId = user.Id,
                UserId = role.Id
            };

            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Role>().HasData(role);
            modelBuilder.Entity<UserRole>().HasData(userRole);
        }
    }
}
