namespace Web.Server.Models.Dtos
{
    public class QuestionDto
    {        
        public int Id { get; set; }
        public string Question { get; set; }
        public Dictionary<int, string> AnswerVariants { get; set; }
        public List<int> CorrectAnswers { get; set; }
    }
}
