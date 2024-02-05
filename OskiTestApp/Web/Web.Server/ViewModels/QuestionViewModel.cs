namespace Web.Server.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public Dictionary<int, string> AnswerVariants { get; set; }
        public int CorrectAnswersCount { get; set; }
    }
}
