using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestCatalog.Host.Data.Entities;

namespace TestCatalog.Host.Data.EntityConfigurations;

public class AnswerEntityConfiguration
    : IEntityTypeConfiguration<AnswerEntity>
{
    public void Configure(EntityTypeBuilder<AnswerEntity> builder)
    {
        builder.ToTable("Answer");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("anwser_hilo")
            .IsRequired();

        builder.Property(x => x.Answer).IsRequired().HasMaxLength(200);
        builder.Property(x => x.isCorrect).IsRequired();
        builder.Property(x => x.QuestionId).IsRequired();
        builder.HasOne(ci => ci.Question)
            .WithMany()
            .HasForeignKey(ci => ci.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}