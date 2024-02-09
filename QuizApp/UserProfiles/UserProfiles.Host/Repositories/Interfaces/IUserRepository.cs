using UserProfiles.Host.Data.Entities;

namespace UserProfiles.Host.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> AddUserAsync(UserEntity user);
    Task UpdateUserAsync(UserEntity user);
    Task DeleteUserAsync(UserEntity user);
    Task<UserEntity> GetUserAsync(string userId);
    Task<UserEntity> GetUserByEmailAsync(string email);
}