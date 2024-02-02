using UserProfiles.Host.Models.Dtos;
using UserProfiles.Host.Models.Requests;

namespace UserProfiles.Host.Services.Interfaces
{
    public interface IUserManageService
    {
        Task AddUserAsync(AddUserRequest user);
        Task UpdateUserAsync(UpdateUserRequest user);
        Task DeleteUserAsync(string userId);
    }
}
