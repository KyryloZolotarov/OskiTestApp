using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Data.EntityConfigurations;

public class TestEntityConfiguration
    : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.ToTable("Test");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("test_hilo")
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired();
    }
}