using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserEducationConfiguration : IEntityTypeConfiguration<UserEducation>
    {
        public void Configure(EntityTypeBuilder<UserEducation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.University).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Program).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Degree).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Grade).HasMaxLength(50);
        }
    }
}