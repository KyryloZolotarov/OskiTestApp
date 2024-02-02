using UserTest.Host.Models.Dtos;
using UserTest.Host.Models.Requests;

namespace UserTest.Host.Services.Interfaces
{
    public interface IUserTestManageService
    {
        Task AddUserTestAsync(AddUserTestRequest userTest);
        Task UpdateUserTestAsync(UpdateUserTestRequest userTest);
        Task DeleteUserTestAsync(int userTestId);
    }
}
