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
    }
}
