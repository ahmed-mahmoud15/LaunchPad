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

            builder.Property(aq => aq.Status).IsRequired();
            builder.Property(aq => aq.CodeSubmitted);
            builder.Property(aq => aq.LanguageUsed).IsRequired().HasMaxLength(50);
            builder.Property(aq => aq.SubmittedAt).IsRequired();

            builder.HasOne(aq => aq.Assessment)
                .WithMany(a => a.Questions)
                .HasForeignKey(aq => aq.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(aq => aq.Question)
                .WithMany()
                .HasForeignKey(aq => aq.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}