namespace TestCatalog.Host.Data.Entities;

public class AnswerEntity
{
    public int Id { get; set; }
    public string Answer { get; set; }
    public int QuestionId { get; set; }

    public QuestionEntity Question { get; set; }

    public bool isCorrect { get; set; }
}