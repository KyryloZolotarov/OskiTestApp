using UserTest.Host.Models.Dtos;
using UserTest.Host.Models.Requests;

namespace UserTest.Host.Services.Interfaces
{
    public interface IUserTestService
    {
        Task AddUserTestAsync(AddUserTestRequest userTest);
        Task UpdateUserTestAsync(UpdateUserTestRequest userTest);
        Task DeleteUserTestAsync(string userId, int testId);
        Task<IEnumerable<UserTestDto>> GetUserTestsAsync(string userId, bool isTestComleted);
    }
}
