using UserTest.Host.Data.Entities;
using UserTest.Host.Models.Dtos;

namespace UserTest.Host.Repositories.Interfaces
{
    public interface IUserTestRepository
    {
        Task AddUserTestAsync(UserTestEntity userTest);
        Task UpdateUserTestAsync(UserTestEntity userTest);
        Task DeleteUserTestAsync(UserTestEntity userTest);
        Task<UserTestEntity> GetUserTestAsync(string userId, int testId);
        Task<IEnumerable<UserTestEntity>> GetUserTestsAsync(string userId, bool isTestComleted);
    }
}
