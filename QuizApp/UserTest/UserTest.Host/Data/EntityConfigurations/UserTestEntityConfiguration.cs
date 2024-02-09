using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserTest.Host.Data.Entities;

namespace UserTest.Host.Data.EntityConfigurations;

public class UserTestEntityConfiguration
    : IEntityTypeConfiguration<UserTestEntity>
{
    public void Configure(EntityTypeBuilder<UserTestEntity> builder)
    {
        builder.ToTable("UserTests");

        builder.HasKey(ci => new { ci.UserId, ci.TestId });

        builder.Property(ci => ci.UserId)
            .IsRequired();

        builder.Property(cb => cb.TestId)
            .IsRequired();

        builder.Property(cj => cj.IsTestCompleted)
            .IsRequired();

        builder.Property(cd => cd.Mark)
            .IsRequired(false);
    }
}