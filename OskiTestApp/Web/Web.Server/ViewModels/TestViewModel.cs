namespace Web.Server.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
