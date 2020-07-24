using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Service.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user, CancellationToken cancellationToken);
    }
}