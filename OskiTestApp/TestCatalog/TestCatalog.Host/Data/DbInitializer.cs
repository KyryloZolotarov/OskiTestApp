using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Data;

public class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.Tests.Any())
        {
            await context.Tests.AddRangeAsync(GetPreconfiguredTests());

            await context.SaveChangesAsync();
        }

        if (!context.Questions.Any())
        {
            await context.Questions.AddRangeAsync(GetPreconfiguredQuestions());

            await context.SaveChangesAsync();
        }

        if (!context.Answers.Any())
        {
            await context.Answers.AddRangeAsync(GetPreconfiguredAnswers());

            await context.SaveChangesAsync();
        }
    }

    public static IEnumerable<AnswerEntity> GetPreconfiguredAnswers()
    {
        return new List<AnswerEntity>
        {
            new()
            {
                Answer = "answer",
                QuestionId = 1,
                isCorrect = false
            },
            new()
            {
                Answer = "answer",
                QuestionId = 2,
                isCorrect = false
            },
            new()
            {
                Answer = "answer",
                QuestionId = 3,
                isCorrect = false
            },
            new()
            {
                Answer = "answer",
                QuestionId = 4,
                isCorrect = false
            },
            new()
            {
                Answer = "answer",
                QuestionId = 5,
                isCorrect = false
            },
            new()
            {
                Answer = "answer",
                QuestionId = 6,
                isCorrect = true
            },
            new()
            {
                Answer = "answer",
                QuestionId = 7,
                isCorrect = false
            }
        };
    }

    private static IEnumerable<QuestionEntity> GetPreconfiguredQuestions()
    {
        return new List<QuestionEntity>
        {
            new()
            {
                Question = "Question 1",
                TestId = 1
            },
            new()
            {
                Question = "Question 2",
                TestId = 1
            },
            new()
            {
                Question = "Question 3",
                TestId = 1
            },
            new()
            {
                Question = "Question 4",
                TestId = 1
            },
            new()
            {
                Question = "Question 5",
                TestId = 1
            },
            new()
            {
                Question = "Question 6",
                TestId = 1
            },
            new()
            {
                Question = "Question 7",
                TestId = 1
            },
            new()
            {
                Question = "Question 1",
                TestId = 2
            },
            new()
            {
                Question = "Question 2",
                TestId = 2
            },
            new()
            {
                Question = "Question 3",
                TestId = 2
            },
            new()
            {
                Question = "Question 1",
                TestId = 3
            },
            new()
            {
                Question = "Question 2",
                TestId = 3
            },
            new()
            {
                Question = "Question 3",
                TestId = 3
            }
        };
    }

    private static IEnumerable<TestEntity> GetPreconfiguredTests()
    {
        return new List<TestEntity>
        {
            new() { Name = "Test 1", Description = "Some test description for test 1" },
            new() { Name = "Test 2", Description = "Some test description for test 2" },
            new() { Name = "Test 3", Description = "Some test description for test 3" }
        };
    }
}