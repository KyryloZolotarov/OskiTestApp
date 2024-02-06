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
            new() { UserId = "ab82536f-b0ce-4b9f-9333-9ccd53334c80", TestId = 1 },
            new() { UserId = "ab82536f-b0ce-4b9f-9333-9ccd53334c80", TestId = 2 },
            new() { UserId = "ab82536f-b0ce-4b9f-9333-9ccd53334c80", TestId = 3 }
        };
    }
}