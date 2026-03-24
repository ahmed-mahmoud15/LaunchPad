using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CodingQuestionConfiguration : IEntityTypeConfiguration<CodingQuestion>
    {
        public void Configure(EntityTypeBuilder<CodingQuestion> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title).IsRequired().HasMaxLength(300);
            builder.Property(q => q.TitleSlug).IsRequired().HasMaxLength(300);
            builder.Property(q => q.LeetcodeId).IsRequired();
            builder.Property(q => q.Description).IsRequired();
            builder.Property(q => q.Difficulty).IsRequired();

            builder.HasIndex(q => q.LeetcodeId).IsUnique();

            builder.HasOne(q => q.Topic)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TopicId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}