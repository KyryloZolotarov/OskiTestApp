using Web.Server.Models.Requests;
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

        public async Task<TestsNamesViewModel> GetAvailableTests(string userId)
        {
            var availableTests = await _userTestRepository.GetAvailableTestsAsync(userId);
            var listTestIds = new TestsNamesRequest() { TestIds = new List<int>() };
            foreach(var test in  availableTests)
            {
                listTestIds.TestIds.Add(test.TestId);
            }
            var result = await _testRepository.GetTestNamesAsync(listTestIds);
            var listTestIdsWithNames = new TestsNamesViewModel() { Names = new Dictionary<int, string>() };
            foreach(var name in  result.Names)
            {
                listTestIdsWithNames.Names.Add(name.Key, name.Value);
            }
            return listTestIdsWithNames;
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
