using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Repositories
{
    public interface IClaimRepository
    {
        Task<ICollection<Role>> GetUserClaimsAsync(User user, CancellationToken cancellationToken);
    }
}