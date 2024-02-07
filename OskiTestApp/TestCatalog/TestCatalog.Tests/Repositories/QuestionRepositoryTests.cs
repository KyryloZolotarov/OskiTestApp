using AutoMapper;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Repositories;

namespace TestCatalog.Tests.Repositories;

public class QuestionRepositoryTests
{
    [Fact]
    public async Task AddQeustionDogAsync_Seccesfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

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

        var repository = new QuestionRepository(wrapper);
        await repository.AddQuestionAsync(questionEntitySuccess);
        var result = wrapper.DbContext.Questions.FirstOrDefault(x => x.Id == questionEntitySuccess.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddQuestionAsync_Throws_ArgumentException_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

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

        wrapper.DbContext.Questions.Add(questionEntitySuccess);
        wrapper.DbContext.SaveChanges();

        var repository = new QuestionRepository(wrapper);
        await Assert.ThrowsAsync<ArgumentException>(async () => {
            await repository.AddQuestionAsync(questionEntitySuccess);
        });
    }

    [Fact]
    public async Task UpdateQeustionDogAsync_Seccesfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

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

        var repository = new QuestionRepository(wrapper);
        await repository.AddQuestionAsync(questionEntitySuccess);

        await repository.UpdateQuestionAsync(questionEntitySuccess);
        var result = wrapper.DbContext.Questions.FirstOrDefault(x => x.Id == questionEntitySuccess.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateQuestionAsync_Throws_ArgumentException_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var questionEntityFailed = new QuestionEntity
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

        var repository = new QuestionRepository(wrapper);
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => {
            await repository.UpdateQuestionAsync(questionEntityFailed);
        });
    }

    [Fact]
    public async Task DeleteQeustionDogAsync_Seccesfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

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

        var repository = new QuestionRepository(wrapper);
        await repository.AddQuestionAsync(questionEntitySuccess);
        await repository.DeleteQuestionAsync(questionEntitySuccess);
        var result = wrapper.DbContext.Questions.FirstOrDefault(x => x.Id == questionEntitySuccess.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteQuestionAsync_Throws_ArgumentException_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var questionEntityFailed = new QuestionEntity
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

        var repository = new QuestionRepository(wrapper);
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => {
            await repository.DeleteQuestionAsync(questionEntityFailed);
        });
    }

    [Fact]
    public async Task GetQuestionsForTestAsync_EmptyResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new QuestionRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetQuestionsForTestAsync(1);


        // Assert
        Assert.Empty(result);
    }

    [Fact]
     public async Task GetQuestionsForTestAsync_ReturnsQuestions()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase2")
                    .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new QuestionRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных
        dbContextMock.Questions.Add(new QuestionEntity { Id = 1, Question = "Question", TestId = 1 });
        dbContextMock.Questions.Add(new QuestionEntity { Id = 2, Question = "Question", TestId = 1 });
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetQuestionsForTestAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // Проверка количества вопросов
    }

    [Fact]
    public async Task GetQuestionsForTestAsync_Successfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase2")
                    .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new QuestionRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных
        dbContextMock.Questions.Add(new QuestionEntity { Id = 1, Question = "Question", TestId = 1 });
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetQuestionsForTestAsync(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetQuestionsForTestAsync_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase2")
                    .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new QuestionRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetQuestionsForTestAsync(1);

        // Assert
        Assert.Empty(result);
    }
}