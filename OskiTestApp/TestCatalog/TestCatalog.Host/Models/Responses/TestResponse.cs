using TestCatalog.Host.Models.Dtos;

namespace TestCatalog.Host.Models.Responses
{
    public class TestResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
