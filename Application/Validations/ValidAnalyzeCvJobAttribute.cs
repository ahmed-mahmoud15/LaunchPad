using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Application.DTOs.Cv;

namespace Application.Validations
{
    public class ValidAnalyzeCvJobAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not AnalyzeCvRequestDto dto)
            {
                return new ValidationResult("Invalid Request");
            }

            bool hasCv = dto.CvId.HasValue;
            bool hasNewCv = dto.File is not null;
            bool hasJob = dto.JobId.HasValue;
            bool hasNewJob = !(string.IsNullOrEmpty(dto.JobDescription) || string.IsNullOrEmpty(dto.JobTitle));

            if (!hasCv && !hasNewCv)
            {
                return new ValidationResult("You must either select an existing Cv or upload new ont",
                    new[] { nameof(dto.CvId), nameof(dto.File) });
            }

            if(hasCv && hasNewCv)
            {
                return new ValidationResult("Provide either an existing CV or a new upload, not both.",
                    new[] { nameof(dto.CvId), nameof(dto.File) });
            }

            if(!hasJob && !hasNewJob)
            {
                return new ValidationResult("You Must either select an existng job or provide new job title and description", 
                    new[] { nameof(dto.JobId), nameof(dto.JobTitle), nameof(dto.JobDescription) });
            }

            if (hasJob && hasNewJob)
            {
                return new ValidationResult("Provide either an existing job or a new job description, not both",
                    new[] { nameof(dto.JobId), nameof(dto.JobTitle), nameof(dto.JobDescription) });
            }

            if (!hasJob)
            {
                if (string.IsNullOrEmpty(dto.JobTitle))
                {
                    return new ValidationResult("Job Title is required when providing a new job",
                        new[] {nameof(dto.JobTitle)});
                }

                if (string.IsNullOrEmpty(dto.JobDescription))
                {
                    return new ValidationResult("Job Description is required when providing a new job",
                        new[] { nameof(dto.JobDescription) });
                }
            }

            return ValidationResult.Success;
        }
    }
}
