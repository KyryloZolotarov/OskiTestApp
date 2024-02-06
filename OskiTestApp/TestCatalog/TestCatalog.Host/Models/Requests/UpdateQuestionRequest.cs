using TestCatalog.Host.Models.Dtos;

namespace TestCatalog.Host.Models.Requests;

public class UpdateQuestionRequest
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public TestDto Test { get; set; }
    public string Question { get; set; }
    public Dictionary<int, string> AnswerVariants { get; set; }
    public List<int> CorrectAnswers { get; set; }
}