using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;

namespace TestCatalog.Host.Services.Interfaces
{
    public interface ITestManageService
    {
        Task AddTestAsync(AddTestRequest test);
        Task UpdateTestAsync(UpdateTestRequest test);
        Task DeleteTestAsync(int testId);
    }
}
