using AutoMapper;
using Web.Server.Models.Dtos;
using Web.Server.Models.Requests;
using Web.Server.Repositories.Interfaces;
using Web.Server.Services.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Services;

public class TestService : ITestService
{
    private readonly IMapper _mapper;
    private readonly ITestRepository _testRepository;
    private readonly IUserTestRepository _userTestRepository;

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
        var listTestIds = new TestsNamesRequest { TestIds = new List<int>() };
        foreach (var test in availableTests) listTestIds.TestIds.Add(test.TestId);
        var result = await _testRepository.GetTestNamesAsync(listTestIds);
        var listTestIdsWithNames = new TestsNamesViewModel { Names = new Dictionary<int, string>() };
        foreach (var name in result.Names) listTestIdsWithNames.Names.Add(name.Key, name.Value);
        return listTestIdsWithNames;
    }

    public async Task<IEnumerable<PassedTestViewModel>> GetPassedTests(string userId)
    {
        var availableTests = await _userTestRepository.GetPassedTestsAsync(userId);
        var listTestIds = new TestsNamesRequest { TestIds = new List<int>() };
        foreach (var test in availableTests) listTestIds.TestIds.Add(test.TestId);
        var result = await _testRepository.GetTestNamesAsync(listTestIds);
        var resultWithNames = new List<PassedTestViewModel>();
        foreach (var name in result.Names)
            resultWithNames.Add(new PassedTestViewModel { Name = name.Value, Id = name.Key });
        foreach (var test in availableTests) resultWithNames.First(s => s.Id == test.TestId).Mark = test.Mark;
        return resultWithNames;
    }

    public async Task<TestViewModel> GetSelectedTest(int testId)
    {
        var test = await _testRepository.GetSelectedTestAsync(testId);
        var testView = new TestViewModel
        {
            Name = test.Name,
            Id = test.Id,
            Description = test.Description,
            Questions = new List<QuestionViewModel>()
        };
        testView.Questions.AddRange(test.Questions.Select(s => new QuestionViewModel
        {
            Id = s.Id,
            Question = s.Question,
            AnswerVariants = s.AnswerVariants,
            CorrectAnswersCount = 0
        }));
        return testView;
    }

    public async Task SubmitAnswersAsync(string userId, UserTestViewModel complitedTest)
    {
        var test = await _testRepository.GetSelectedTestAsync(complitedTest.TestId);
        var userTestAdd = new UserTestDto();
        userTestAdd.UserId = userId;
        userTestAdd.TestId = complitedTest.TestId;
        var answersDictionary = new Dictionary<int, List<int>>(complitedTest.Answers.Select(l => new KeyValuePair<int, List<int>>(l.QuiestionId, l.AnswersKeys)));
        var answersCount = 0;
        var correctAnswersCount = 0;
        test.QuestionsCorrectAnswers = new Dictionary<int, List<int>>();
        foreach(var question in test.Questions)
        {
            test.QuestionsCorrectAnswers.Add(question.Id, question.CorrectAnswers);
        }
        foreach (var question in test.QuestionsCorrectAnswers)
        {
            answersCount += question.Value.Count();
            if(answersDictionary.TryGetValue(question.Key, out var answer))
            {
                 correctAnswersCount += question.Value.Intersect(answer).Count();
            }
        }
        float temp = ((float) correctAnswersCount / (float) answersCount);
        temp = temp * 100;
        userTestAdd.Mark = (int)temp;
        userTestAdd.IsTestCompleted = true;

        await _userTestRepository.SubmitAnswersAsync(userTestAdd);
    }
}