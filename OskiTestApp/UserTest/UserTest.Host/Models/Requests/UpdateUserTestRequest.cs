namespace UserTest.Host.Models.Requests
{
    public class UpdateUserTestRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TestId { get; set; }
        public int Mark { get; set; }
    }
}
