using AutoMapper;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCatalog.Host.Controllers;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Repositories;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services;

namespace TestCatalog.Tests.Services
{
    public class QuestionServiceTests
    {
        [Fact]
        public async Task AddQuestionAsync_Succesfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<QuestionService>>();
            var questionRepositoryMock = new Mock<IQuestionRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransactionMock.Object);

            var questionDtoSucces = new AddQuestionRequest()
            {
                TestId = 1,
                CorrectAnswers = new List<int>(),
                AnswerVariants = new Dictionary<int, string>(),
                Question = "Test",
                Test = new TestDto
                {
                    Id = 1,
                    Description = "Test",
                    Name = "Test"
                }
            };

            var questionEntitySucces = new QuestionEntity()
            {
                TestId = 1,
                CorrectAnswers = new List<int>(),
                AnswerVariants = new Dictionary<int, string>(),
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

            await questionService.AddQuestionAsync(questionDtoSucces);
            questionRepositoryMock.Verify(r => r.AddQuestionAsync(questionEntitySucces), Times.Once);
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

            var questionDtoSucces = new AddQuestionRequest();

            var questionEntitySucces = new QuestionEntity();

            questionRepositoryMock.Setup(h => h.AddQuestionAsync(It.IsAny<QuestionEntity>())).Throws<Exception>();

            var questionService = new QuestionService(
                questionRepositoryMock.Object,
                dbContextWrapperMock.Object,
                loggerMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => {
                await questionService.AddQuestionAsync(questionDtoSucces);
            });
            
        }
    }
}
