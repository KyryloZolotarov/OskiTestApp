using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Models.Responses;

namespace TestCatalog.Host.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task AddTestAsync(TestEntity test);
        Task UpdateTestAsync(TestEntity test);
        Task DeleteTestAsync(TestEntity test);
        Task<TestEntity> GetTestAsync(int testId);
        Task<IEnumerable<TestEntity>> GetTestsNamesAsync(TestsNamesRequest testsIds);
    }
}
