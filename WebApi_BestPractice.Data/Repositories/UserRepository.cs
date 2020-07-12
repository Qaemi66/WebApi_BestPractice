using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Common.Exceptions;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        ApplicationDbContext IUserRepository.dbContext => DbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByUserPassAsync(string userName, string password, CancellationToken cancellationToken)
        {
            if (!userName.HasValue() || !password.HasValue())
                throw new BadRequestException("نام كاربري يا كلمه عبور وارد نشده");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return await TableNoTracking.SingleOrDefaultAsync(p => p.UserName == userName && p.PasswordHash == passwordHash, cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }

        public override void Update(User entity, bool saveNow = true)
        {
            UpdateSecurityStamp(entity);
            base.Update(entity, saveNow);
        }

        public override Task UpdateAsync(User entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            UpdateSecurityStamp(entity);
            return base.UpdateAsync(entity, cancellationToken, saveNow);
        }

        public override void UpdateRange(IEnumerable<User> entities, bool saveNow = true)
        {
            UpdateSecurityStamp(entities);
            base.UpdateRange(entities, saveNow);
        }

        public override Task UpdateRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            UpdateSecurityStamp(entities);
            return base.UpdateRangeAsync(entities, cancellationToken, saveNow);
        }

        private void UpdateSecurityStamp(User user)
        {
            user.SecurityStamp = Guid.NewGuid();
        }
      
        private void UpdateSecurityStamp(IEnumerable<User> users)
        {
            foreach (var user in users)
                user.SecurityStamp = Guid.NewGuid();
        }

        public async Task<User> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            var user = await TableNoTracking.SingleOrDefaultAsync(p => p.UserName == userName);
            return user;
        }

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }

    }
}
