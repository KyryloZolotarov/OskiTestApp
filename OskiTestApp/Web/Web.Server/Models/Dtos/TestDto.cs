namespace Web.Server.Models.Dtos;

public class TestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<QuestionDto> Questions { get; set; }
}