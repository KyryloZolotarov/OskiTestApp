using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Models.Requests
{
    public class AddQuestionRequest
    {
        public int TestId { get; set; }
        public TestEntity Test { get; set; }
        public List<string> WrongAnswers { get; set; }
        public List<string> CorrectAnswers { get; set; }
    }
}
