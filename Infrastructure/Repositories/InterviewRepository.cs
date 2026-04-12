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
    public class InterviewRepository : Repository<Interview>, IInterviewRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Interview> set;
        public InterviewRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            this.set = context.Interviews;
        }

        public async Task<CardInfoRaw> GetInterviewCountAsync()
        {
            var start = new DateTime(DateTime.UtcNow.Year, 1, 1);
            var end = start.AddYears(1);

            var totalCount = await set.CountAsync();

            var monthData = await set.Where(a => a.StartedAt  >= start && a.StartedAt < end)
                                .GroupBy(a => a.StartedAt.Month)
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
    }
}
