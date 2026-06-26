using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;

namespace Application.Validations
{
    public class ValidJobAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not AnalyzeCvRequestDto dto)
            {
                return new ValidationResult("Invalid Request");
            }

            bool hasJob = dto.JobId.HasValue;
            bool hasNewJob =
                !string.IsNullOrWhiteSpace(dto.JobTitle) &&
                !string.IsNullOrWhiteSpace(dto.JobDescription);

            if (!hasJob && !hasNewJob)
            {
                return new ValidationResult(
                    "You must either select an existing job or provide a new job title and description.",
                    new[] { nameof(dto.JobId), nameof(dto.JobTitle), nameof(dto.JobDescription) });
            }

            if (hasJob && (dto.JobTitle is not null || dto.JobDescription is not null))
            {
                return new ValidationResult(
                    "Provide either an existing job or a new job description, not both.",
                    new[] { nameof(dto.JobId), nameof(dto.JobTitle), nameof(dto.JobDescription) });
            }

            if (!hasJob)
            {
                if (string.IsNullOrWhiteSpace(dto.JobTitle))
                {
                    return new ValidationResult(
                        "Job Title is required.",
                        new[] { nameof(dto.JobTitle) });
                }

                if (string.IsNullOrWhiteSpace(dto.JobDescription))
                {
                    return new ValidationResult(
                        "Job Description is required.",
                        new[] { nameof(dto.JobDescription) });
                }
            }

            return ValidationResult.Success;
        }

    }
}
