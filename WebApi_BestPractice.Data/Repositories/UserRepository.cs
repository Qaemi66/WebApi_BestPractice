using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Common.Exceptions;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Domain.Entities;

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
        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
    }
}
