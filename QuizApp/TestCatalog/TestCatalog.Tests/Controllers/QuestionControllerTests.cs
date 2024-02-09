using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestCatalog.Host.Controllers;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Tests.Controllers;

public class QuestionControllerTests
{
    [Fact]
    public async Task AddQuestionAsync_ReturnesStatusCodeOk_Succesfully()
    {
        var questionServiceMock = new Mock<IQuestionService>();

        var questionDtoSucces = new AddQuestionRequest
        {
            AnswerVariants = new Dictionary<int, string>(),
            CorrectAnswers = new List<int> { 1, 2 },
            Question = "Question"
        };

        questionServiceMock.Setup(h => h.AddQuestionAsync(It.IsAny<AddQuestionRequest>())).Returns(Task.CompletedTask);

        var questionController = new QuestionController(
            questionServiceMock.Object);

        var result = await questionController.AddQuestionAsync(questionDtoSucces);
        Assert.Equal((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

        questionServiceMock.Verify(x => x.AddQuestionAsync(It.IsAny<AddQuestionRequest>()), Times.Once());
    }

    [Fact]
    public async Task AddQuestionAsync_ThrowsException_Failed()
    {
        var questionServiceMock = new Mock<IQuestionService>();

        var questionDtoSucces = new AddQuestionRequest
        {
            AnswerVariants = new Dictionary<int, string>(),
            CorrectAnswers = new List<int> { 1, 2 },
            Question = "Question"
        };

        questionServiceMock.Setup(h => h.AddQuestionAsync(It.IsAny<AddQuestionRequest>())).Throws<Exception>();

        var questionController = new QuestionController(
            questionServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await questionController.AddQuestionAsync(questionDtoSucces);
        });
    }

    [Fact]
    public async Task UpdateQuestionAsync_ReturnesStatusCodeOk_Succesfully()
    {
        var questionServiceMock = new Mock<IQuestionService>();

        var questionDtoSucces = new UpdateQuestionRequest
        {
            AnswerVariants = new Dictionary<int, string>(),
            CorrectAnswers = new List<int> { 1, 2 },
            Question = "Question"
        };

        questionServiceMock.Setup(h => h.UpdateQuestionAsync(It.IsAny<UpdateQuestionRequest>()))
            .Returns(Task.CompletedTask);

        var questionController = new QuestionController(
            questionServiceMock.Object);

        var result = await questionController.UpdateQuestionAsync(questionDtoSucces);
        Assert.Equal((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

        questionServiceMock.Verify(x => x.UpdateQuestionAsync(It.IsAny<UpdateQuestionRequest>()), Times.Once());
    }

    [Fact]
    public async Task UpdateQuestionAsync_ThrowsException_Failed()
    {
        var questionServiceMock = new Mock<IQuestionService>();

        var questionDtoSucces = new UpdateQuestionRequest
        {
            AnswerVariants = new Dictionary<int, string>(),
            CorrectAnswers = new List<int> { 1, 2 },
            Question = "Question"
        };

        questionServiceMock.Setup(h => h.UpdateQuestionAsync(It.IsAny<UpdateQuestionRequest>())).Throws<Exception>();

        var questionController = new QuestionController(
            questionServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await questionController.UpdateQuestionAsync(questionDtoSucces);
        });
    }

    [Fact]
    public async Task DeleteQuestionAsync_ReturnesStatusCodeOk_Succesfully()
    {
        var questionServiceMock = new Mock<IQuestionService>();
        var questionIdMock = 1;

        questionServiceMock.Setup(h => h.DeleteQuestionAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var questionController = new QuestionController(
            questionServiceMock.Object);

        var result = await questionController.DeleteQuestionAsync(questionIdMock);
        Assert.Equal((int)HttpStatusCode.OK, ((OkResult)result).StatusCode);

        questionServiceMock.Verify(x => x.DeleteQuestionAsync(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task DeleteQuestionAsync_ThrowsException_Failed()
    {
        var questionServiceMock = new Mock<IQuestionService>();

        var questionIdMock = 1;

        questionServiceMock.Setup(h => h.DeleteQuestionAsync(It.IsAny<int>())).Throws<Exception>();

        var questionController = new QuestionController(
            questionServiceMock.Object);

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await questionController.DeleteQuestionAsync(questionIdMock);
        });
    }
}