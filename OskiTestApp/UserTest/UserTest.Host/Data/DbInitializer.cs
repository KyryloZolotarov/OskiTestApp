using System.ComponentModel.DataAnnotations;
using UserTest.Host.Data.Entities;

namespace UserTest.Host.Data
{
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
                new () { UserId = "", TestId = 1 },
                new () { UserId = "", TestId = 2 },
                new () { UserId = "", TestId = 3 },
            };
        }
    }
}
