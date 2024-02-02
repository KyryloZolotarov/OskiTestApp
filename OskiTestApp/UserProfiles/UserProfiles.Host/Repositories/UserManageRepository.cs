using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserProfiles.Host.Data;
using UserProfiles.Host.Data.Entities;
using UserProfiles.Host.Repositories.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace UserProfiles.Host.Repositories
{
    public class UserManageRepository : IUserManageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserManageRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(UserEntity user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserAsync(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(h => h.Id == userId);
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
