using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.RawData;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentsRepository
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<Assessment> set;
        public AssessmentRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            this.set = context.Assessments;
        }

        public async Task<CardInfoRaw> GetAssessmentCountAsync()
        {
            var start = new DateTime(DateTime.UtcNow.Year, 1, 1);
            var end = start.AddYears(1);

            var totalCount = await set.CountAsync();

            var monthData = await set.Where(a => a.CreatedAt >= start && a.CreatedAt < end)
                                .GroupBy(a => a.CreatedAt.Month)
                                .Select(g => new
                                {
                                    Month = g.Key,
                                    Count = g.Count()
                                })
                                .ToListAsync();

            return new CardInfoRaw {
                TotalCount = totalCount,
                CountPerMonth = Enumerable.Range(1, 12)
                .ToDictionary(m => m, m => monthData.FirstOrDefault(x => x.Month == m)?.Count ?? 0)
            };

        }

        public async Task<PagedResponse<Assessment>> GetAssessmentForUserPaginatedAsync(int userId, PagedRequest request)
        {
            var query = set.Where(x => x.UserId == userId);

            var totalCount = await query.CountAsync();

            var items = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new PagedResponse<Assessment>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task<AssessmentPreferencesRaw> GetAssessmentPreferencesRawAsync()
        {
            return await set.GroupBy(a => 1).Select(g => new AssessmentPreferencesRaw
            {
                EasyCount = g.Sum(a => a.EasyCount),
                MediumCount = g.Sum(a => a.MediumCount),
                HardCount = g.Sum(a => a.HardCount)
            }).FirstOrDefaultAsync();
        }

        public async Task<Assessment> GetAssessmentWithIncludesAsync(int assessmentId)
        {
            return await set.Include(a => a.Questions).ThenInclude(q => q.Question).FirstOrDefaultAsync(a => a.Id == assessmentId);
        }
    }
}
