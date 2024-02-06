using TestCatalog.Host.Models.Requests;

namespace TestCatalog.Host.Services.Interfaces;

public interface IQuestionService
{
    Task AddQuestionAsync(AddQuestionRequest question);
    Task UpdateQuestionAsync(UpdateQuestionRequest question);
    Task DeleteQuestionAsync(int questionId);
}