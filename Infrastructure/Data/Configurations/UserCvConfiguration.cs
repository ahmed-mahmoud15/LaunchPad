using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserCvConfiguration : IEntityTypeConfiguration<UserCv>
    {
        public void Configure(EntityTypeBuilder<UserCv> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FileName).IsRequired().HasMaxLength(300);
            builder.Property(c => c.FilePath).IsRequired().HasMaxLength(500);
            builder.Property(c => c.IsDefault).IsRequired();
            builder.Property(c => c.UploadedAt).IsRequired();
            builder.Property(c => c.Score).IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(u => u.Cvs)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}