using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.RawData;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CvRepository : Repository<UserCv>, ICvRepository
    {
        private readonly AppDbContext context;
        public CvRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<CardInfoRaw> GetCvCountAsync()
        {
            var start = new DateTime(DateTime.UtcNow.Year, 1, 1);
            var end = start.AddYears(1);

            var totalCount = await context.CvJobAnalyses.CountAsync();

            var monthData = await context.CvJobAnalyses.Where(a => a.AnalyzeDate >= start && a.AnalyzeDate < end)
                                .GroupBy(a => a.AnalyzeDate.Month)
                                .Select(g => new
                                {
                                    Month = g.Key,
                                    Count = g.Count()
                                })
                                .ToListAsync();

            return new CardInfoRaw
            {
                TotalCount = totalCount,
                CountPerMonth = Enumerable.Range(1, 12)
                .ToDictionary(m => m, m => monthData.FirstOrDefault(x => x.Month == m)?.Count ?? 0)
            };
        }

        public async Task<CvEvaluationRaw> GetCvEvaluationAsync(int average)
        {
            var start = new DateTime(DateTime.UtcNow.Year, 1, 1);
            var end = start.AddYears(1);

            var data = await context.CvJobAnalyses
                .Where(a => a.AnalyzeDate >= start && a.AnalyzeDate < end)
                .GroupBy(a => a.AnalyzeDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Above = g.Count(x => x.Score >= average),
                    Below = g.Count(x => x.Score < average)
                })
                .ToListAsync();

            return new CvEvaluationRaw
            {
                AboveAverage = Enumerable.Range(1, 12)
                    .ToDictionary(m => m, m => data.FirstOrDefault(x => x.Month == m)?.Above ?? 0),

                BelowAverage = Enumerable.Range(1, 12)
                    .ToDictionary(m => m, m => data.FirstOrDefault(x => x.Month == m)?.Below ?? 0)
            };
        }
    }
}
