using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(QuestionEntity question);
        Task UpdateQuestionAsync(QuestionEntity question);
        Task DeleteQuestionAsync(QuestionEntity question);
        Task<QuestionEntity> GetQuestionAsync(int questionId);
    }
}
