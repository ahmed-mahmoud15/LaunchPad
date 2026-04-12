using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.RawData;

namespace Domain.Interfaces
{
    public interface ICvRepository : IRepository<UserCv>
    {
        Task<CardInfoRaw> GetCvCountAsync();
        Task<CvEvaluationRaw> GetCvEvaluationAsync(int average);
    }
}
