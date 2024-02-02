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

        public async Task<UserTestEntity> GetUserTestAsync(string userId, int testId)
        {
            return await _dbContext.UserTests.FirstOrDefaultAsync(h => h.UserId == userId && h.TestId == testId);
        }

        public async Task<IEnumerable<UserTestEntity>> GetUserTestsAsync(string userId, bool isTestComleted)
        {
            return await _dbContext.UserTests.Where(h => h.UserId == userId && h.IsTestCompleted == isTestComleted).ToListAsync();
        }

        public async Task UpdateUserTestAsync(UserTestEntity userTest)
        {
            _dbContext.UserTests.Update(userTest);
            await _dbContext.SaveChangesAsync();
        }
    }
}
