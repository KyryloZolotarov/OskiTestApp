using AutoMapper;
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
        private readonly IMapper _mapper;

        public TestService(ITestRepository testRepository,
            IMapper mapper,
            IUserTestRepository userTestRepository)
        {
            _mapper = mapper;
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

        public async Task<IEnumerable<PassedTestViewModel>> GetPassedTests(string userId)
        {
            var availableTests = await _userTestRepository.GetPassedTestsAsync(userId);
            var listTestIds = new TestsNamesRequest() { TestIds = new List<int>() };
            foreach (var test in availableTests)
            {
                listTestIds.TestIds.Add(test.TestId);
            }
            var result = await _testRepository.GetTestNamesAsync(listTestIds);
            var resultWithNames = new List<PassedTestViewModel>();
            foreach (var name in result.Names)
            {
                resultWithNames.Add(new PassedTestViewModel() { Name = name.Value, Id = name.Key });
            }
            foreach(var test in availableTests)
            {
                resultWithNames.First(s =>s.Id == test.TestId).Mark = test.Mark;
            }
            return resultWithNames;
        }

        public async Task<TestViewModel> GetSelectedTest(int testId)
        {
            var test = await _testRepository.GetSelectedTestAsync(testId);
            var testView = new TestViewModel()
            {
                Name = test.Name,
                Id = test.Id,
                Description = test.Description,
                Questions = new List<QuestionViewModel>()
            };
            testView.Questions = test.Questions.Select(s => _mapper.Map<QuestionViewModel>(s)).ToList();
            return testView;
        }
    }
}
