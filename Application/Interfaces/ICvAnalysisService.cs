using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;
using Domain.Common;

namespace Application.Interfaces
{
    public interface ICvAnalysisService
    {
        public Task<Result<AnalyzeCvResultDto>> AnalyzeCvAsync(int userId, AnalyzeCvRequestDto dto);
    }
}
