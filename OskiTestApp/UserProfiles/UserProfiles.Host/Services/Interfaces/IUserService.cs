using UserProfiles.Host.Models.Dtos;
using UserProfiles.Host.Models.Requests;

namespace UserProfiles.Host.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserRequest user);
        Task UpdateUserAsync(UpdateUserRequest user);
        Task DeleteUserAsync(string userId);
    }
}
