using Web.Server.Models;
using Web.Server.Models.Requests;
using Web.Server.ViewModels;

namespace Web.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> LoginAsync(LoginViewModel login);
        Task<UserViewModel> SignUpAsync(AddUserRequest user);
    }
}
