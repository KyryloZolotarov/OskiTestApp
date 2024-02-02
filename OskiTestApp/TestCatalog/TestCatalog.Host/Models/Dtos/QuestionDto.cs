namespace TestCatalog.Host.Models.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public List<string> WrongAnswers { get; set; }
        public List<string> CorrectAnswers { get; set; }
        public int CorrectAnswersCount { get; set; }
    }
}
