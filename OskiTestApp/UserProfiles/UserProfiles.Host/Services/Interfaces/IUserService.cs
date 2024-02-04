using UserProfiles.Host.Models.Dtos;
using UserProfiles.Host.Models.Requests;

namespace UserProfiles.Host.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(AddUserRequest user);
        Task UpdateUserAsync(UpdateUserRequest user);
        Task DeleteUserAsync(string userId);
        Task<UserDto> LoginAsynnc(LoginRequest login);
    }
}
