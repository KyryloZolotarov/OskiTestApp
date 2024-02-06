using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Models.Responses;

namespace TestCatalog.Host.Services.Interfaces;

public interface ITestService
{
    Task AddTestAsync(AddTestRequest test);
    Task UpdateTestAsync(UpdateTestRequest test);
    Task DeleteTestAsync(int testId);
    Task<TestResponse> GetTestAsync(int testId);
    Task<TestsNamesResponse> GetTestsNamesAsync(TestsNamesRequest testsIds);
}