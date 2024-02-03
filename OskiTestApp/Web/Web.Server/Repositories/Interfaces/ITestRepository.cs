using Web.Server.Models.Dtos;
using Web.Server.ViewModels;

namespace Web.Server.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestDto>> GetAvailableTests(List<int> testsList);
        Task<IEnumerable<TestDto>> GetPassedTests(List<int> testsList);
    }
}
