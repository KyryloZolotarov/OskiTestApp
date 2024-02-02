using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserTest.Host.Data.Entities;

namespace UserTest.Host.Data.EntityConfigurations
{
    public class UserTestEntityConfiguration
    : IEntityTypeConfiguration<UserTestEntity>
    {
        public void Configure(EntityTypeBuilder<UserTestEntity> builder)
        {
            builder.ToTable("UserTests");

            builder.Property(ci => ci.UserId)
                .IsRequired();

            builder.Property(cb => cb.TestId)
                .IsRequired();

            builder.Property(cd => cd.Mark)
                .IsRequired(false);
        }
    }
}
