using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task AddTestAsync(TestEntity test);
        Task UpdateTestAsync(TestEntity test);
        Task DeleteTestAsync(TestEntity test);
        Task<TestEntity> GetTestAsync(int testId);
    }
}
