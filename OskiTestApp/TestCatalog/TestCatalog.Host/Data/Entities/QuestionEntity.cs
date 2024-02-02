namespace TestCatalog.Host.Data.Entities
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public TestEntity Test { get; set; }
        public List<string> WrongAnswers { get; set; }
        public List<string> CorrectAnswers { get; set; }
    }
}
