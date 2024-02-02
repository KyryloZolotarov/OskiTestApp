
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;

namespace TestCatalog.Host.Services.Interfaces
{
    public interface IQuestionManageService
    {
        Task AddQuestionAsync(AddQuestionRequest question);
        Task UpdateQuestionAsync(UpdateQuestionRequest question);
        Task DeleteQuestionAsync(int questionId);
    }
}
