using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class QuestionTopicConfiguration : IEntityTypeConfiguration<QuestionTopic>
    {
        public void Configure(EntityTypeBuilder<QuestionTopic> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Slug).IsRequired().HasMaxLength(100);
            builder.HasIndex(t => t.Slug).IsUnique();
        }
    }
}