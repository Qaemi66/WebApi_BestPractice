using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Repositories
{
    public interface IUserRoleRepository
    {
        Task<ICollection<Role>> GetRoleAsync(User user, CancellationToken cancellationToken);
    }
}