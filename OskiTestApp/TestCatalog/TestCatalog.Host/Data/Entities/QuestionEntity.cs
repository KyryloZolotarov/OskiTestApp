namespace TestCatalog.Host.Data.Entities
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public List<string> WrongAnswers { get; set; }
        public List<string> CorrectAnswers { get; set; }
        public int CorrectAnswersCount { get; set; }
    }
}
