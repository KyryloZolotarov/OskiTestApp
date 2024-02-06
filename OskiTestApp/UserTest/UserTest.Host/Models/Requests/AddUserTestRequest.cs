namespace UserTest.Host.Models.Requests;

public class AddUserTestRequest
{
    public string UserId { get; set; }
    public int TestId { get; set; }

    public bool IsTestCompleted { get; set; }
    public int Mark { get; set; }
}