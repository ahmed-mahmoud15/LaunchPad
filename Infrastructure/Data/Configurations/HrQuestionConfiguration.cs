using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class HrQuestionConfiguration : IEntityTypeConfiguration<HrQuestion>
    {
        public void Configure(EntityTypeBuilder<HrQuestion> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Question).IsRequired().HasMaxLength(1000);
            builder.Property(h => h.ModelAnswer).HasMaxLength(3000);
        }
    }
}