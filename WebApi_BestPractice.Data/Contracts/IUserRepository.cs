﻿using System.Threading;
using System.Threading.Tasks;
using WebApi_BestPractice.Domain.Etities;

namespace WebApi_BestPractice.Data.Contracts
{
    public interface IUserRepository:IRepository<User>
    {
        ApplicationDbContext dbContext { get; }

        Task<User> GetUserByUserPassAsync(string userName, string PasswordHash, CancellationToken cancellationToken);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task<User> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}