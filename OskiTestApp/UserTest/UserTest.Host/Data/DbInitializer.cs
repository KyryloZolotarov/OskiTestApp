using UserTest.Host.Data.Entities;

namespace UserTest.Host.Data;

public class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();


        if (!context.UserTests.Any())
        {
            await context.UserTests.AddRangeAsync(GetPreconfiguredUserTests());

            await context.SaveChangesAsync();
        }
    }


    private static IEnumerable<UserTestEntity> GetPreconfiguredUserTests()
    {
        return new List<UserTestEntity>
        {
            new() { UserId = "e49a2977-ddd9-4a45-8ba4-1bab091984e6", TestId = 1 },
            new() { UserId = "e49a2977-ddd9-4a45-8ba4-1bab091984e6", TestId = 2 },
            new() { UserId = "e49a2977-ddd9-4a45-8ba4-1bab091984e6", TestId = 3 }
        };
    }
}