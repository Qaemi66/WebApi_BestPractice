using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Common.Exceptions;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Domain.Entities;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Role>> GetRoleAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
                return null;

            var roles = await TableNoTracking.Where(p => p.UserId == user.Id).Select(p => p.Role).ToListAsync();

            return roles;
        }
    }
}
