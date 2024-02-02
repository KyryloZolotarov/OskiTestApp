using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Models.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public TestDto Test { get; set; }
        public string Question { get; set; }
        public Dictionary<int, string> AnswerVariants { get; set; }
        public List<int> CorrectAnswers { get; set; }
    }
}
