using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InterviewQuestionConfiguration : IEntityTypeConfiguration<InterviewQuestion>
    {
        public void Configure(EntityTypeBuilder<InterviewQuestion> builder)
        {
            builder.HasKey(iq => iq.Id);

            builder.Property(iq => iq.UserResponse).IsRequired();
            builder.Property(iq => iq.Feedback).HasMaxLength(2000);

            builder.HasOne(iq => iq.Question)
                .WithMany()
                .HasForeignKey(iq => iq.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}