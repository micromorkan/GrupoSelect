using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace GrupoSelect.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GSDbContext _dbContext;

        public UserRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Authenticate(User filter)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == filter.Login && x.Password == filter.Password);
        }
    }
}
