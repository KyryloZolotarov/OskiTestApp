using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Models.Dtos;

public class AnswerDto
{
    public int Id { get; set; }
    public string Answer { get; set; }
    public int QuestionId { get; set; }

    public QuestionEntity Question { get; set; }

    public bool isCorrect { get; set; }
}