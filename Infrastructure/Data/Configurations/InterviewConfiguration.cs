using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.StartedAt).IsRequired();
            builder.Property(i => i.EndedAt);
            builder.Property(i => i.Score).IsRequired();

            builder.HasOne(i => i.User)
                .WithMany(u => u.Interviews)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.InterviewQuestions)
                .WithOne(iq => iq.Interview)
                .HasForeignKey(iq => iq.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}