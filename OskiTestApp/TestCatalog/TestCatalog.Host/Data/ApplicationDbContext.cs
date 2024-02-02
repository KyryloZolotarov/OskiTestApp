using Microsoft.EntityFrameworkCore;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Data.EntityConfigurations;


namespace TestCatalog.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<QuestionEntity> Questions { get; set; } = null!;
        public DbSet<TestEntity> Tests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new QuestionEntityConfiguration());
            builder.ApplyConfiguration(new TestEntityConfiguration());
        }
    }
}
