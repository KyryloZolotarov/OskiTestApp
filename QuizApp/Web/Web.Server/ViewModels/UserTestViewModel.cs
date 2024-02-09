namespace Web.Server.ViewModels
{
    public class UserTestViewModel
    {
        public int TestId { get; set; }
        public List<QuestionForUserTestViewModel> Answers { get; set; }
    }
}
