using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Domain.Entities;

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

            var userRole = new IdentityUserRole<int>()
            {
                RoleId = user.Id,
                UserId = role.Id
            };

            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Role>().HasData(role);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(userRole);
        }
    }
}
