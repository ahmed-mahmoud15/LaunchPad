using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CvJobAnalysisConfiguration : IEntityTypeConfiguration<CvJobAnalysis>
    {
        public void Configure(EntityTypeBuilder<CvJobAnalysis> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Feedback).IsRequired();
            builder.Property(c => c.Score).IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(u => u.AnalysedJobs)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Cv)
                .WithMany()
                .HasForeignKey(c => c.CvId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Job)
                .WithMany()
                .HasForeignKey(c => c.JobId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}