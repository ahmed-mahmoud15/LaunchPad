using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserExperienceConfiguration : IEntityTypeConfiguration<UserExperience>
    {
        public void Configure(EntityTypeBuilder<UserExperience> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Company).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Description).HasMaxLength(2000);
        }
    }
}