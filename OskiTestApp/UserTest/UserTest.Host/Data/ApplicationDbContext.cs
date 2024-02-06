using Microsoft.EntityFrameworkCore;
using UserTest.Host.Data.Entities;
using UserTest.Host.Data.EntityConfigurations;

namespace UserTest.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserTestEntity> UserTests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserTestEntityConfiguration());
    }
}