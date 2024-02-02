using Microsoft.EntityFrameworkCore;
using UserProfiles.Host.Data.Entities;
using UserProfiles.Host.Data.EntityConfigurations;

namespace UserProfiles.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
