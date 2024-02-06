using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Repositories.Interfaces;

public interface IAnswerRepository
{
    Task AddAnswerAsync(AnswerEntity answer);
    Task UpdateAnswerAsync(AnswerEntity answer);
    Task DeleteAnswerAsync(AnswerEntity answer);
    Task<AnswerEntity> GetAnswerAsync(int answerId);
    Task<IEnumerable<AnswerEntity>> GetAnswersForTestAsync(int testId);
}