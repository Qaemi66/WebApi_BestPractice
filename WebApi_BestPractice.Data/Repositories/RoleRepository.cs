using WebApi_BestPractice.Domain.Entities;

namespace WebApi_BestPractice.Data.Repositories
{
    public class RoleRepository : Repository<Role> 
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
