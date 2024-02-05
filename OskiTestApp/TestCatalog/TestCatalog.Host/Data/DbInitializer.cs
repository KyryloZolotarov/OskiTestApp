using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();


            if (!context.Questions.Any())
            {
                await context.Questions.AddRangeAsync(GetPreconfiguredQuestions());

                await context.SaveChangesAsync();
            }

            if (!context.Tests.Any())
            {
                await context.Tests.AddRangeAsync(GetPreconfiguredTests());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<QuestionEntity> GetPreconfiguredQuestions()
        {
            return new List<QuestionEntity>
            {
                new () 
                { 
                    Question = "Question 1",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 1, 4 } 
                },
                new () 
                {
                    Question = "Question 2",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 1, 2 }
                },
                new () 
                {
                    Question = "Question 3",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 2, 3 } 
                },
                new () 
                { 
                    Question = "Question 4",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 1 } 
                },
                new () 
                { 
                    Question = "Question 5",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 1, 2 } 
                },
                new () 
                { 
                    Question = "Question 6",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 3, 4 } 
                },
                new () 
                { 
                    Question = "Question 7",
                    TestId = 1,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 4 } 
                },
                new () 
                { 
                    Question = "Question 1",
                    TestId = 2,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 2 } 
                },
                new () 
                { 
                    Question = "Question 2",
                    TestId = 2,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 3 } 
                },
                new () 
                { 
                    Question = "Question 3",
                    TestId = 2,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 2, 4 } 
                },
                new () 
                { 
                    Question = "Question 1",
                    TestId = 3,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 1, 4 } 
                },
                new () 
                { 
                    Question = "Question 2",
                    TestId = 3,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 1, 3 } 
                },
                new () 
                { 
                    Question = "Question 3",
                    TestId = 3,
                    AnswerVariants = new Dictionary<int, string>()
                    {
                        { 1, "Variant 1" },
                        { 2, "Variant 2" },
                        { 3, "Variant 3" },
                        { 4, "Variant 4" }

                    },
                    CorrectAnswers = new List<int> { 2 } 
                },
            };
        }
        private static IEnumerable<TestEntity> GetPreconfiguredTests()
        {
            return new List<TestEntity>
            {
                new () { Name = "Test 1", Description = "Some test description for test 1"},
                new () { Name = "Test 2", Description = "Some test description for test 2"},
                new () { Name = "Test 3", Description = "Some test description for test 3"}
            };
        }
    }
}

