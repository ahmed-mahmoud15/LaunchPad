using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CodingQuestionTopicConfiguration : IEntityTypeConfiguration<CodingQuestionTopic>
    {
        public void Configure(EntityTypeBuilder<CodingQuestionTopic> builder)
        {
            builder.HasKey(ct => new { ct.CodingQuestionId, ct.QuestionTopicId });

            builder.HasOne(ct => ct.CodingQuestion)
                .WithMany(q => q.CodingQuestionTopics)
                .HasForeignKey(ct => ct.CodingQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ct => ct.QuestionTopic)
                .WithMany(t => t.CodingQuestionTopics)
                .HasForeignKey(ct => ct.QuestionTopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
