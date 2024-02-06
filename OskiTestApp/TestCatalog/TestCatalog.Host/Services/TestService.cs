using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Models.Responses;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Services;

public class TestService : BaseDataService<ApplicationDbContext>, ITestService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;
    private readonly ITestRepository _testRepository;

    public TestService(IQuestionRepository questionRepository,
        IAnswerRepository answerRepository,
        ITestRepository testRepository,
        IMapper mapper,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger)
        : base(dbContextWrapper, logger)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _mapper = mapper;
        _testRepository = testRepository;
    }

    public async Task AddTestAsync(AddTestRequest test)
    {
        await ExecuteSafeAsync(async () =>
        {
            var testAdd = new TestEntity
            {
                Description = test.Description,
                Name = test.Name
            };
            await _testRepository.AddTestAsync(testAdd);
        });
    }

    public async Task DeleteTestAsync(int testId)
    {
        var testExists = await ExecuteSafeAsync(async () => await _testRepository.GetTestAsync(testId));

        if (testExists == null) throw new BusinessException($"Test with id: {testId} not found");

        await ExecuteSafeAsync(async () => { await _testRepository.DeleteTestAsync(testExists); });
    }

    public async Task<TestResponse> GetTestAsync(int testId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var test = await _testRepository.GetTestAsync(testId);
            var questions = await _questionRepository.GetQuestionsForTestAsync(testId);
            var answers = await _answerRepository.GetAnswersForTestAsync(testId);
            var mappedQuestion = questions.Select(s => _mapper.Map<QuestionDto>(s)).ToList();
            var mappedAnswers = answers.Select(s => _mapper.Map<AnswerDto>(s)).ToList();
            var fullTest = new TestResponse
                { Id = test.Id, Description = test.Description, Name = test.Name, Questions = new List<QuestionDto>() };
            fullTest.Questions.AddRange(mappedQuestion);
            foreach (var question in fullTest.Questions)
                question.AnswerVariants = new Dictionary<int, string>(mappedAnswers
                    .Where(x => x.QuestionId == question.Id)
                    .Select(y => new KeyValuePair<int, string>(y.Id, y.Answer)));
            return fullTest;
        });
    }

    public async Task<TestsNamesResponse> GetTestsNamesAsync(TestsNamesRequest testsIds)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _testRepository.GetTestsNamesAsync(testsIds);
            var testsNames = new TestsNamesResponse { Names = new Dictionary<int, string>() };
            foreach (var test in result) testsNames.Names.Add(test.Id, test.Name);
            return testsNames;
        });
    }

    public async Task UpdateTestAsync(UpdateTestRequest test)
    {
        var testExists = await ExecuteSafeAsync(async () => await _testRepository.GetTestAsync(test.Id));

        if (testExists == null) throw new BusinessException($"Test with id: {test.Id} not found");

        if (test.Name != null) testExists.Name = test.Name;

        if (test.Description != null) testExists.Description = test.Description;

        await ExecuteSafeAsync(async () => { await _testRepository.UpdateTestAsync(testExists); });
    }
}