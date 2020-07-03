using Common;
using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByUserPassAsync(string userName, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return await TableNoTracking.SingleOrDefaultAsync(p => p.UserName == userName && p.PasswordHash == passwordHash, cancellationToken);
        }
    }
}
