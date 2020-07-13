using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Data.Contracts
{
    public interface IUserRepository:IRepository<User>
    {
        ApplicationDbContext dbContext { get; }
        Task<User> GetUserByUserPassAsync(string userName, string PasswordHash, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}