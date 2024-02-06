using Web.Server.Models;
using Web.Server.Models.Dtos;
using Web.Server.Models.Requests;

namespace Web.Server.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserDto> LoginAsync(LoginDto login);
    Task<UserDto> SignUpAsync(AddUserRequest user);
}