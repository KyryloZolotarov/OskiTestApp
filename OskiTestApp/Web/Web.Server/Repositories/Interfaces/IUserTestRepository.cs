using Web.Server.ViewModels;

namespace Web.Server.Repositories.Interfaces
{
    public interface IUserTestRepository
    {
        Task<TestViewModel> GetSelectedTest(int testId);
    }
}
