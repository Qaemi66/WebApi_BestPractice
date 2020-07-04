using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Contracts
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserByUserPassAsync(string userName, string PasswordHash, CancellationToken cancellationToken);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
    }
}