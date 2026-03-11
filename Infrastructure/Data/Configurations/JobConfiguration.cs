using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Title).IsRequired().HasMaxLength(200);
            builder.Property(j => j.Info).HasMaxLength(2000);
            builder.Property(j => j.Type).IsRequired();

            builder.HasOne(j => j.User)
                .WithMany(u => u.AppliedJobs)
                .HasForeignKey(jt => jt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(j => j.Cv)
                .WithMany()
                .HasForeignKey(j => j.CvId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}