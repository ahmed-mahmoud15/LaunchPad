using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;
using Application.DTOs.Interview;
using Application.Interfaces;

namespace Application.Validations
{
    public class ValidCvAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not ICvRequest dto)
            {
                return new ValidationResult("Invalid Request");
            }

            bool hasCv = dto.CvId.HasValue;
            bool hasNewCv = dto.File is not null;

            if (!hasCv && !hasNewCv && dto is AnalyzeCvRequestDto)
            {
                return new ValidationResult(
                    "You must either select an existing CV or upload a new one.",
                    new[] { nameof(dto.CvId), nameof(dto.File) });
            }

            if (hasCv && hasNewCv)
            {
                return new ValidationResult(
                    "Provide either an existing CV or a new upload, not both.",
                    new[] { nameof(dto.CvId), nameof(dto.File) });
            }

            return ValidationResult.Success;
        }
    }
}
