using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestCatalog.Host.Controllers;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Models.Responses;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Tests.Controllers;

public class TestControllerTests
{
    [Fact]
    public async Task GetTestAsync_ReturnesTestResponse_Succesfully()
    {
        var testServiceMock = new Mock<ITestService>();
        var testIdMock = 1;

        var testResponseSucces = new TestResponse
        {
            Name = "Test",
            Description = "Test",
            Questions = new List<QuestionDto>()
        };

        testServiceMock.Setup(h => h.GetTestAsync(It.IsAny<int>())).ReturnsAsync(testResponseSucces);

        var testController = new TestController(
            testServiceMock.Object);

        var result = await testController.GetTestAsync(testIdMock);
        Assert.NotNull(result);

        testServiceMock.Verify(x => x.GetTestAsync(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetDogsAsync_ThrowsException_Failed()
    {
        var testServiceMock = new Mock<ITestService>();
        var testIdMock = 1;

        var testResponseSucces = new TestResponse();

        testServiceMock.Setup(h => h.GetTestAsync(It.IsAny<int>())).Throws<Exception>();

        var testController = new TestController(
            testServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await testController.GetTestAsync(testIdMock);
        });
    }


    [Fact]
    public async Task AddTestAsync_ReturnesStatusCodeOk_Succesfully()
    {
        var testServiceMock = new Mock<ITestService>();

        var testDtoSucces = new AddTestRequest
        {
            Name = "Test",
            Description = "Test"
        };

        testServiceMock.Setup(h => h.AddTestAsync(It.IsAny<AddTestRequest>())).Returns(Task.CompletedTask);

        var testController = new TestController(
            testServiceMock.Object);

        var result = await testController.AddTestAsync(testDtoSucces);
        Assert.Equal((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

        testServiceMock.Verify(x => x.AddTestAsync(It.IsAny<AddTestRequest>()), Times.Once());
    }

    [Fact]
    public async Task AddTestAsync_ThrowsException_Failed()
    {
        var testServiceMock = new Mock<ITestService>();

        var testDtoSucces = new AddTestRequest
        {
            Name = "Test",
            Description = "Test"
        };

        testServiceMock.Setup(h => h.AddTestAsync(It.IsAny<AddTestRequest>())).Throws<Exception>();

        var testController = new TestController(
            testServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await testController.AddTestAsync(testDtoSucces);
        });
    }

    [Fact]
    public async Task UpdateTestAsync_ReturnesStatusCodeOk_Succesfully()
    {
        var testServiceMock = new Mock<ITestService>();

        var testDtoSucces = new UpdateTestRequest
        {
            Name = "Test",
            Description = "Test"
        };

        testServiceMock.Setup(h => h.AddTestAsync(It.IsAny<AddTestRequest>())).Returns(Task.CompletedTask);

        var testController = new TestController(
            testServiceMock.Object);

        var result = await testController.UpdateTestAsync(testDtoSucces);
        Assert.Equal((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

        testServiceMock.Verify(x => x.UpdateTestAsync(It.IsAny<UpdateTestRequest>()), Times.Once());
    }

    [Fact]
    public async Task UpdateTestAsync_ThrowsException_Failed()
    {
        var testServiceMock = new Mock<ITestService>();

        var testDtoSucces = new UpdateTestRequest
        {
            Name = "Test",
            Description = "Test"
        };

        testServiceMock.Setup(h => h.UpdateTestAsync(It.IsAny<UpdateTestRequest>())).Throws<Exception>();

        var testController = new TestController(
            testServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await testController.UpdateTestAsync(testDtoSucces);
        });
    }

    [Fact]
    public async Task DeleteTestAsync_ReturnesStatusCodeOk_Succesfully()
    {
        var testServiceMock = new Mock<ITestService>();
        var testIdMock = 1;

        testServiceMock.Setup(h => h.DeleteTestAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var testController = new TestController(
            testServiceMock.Object);

        var result = await testController.DeleteTestAsync(testIdMock);
        Assert.Equal((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

        testServiceMock.Verify(x => x.DeleteTestAsync(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task DeleteTestAsync_ThrowsException_Failed()
    {
        var testServiceMock = new Mock<ITestService>();
        var testIdMock = 1;

        testServiceMock.Setup(h => h.DeleteTestAsync(It.IsAny<int>())).Throws<Exception>();

        var testController = new TestController(
            testServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await testController.DeleteTestAsync(testIdMock);
        });
    }

    [Fact]
    public async Task GetTestsNamesAsync_ReturnesTestResponse_Succesfully()
    {
        var testServiceMock = new Mock<ITestService>();

        var testNamesRequestMock = new TestsNamesRequest { TestIds = new List<int>() };
        testNamesRequestMock.TestIds.Add(1);
        testNamesRequestMock.TestIds.Add(2);
        testNamesRequestMock.TestIds.Add(3);

        var testNamesResponseMock = new TestsNamesResponse { Names = new Dictionary<int, string>() };
        testNamesResponseMock.Names.Add(1, "Test");
        testNamesResponseMock.Names.Add(2, "Test1");
        testNamesResponseMock.Names.Add(3, "Test3");

        testServiceMock.Setup(h => h.GetTestsNamesAsync(It.IsAny<TestsNamesRequest>()))
            .ReturnsAsync(testNamesResponseMock);

        var testController = new TestController(
            testServiceMock.Object);

        var result = await testController.GetTestsNamesAsync(testNamesRequestMock);
        Assert.NotNull(result);

        testServiceMock.Verify(x => x.GetTestsNamesAsync(It.IsAny<TestsNamesRequest>()), Times.Once());
    }

    [Fact]
    public async Task GetTestsNamesAsync_ThrowsException_Failed()
    {
        var testServiceMock = new Mock<ITestService>();

        var testNamesRequestMock = new TestsNamesRequest { TestIds = new List<int>() };
        testNamesRequestMock.TestIds.Add(1);
        testNamesRequestMock.TestIds.Add(2);
        testNamesRequestMock.TestIds.Add(3);

        var testNamesResponseMock = new TestsNamesResponse { Names = new Dictionary<int, string>() };
        testNamesResponseMock.Names.Add(1, "Test");
        testNamesResponseMock.Names.Add(2, "Test1");
        testNamesResponseMock.Names.Add(3, "Test3");

        testServiceMock.Setup(h => h.GetTestsNamesAsync(It.IsAny<TestsNamesRequest>())).Throws<Exception>();

        var testController = new TestController(
            testServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await testController.GetTestsNamesAsync(testNamesRequestMock);
        });
    }
}