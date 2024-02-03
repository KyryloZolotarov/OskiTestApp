using Web.Server.Models.Dtos;
using Web.Server.Models.Requests;
using Web.Server.ViewModels;

namespace Web.Server.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task<TestDto> GetSelectedTestAsync(int testId);
        Task<TestsNamesDto> GetTestNamesAsync(TestsNamesRequest testsIds);
    }
}
