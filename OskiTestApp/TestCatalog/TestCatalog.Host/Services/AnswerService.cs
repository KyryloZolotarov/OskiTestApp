

using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Repositories;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TestCatalog.Host.Services
{
    public class AnswerService : BaseDataService<ApplicationDbContext>, IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IMapper mapper,
            IAnswerRepository answerRepository,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger)
            : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
        }
        public async Task AddAnswerAsync(AddAnswerRequest answer)
        {
            await ExecuteSafeAsync(async () =>
            {
                var answerAdd = new AnswerEntity
                {
                    Answer = answer.Answer,
                    isCorrect = answer.isCorrect,
                    QuestionId = answer.QuestionId,
                    Question = new QuestionEntity
                    {
                        Id = answer.Question.Id,
                        Question = answer.Question.Question,
                        Test =  new TestEntity
                        {
                            Id = answer.Question.Test.Id,
                            Description = answer.Question.Test.Description,
                            Name = answer.Question.Test.Name
                        },
                        TestId = answer.Question.TestId
                        
                    }
                };
                await _answerRepository.AddAnswerAsync(answerAdd);
            });
        }

        public async Task DeleteAnswerAsync(int answerId)
        {
            var answerExists = await ExecuteSafeAsync(async () => await _answerRepository.GetAnswerAsync(answerId));

            if (answerExists == null) throw new BusinessException($"Answer with id: {answerId} not found");

            await ExecuteSafeAsync(async () => { await _answerRepository.DeleteAnswerAsync(answerExists); });
        }

        public async Task UpdateAnswerAsync(UpdateAnswerRequest answer)
        {
            var answerExists =
                await ExecuteSafeAsync(async () => await _answerRepository.GetAnswerAsync(answer.Id));

            if (answerExists == null) throw new BusinessException($"Answer with id: {answer.Id} not found");

            if (answerExists.Answer != null) answerExists.Answer = answer.Answer;

            if (answerExists.QuestionId != null) answerExists.QuestionId = answer.QuestionId;

            if (answerExists.isCorrect != null) answerExists.isCorrect = answer.isCorrect;
            if (answerExists.Question != null) answerExists.Question = _mapper.Map<QuestionEntity>(answer.Question); 

            await ExecuteSafeAsync(async () => { await _answerRepository.UpdateAnswerAsync(answerExists); });
        }
    }
}
