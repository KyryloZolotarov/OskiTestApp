using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services;

namespace TestCatalog.Tests.Services;

public class QuestionServiceTests
{
    [Fact]
    public async Task AddQuestionAsync_Successfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var questionDtoSuccess = new AddQuestionRequest
        {
            TestId = 1,
            Question = "Test",
            Test = new TestDto
            {
                Id = 1,
                Description = "Test",
                Name = "Test"
            }
        };

        var questionEntitySuccess = new QuestionEntity
        {
            TestId = 1,
            Question = "Test",
            Test = new TestEntity
            {
                Id = 1,
                Description = "Test",
                Name = "Test"
            }
        };

        questionRepositoryMock.Setup(h => h.AddQuestionAsync(It.IsAny<QuestionEntity>())).Returns(Task.CompletedTask);

        var questionService = new QuestionService(
            questionRepositoryMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        await questionService.AddQuestionAsync(questionDtoSuccess);

        questionRepositoryMock.Verify(r => r.AddQuestionAsync(It.IsAny<QuestionEntity>()), Times.Once);
    }

    [Fact]
    public async Task AddQuestionAsync_Failed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var questionDtoFailure = new AddQuestionRequest();

        var questionService = new QuestionService(
            questionRepositoryMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        await questionService.AddQuestionAsync(questionDtoFailure);

        // Перевірка, що метод AddQuestionAsync не викликаний жодного разу
        questionRepositoryMock.Verify(r => r.AddQuestionAsync(It.IsAny<QuestionEntity>()), Times.Never);
    }

    [Fact]
    public async Task UpdateQuestionAsync_ValidQuestion_UpdatesQuestion()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        // Arrange

        var existingQuestionId = 1;
        var existingQuestion = new QuestionEntity { Id = existingQuestionId, Question = "Existing question", TestId = 1 };

        var updatedQuestion = new UpdateQuestionRequest { Id = existingQuestionId, Question = "Updated question", TestId = 2 };

        questionRepositoryMock.Setup(repo => repo.GetQuestionAsync(existingQuestionId))
            .ReturnsAsync(existingQuestion);

        questionRepositoryMock.Setup(h => h.UpdateQuestionAsync(It.IsAny<QuestionEntity>())).Returns(Task.CompletedTask);

        var questionService = new QuestionService(
            questionRepositoryMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act
        await questionService.UpdateQuestionAsync(updatedQuestion);

        // Assert
        questionRepositoryMock.Verify(repo => repo.UpdateQuestionAsync(It.IsAny<QuestionEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateQuestionAsync_NonexistentQuestion_ThrowsBusinessException()
    {
        // Arrange
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var nonExistentQuestionId = 999;
        var updatedQuestion = new UpdateQuestionRequest { Id = nonExistentQuestionId, Question = "Updated question", TestId = 2 };

        questionRepositoryMock.Setup(repo => repo.GetQuestionAsync(nonExistentQuestionId))
            .ReturnsAsync((QuestionEntity)null); // Simulate non-existent question

        var questionService = new QuestionService(
            questionRepositoryMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<BusinessException>(async () => await questionService.UpdateQuestionAsync(updatedQuestion));
    }

    [Fact]
    public async Task DeleteQuestionAsync_ValidQuestion_UpdatesQuestion()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        // Arrange

        var existingQuestionId = 1;
        var existingQuestion = new QuestionEntity { Id = existingQuestionId, Question = "Existing question", TestId = 1 };

        var updatedQuestion = new UpdateQuestionRequest { Id = existingQuestionId, Question = "Updated question", TestId = 2 };

        questionRepositoryMock.Setup(repo => repo.GetQuestionAsync(existingQuestionId))
            .ReturnsAsync(existingQuestion);

        questionRepositoryMock.Setup(h => h.DeleteQuestionAsync(It.IsAny<QuestionEntity>())).Returns(Task.CompletedTask);

        var questionService = new QuestionService(
            questionRepositoryMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act
        await questionService.DeleteQuestionAsync(existingQuestionId);

        // Assert
        questionRepositoryMock.Verify(repo => repo.DeleteQuestionAsync(It.IsAny<QuestionEntity>()), Times.Once);
    }

    [Fact]
    public async Task DeleteQuestionAsync_NonexistentQuestion_ThrowsBusinessException()
    {
        // Arrange
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<QuestionService>>();
        var questionRepositoryMock = new Mock<IQuestionRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        var nonExistentQuestionId = 999;
        var updatedQuestion = new UpdateQuestionRequest { Id = nonExistentQuestionId, Question = "Updated question", TestId = 2 };

        questionRepositoryMock.Setup(repo => repo.GetQuestionAsync(nonExistentQuestionId))
            .ReturnsAsync((QuestionEntity)null); // Simulate non-existent question

        var questionService = new QuestionService(
            questionRepositoryMock.Object,
            dbContextWrapperMock.Object,
            loggerMock.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<BusinessException>(async () => await questionService.DeleteQuestionAsync(nonExistentQuestionId));
    }
}