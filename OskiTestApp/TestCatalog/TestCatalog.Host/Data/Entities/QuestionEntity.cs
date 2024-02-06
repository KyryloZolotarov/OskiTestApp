namespace TestCatalog.Host.Data.Entities;

public class QuestionEntity
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public TestEntity Test { get; set; }
    public string Question { get; set; }
}