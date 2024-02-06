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
            new() { UserId = "3808e841-ee4e-46a9-8f33-75d9d44acbbd", TestId = 1 },
            new() { UserId = "3808e841-ee4e-46a9-8f33-75d9d44acbbd", TestId = 2 },
            new() { UserId = "3808e841-ee4e-46a9-8f33-75d9d44acbbd", TestId = 3 }
        };
    }
}