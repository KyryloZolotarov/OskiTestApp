using TestCatalog.Host.Models.Requests;

namespace TestCatalog.Host.Services.Interfaces
{
    public interface IAnswerService
    {
        Task AddAnswerAsync(AddAnswerRequest answer);
        Task UpdateAnswerAsync(UpdateAnswerRequest answer);
        Task DeleteAnswerAsync(int answerId);
    }
}
