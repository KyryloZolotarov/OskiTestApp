using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using System;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Repositories;
using TestCatalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Services
{
    public class QuestionService : BaseDataService<ApplicationDbContext>, IQuestionService
    {
        private readonly IQuestionRepository _questionManageRepository;

        public QuestionService(IQuestionRepository questionManageRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger)
            : base(dbContextWrapper, logger)
        {
            _questionManageRepository = questionManageRepository;
        }
        public async Task AddQuestionAsync(AddQuestionRequest question)
        {

            await ExecuteSafeAsync(async () =>
            {
                var questionAdd = new QuestionEntity() { TestId = question.TestId,
                    CorrectAnswers = question.CorrectAnswers,
                    AnswerVariants = question.AnswerVariants,
                    Question = question.Question,
                    Test = new TestEntity 
                    { 
                        Id = question.Test.Id,
                        Description = question.Test.Description,
                        Name = question.Test.Name
                    } 
                };
                await _questionManageRepository.AddQuestionAsync(questionAdd);
            });
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var questionExists = await ExecuteSafeAsync(async () => await _questionManageRepository.GetQuestionAsync(questionId));

            if (questionExists == null)
            {
                throw new BusinessException($"Question with id: {questionId} not found");
            }

            await ExecuteSafeAsync(async () =>
            {
                await _questionManageRepository.DeleteQuestionAsync(questionExists);
            });
        }

        public async Task UpdateQuestionAsync(UpdateQuestionRequest question)
        {
            var questionExists = await ExecuteSafeAsync(async () => await _questionManageRepository.GetQuestionAsync(question.Id));

            if (questionExists == null)
            {
                throw new BusinessException($"Question with id: {question.Id} not found");
            }

            if (question.AnswerVariants != null)
            {
                questionExists.AnswerVariants = question.AnswerVariants;
            }

            if (question.CorrectAnswers != null)
            {
                questionExists.CorrectAnswers = question.CorrectAnswers;
            }

            if (question.Question != null)
            {
                questionExists.Question = question.Question;
            }

            if (question.TestId != null)
            {
                questionExists.TestId = question.TestId;
            }

            await ExecuteSafeAsync(async () =>
            {
                await _questionManageRepository.UpdateQuestionAsync(questionExists);
            });
        }
    }
}
