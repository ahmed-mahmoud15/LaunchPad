using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class JobTrackConfiguration : IEntityTypeConfiguration<JobTrack>
    {
        public void Configure(EntityTypeBuilder<JobTrack> builder)
        {
            builder.ToTable("JobTracks");

            builder.Property(jt => jt.CompanyName).IsRequired().HasMaxLength(50);
            builder.Property(jt => jt.Location).HasMaxLength(200);
            builder.Property(jt => jt.Salary).HasColumnType("decimal(18,2)");
            builder.Property(jt => jt.JobUrl).HasMaxLength(500);
            builder.Property(jt => jt.CurrentStatus).IsRequired();

            builder.HasMany(jt => jt.History)
                .WithOne(h => h.Job)
                .HasForeignKey(h => h.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(jt => jt.SkillsRequired)
                .WithOne(js => js.Job)
                .HasForeignKey(js => js.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}