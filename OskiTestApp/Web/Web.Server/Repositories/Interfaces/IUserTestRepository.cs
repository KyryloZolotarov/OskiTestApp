using Web.Server.Models.Dtos;
using Web.Server.ViewModels;

namespace Web.Server.Repositories.Interfaces
{
    public interface IUserTestRepository
    {
        Task<IEnumerable<UserTestDto>> GetAvailableTestsAsync(string userId);
        Task<IEnumerable<UserTestDto>> GetPassedTestsAsync(string userId);
    }
}
