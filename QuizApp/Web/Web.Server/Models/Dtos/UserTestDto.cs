namespace Web.Server.Models.Dtos;

public class UserTestDto
{
    public string UserId { get; set; }
    public int TestId { get; set; }

    public bool IsTestCompleted { get; set; }
    public int Mark { get; set; }
}