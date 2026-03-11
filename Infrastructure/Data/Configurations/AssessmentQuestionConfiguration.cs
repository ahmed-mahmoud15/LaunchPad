using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AssessmentQuestionConfiguration : IEntityTypeConfiguration<AssessmentQuestion>
    {
        public void Configure(EntityTypeBuilder<AssessmentQuestion> builder)
        {
            builder.HasKey(aq => aq.Id);

            builder.Property(aq => aq.CodeSubmitted).IsRequired();
            builder.Property(aq => aq.LanguageUsed).IsRequired().HasMaxLength(50);
            builder.Property(aq => aq.Status).IsRequired();

            builder.HasOne(aq => aq.Question)
                .WithMany()
                .HasForeignKey(aq => aq.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}