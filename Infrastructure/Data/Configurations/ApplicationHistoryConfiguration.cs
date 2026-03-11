using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ApplicationHistoryConfiguration : IEntityTypeConfiguration<ApplicationHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationHistory> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Status).IsRequired();
            builder.Property(h => h.Notes).HasMaxLength(1000);
            builder.Property(h => h.UpdatedAt).IsRequired();

            // Job navigation points to JobTrack (parent), cascade handled there
            builder.HasOne(h => h.Job)
                .WithMany()
                .HasForeignKey(h => h.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}