using Web.Server.Models.Dtos;
using Web.Server.Models.Requests;

namespace Web.Server.Repositories.Interfaces;

public interface ITestRepository
{
    Task<TestDto> GetSelectedTestAsync(int testId);
    Task<TestsNamesDto> GetTestNamesAsync(TestsNamesRequest testsIds);
}