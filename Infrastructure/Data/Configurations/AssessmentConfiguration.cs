using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.EasyCount).IsRequired();
            builder.Property(a => a.MediumCount).IsRequired();
            builder.Property(a => a.HardCount).IsRequired();
            builder.Property(a => a.Score).IsRequired();
            builder.Property(a => a.CreatedAt).IsRequired();

            builder.Ignore(a => a.TotalCount);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Assessments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Questions)
                .WithOne(aq => aq.Assessment)
                .HasForeignKey(aq => aq.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}