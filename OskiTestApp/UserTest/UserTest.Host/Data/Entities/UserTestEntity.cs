using System.ComponentModel.DataAnnotations;

namespace UserTest.Host.Data.Entities;

public class UserTestEntity
{
    [Key] public string UserId { get; set; }

    [Key] public int TestId { get; set; }

    public bool IsTestCompleted { get; set; } = false;
    public int? Mark { get; set; }
}