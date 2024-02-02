using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserTest.Host.Data;
using UserTest.Host.Data.Entities;
using UserTest.Host.Repositories.Interfaces;

namespace UserTest.Host.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserTestRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task AddUserTestAsync(UserTestEntity userTest)
        {
            await _dbContext.UserTests.AddAsync(userTest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserTestAsync(UserTestEntity userTest)
        {
            _dbContext.UserTests.Remove(userTest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserTestEntity> GetUserTestAsync(int userTestId)
        {
            return await _dbContext.UserTests.FirstOrDefaultAsync(h => h.Id == userTestId);
        }

        public async Task UpdateUserTestAsync(UserTestEntity userTest)
        {
            _dbContext.UserTests.Update(userTest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
