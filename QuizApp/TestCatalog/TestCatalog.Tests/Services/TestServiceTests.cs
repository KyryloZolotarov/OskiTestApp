using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Models.Responses;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services;

namespace TestCatalog.Tests.Services;

public class TestServiceTests
{
    [Fact]
    public async Task AddTestAsync_Successfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<TestService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();


        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var testDtoSuccess = new AddTestRequest()
        {
            Name = "Test",
            Description = "Test",
        };

        var testEntitySuccess = new TestEntity
        {
            Name = "Test",
            Description = "Test",
        };

        var mapperMock = new Mock<IMapper>();

        testRepositoryMock.Setup(h => h.AddTestAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        await testService.AddTestAsync(testDtoSuccess);

        testRepositoryMock.Verify(r => r.AddTestAsync(It.IsAny<TestEntity>()), Times.Once);
    }

    [Fact]
    public async Task AddTestAsync_Failed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ThrowsAsync(new Exception());

        var testDtoFailure = new AddTestRequest();

        testRepositoryMock.Setup(repo => repo.AddTestAsync(It.IsAny<TestEntity>()))
            .Throws(new Exception("Failed to add test")); // Заміна ThrowsAsync на Throws

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Перевірка, що метод AddTestAsync викликає виняток
        await Assert.ThrowsAsync<Exception>(async () => await testService.AddTestAsync(testDtoFailure));
    }

    [Fact]
    public async Task UpdateTestAsync_ValidQuestion_UpdatesQuestion()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        // Arrange

        var existingTestId = 1;
        var existingTest = new TestEntity { Id = existingTestId, Name = "Existing"};

        var updatedTest = new UpdateTestRequest { Id = existingTestId, Name = "Existing" };

        testRepositoryMock.Setup(repo => repo.GetTestAsync(existingTestId))
            .ReturnsAsync(existingTest);

        testRepositoryMock.Setup(h => h.UpdateTestAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act
        await testService.UpdateTestAsync(updatedTest);

        // Assert
        testRepositoryMock.Verify(repo => repo.UpdateTestAsync(It.IsAny<TestEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTestAsync_NonexistentQuestion_ThrowsBusinessException()
    {
        // Arrange
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var nonExistentTestId = 999;
        var updatedTest = new UpdateTestRequest { Id = nonExistentTestId, Name = "Updated question" };

        testRepositoryMock.Setup(repo => repo.GetTestAsync(nonExistentTestId))
            .ReturnsAsync((TestEntity)null); // Simulate non-existent question

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<BusinessException>(async () => await testService.UpdateTestAsync(updatedTest));
    }

    [Fact]
    public async Task DeleteTestAsync_ValidQuestion_DeletesTest()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        // Arrange

        var existingTestId = 1;
        var existingTest = new TestEntity { Id = existingTestId, Name = "Existing" };

        testRepositoryMock.Setup(repo => repo.GetTestAsync(existingTestId))
            .ReturnsAsync(existingTest);

        testRepositoryMock.Setup(h => h.DeleteTestAsync(It.IsAny<TestEntity>())).Returns(Task.CompletedTask);

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act
        await testService.DeleteTestAsync(existingTestId);

        // Assert
        testRepositoryMock.Verify(repo => repo.DeleteTestAsync(existingTest), Times.Once);
    }

    [Fact]
    public async Task DeleteTestAsync_NonexistentQuestion_ThrowsBusinessException()
    {
        // Arrange
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var nonExistentTestId = 999;
        var updatedTest = new TestEntity {Id = nonExistentTestId, Name = "Existing" };

        testRepositoryMock.Setup(repo => repo.GetTestAsync(nonExistentTestId))
            .ReturnsAsync((TestEntity)null); // Simulate non-existent question

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<BusinessException>(async () => await testService.DeleteTestAsync(nonExistentTestId));
    }

    [Fact]
    public async Task GetTestAsync_ReturnsFullTest()
    {
        // Arrange
        var testId = 1;
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var testEntity = new TestEntity { Id = testId, Name = "Test", Description = "Description" };
        var questions = new List<QuestionEntity>
    {
        new QuestionEntity { Id = 1, TestId = testId },
        new QuestionEntity { Id = 2, TestId = testId }
    };
        var answers = new List<AnswerEntity>
    {
        new AnswerEntity { Id = 1, QuestionId = 1, Answer = "Answer 1", isCorrect = true },
        new AnswerEntity { Id = 2, QuestionId = 1, Answer = "Answer 2", isCorrect = false },
        new AnswerEntity { Id = 3, QuestionId = 2, Answer = "Answer 3", isCorrect = true },
        new AnswerEntity { Id = 4, QuestionId = 2, Answer = "Answer 4", isCorrect = false }
    };

        testRepositoryMock.Setup(repo => repo.GetTestAsync(testId)).ReturnsAsync(testEntity);
        questionRepositoryMock.Setup(repo => repo.GetQuestionsForTestAsync(testId)).ReturnsAsync(questions);
        answerRepositoryMock.Setup(repo => repo.GetAnswersForTestAsync(testId)).ReturnsAsync(answers);

        var expectedFullTest = new TestResponse
        {
            Id = testEntity.Id,
            Name = testEntity.Name,
            Description = testEntity.Description,
            Questions = questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                TestId = q.TestId,
                AnswerVariants = answers
                    .Where(a => a.QuestionId == q.Id)
                    .ToDictionary(a => a.Id, a => a.Answer),
                CorrectAnswers = answers
                    .Where(a => a.QuestionId == q.Id && a.isCorrect)
                    .Select(a => a.Id)
                    .ToList()
            }).ToList()
        };

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act
        var result = await testService.GetTestAsync(testId);
        testRepositoryMock.Verify(repo => repo.GetTestAsync(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetTestAsync_EmptyTest_ReturnsNull()
    {
        // Arrange
        var testId = 1;
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<TestService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();
        var answerRepositoryMock = new Mock<IAnswerRepository>();
        var testRepositoryMock = new Mock<ITestRepository>();
        var mapperMock = new Mock<IMapper>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        testRepositoryMock.Setup(repo => repo.GetTestAsync(testId)).ReturnsAsync((TestEntity)null);

        var testService = new TestService(
            questionRepositoryMock.Object,
            answerRepositoryMock.Object,
            testRepositoryMock.Object,
            mapperMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act
        var result = await testService.GetTestAsync(testId);

        // Assert
        Assert.Null(result);
    }
}