using Web.Server.Repositories.Interfaces;
using Web.Server.Services.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IUserTestRepository _userTestRepository;

        public TestService(ITestRepository testRepository,
            IUserTestRepository userTestRepository)
        {
            _testRepository = testRepository;
            _userTestRepository = userTestRepository;
        }

        public async Task<IEnumerable<TestViewModel>> GetAvailableTests(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestViewModel>> GetPassedTests(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TestViewModel> GetSelectedTest(int testId)
        {
            throw new NotImplementedException();
        }
    }
}
