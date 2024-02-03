using Microsoft.AspNetCore.Mvc;
using Web.Server.ViewModels;

namespace Web.Server.Services.Interfaces
{
    public interface ITestService
    {
        Task<TestsNamesViewModel> GetAvailableTests(string userId);
        Task<TestViewModel> GetSelectedTest(int testId);
        Task<IEnumerable<PassedTestViewModel>> GetPassedTests(string userId);
    }
}
