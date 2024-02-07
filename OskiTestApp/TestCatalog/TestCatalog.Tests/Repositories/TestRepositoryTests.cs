using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Repositories;
using TestCatalog.Host.Repositories.Interfaces;

namespace TestCatalog.Tests.Repositories;

public class TestRepositoryTests
{
    [Fact]
    public async Task AddTestDogAsync_Seccesfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var testEntitySuccess = new TestEntity
        {
            Id = 1,
            Description = "Test",
            Name = "Test"
        };

        var repository = new TestRepository(wrapper);
        await repository.AddTestAsync(testEntitySuccess);
        var result = wrapper.DbContext.Tests.FirstOrDefault(x => x.Id == testEntitySuccess.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddTestAsync_Throws_ArgumentException_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var testEntityFailed = new TestEntity
        {
            Id = 1,
            Description = "Test",
            Name = "Test"
        };

        wrapper.DbContext.Tests.Add(testEntityFailed);
        wrapper.DbContext.SaveChanges();

        var repository = new TestRepository(wrapper);
        await Assert.ThrowsAsync<ArgumentException>(async () => {
            await repository.AddTestAsync(testEntityFailed);
        });
    }

    [Fact]
    public async Task UpdateTestDogAsync_Seccesfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var testEntitySuccess = new TestEntity
        {
            Id = 3,
            Description = "Test",
            Name = "Test"
        };

        var repository = new TestRepository(wrapper);
        await repository.AddTestAsync(testEntitySuccess);

        await repository.UpdateTestAsync(testEntitySuccess);
        var result = wrapper.DbContext.Tests.FirstOrDefault(x => x.Id == testEntitySuccess.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateTestAsync_Throws_ArgumentException_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var testEntityFailed = new TestEntity
        {
        };

        var repository = new TestRepository(wrapper);
        await Assert.ThrowsAsync<DbUpdateException>(async () => {
            await repository.AddTestAsync(testEntityFailed);
        });
    }

    [Fact]
    public async Task DeleteTestDogAsync_Seccesfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase1")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var testEntitySuccess = new TestEntity
        {
            Description = "Test",
            Name = "Test"
        };

        var repository = new TestRepository(wrapper);
        await repository.AddTestAsync(testEntitySuccess);
        await repository.DeleteTestAsync(testEntitySuccess);
        var result = wrapper.DbContext.Tests.FirstOrDefault(x => x.Id == testEntitySuccess.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteTestsync_Throws_ArgumentException_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);

        var wrapper = new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object);

        var testEntityFailed = new TestEntity
        {
            Description = "Test",
            Name = "Test"
        };

        var repository = new TestRepository(wrapper);
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => {
            await repository.DeleteTestAsync(testEntityFailed);
        });
    }

    [Fact]
    public async Task GetTestAsync_EmptyResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2")
            .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new TestRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных

        // Act
        var result = await repository.GetTestAsync(1);


        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetTestAsync_ReturnsTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase2")
                    .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new TestRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        var testEntitySuccess = new TestEntity
        {
            Id = 1,
            Description = "Test",
            Name = "Test"
        };
        // Вставка тестовых данных
        dbContextMock.Tests.Add(new TestEntity
        {
            Id = 1,
            Description = "Test",
            Name = "Test"
        });
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetTestAsync(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetTestNamesAsync_Successfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase2")
                    .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new TestRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных
        dbContextMock.Tests.Add(new TestEntity { Id = 1, Description = "Test", Name = "Test" });
        dbContextMock.Tests.Add(new TestEntity { Id = 2, Description = "Test", Name = "Test" });
        dbContextMock.Tests.Add(new TestEntity { Id = 3, Description = "Test", Name = "Test" });
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetTestsNamesAsync(new TestsNamesRequest() { TestIds = new List<int>() { 1, 2, 4 } });

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetTestNamesAsync_Failed()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase2")
                    .Options;
        var dbContextMock = new ApplicationDbContext(options);
        var dbContextFactoryMock = new Mock<IDbContextFactory<ApplicationDbContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(dbContextMock);
        var repository = new TestRepository(new DbContextWrapper<ApplicationDbContext>(dbContextFactoryMock.Object));

        // Вставка тестовых данных
        dbContextMock.SaveChanges();

        // Act
        var result = await repository.GetTestsNamesAsync(new TestsNamesRequest() { TestIds = new List<int>() { 1, 2, 4 } });

        // Assert
        Assert.Empty(result);
    }
}