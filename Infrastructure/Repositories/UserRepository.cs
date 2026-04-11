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
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<User> set;
        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            this.set = context.Users;
        }

        public async Task<CardInfoRaw> GetUsersCountAsync()
        {
            int currentYear = DateOnly.FromDateTime(DateTime.UtcNow).Year;
            var start = new DateOnly(currentYear, 1, 1);
            var end = start.AddYears(1);

            var totalCount = await set.CountAsync();

            var monthData = await set.Where(a => a.JoinDate >= start && a.JoinDate < end)
                                .GroupBy(a => a.JoinDate.Month)
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

        public async Task<IEnumerable<UserActivityRaw>> GetRecentActivitiesAsync(int userId, int count)
        {
            var jobActivities = await context.JobTracks
                .Where(j => j.Job.UserId == userId)
                .Select(j => new UserActivityRaw
                {
                    Type = "Job",
                    Date = j.AppliedAt,
                    Activity = $"Applied to job \"{j.Job.Title}\" at {j.CompanyName}"
                })
                .ToListAsync();

            var cvActivites = await context.CvJobAnalyses
                .Where(c => c.UserId == userId)
                .Select(c => new UserActivityRaw
                {
                    Type = "Cv",
                    Date = c.Cv.UploadedAt,
                    Activity = $"CV analyzed for job \"{c.Job.Title}\" (score: {c.Score})"
                })
                .ToListAsync();

            var assessmentActivites = await context.Assessments
                .Where(a => a.UserId == userId)
                .Select(a => new UserActivityRaw {
                    Type = "Assessment",
                    Date = a.CreatedAt,
                    Activity =$"Assessment created ({a.TotalCount}) questions"
                })
                .ToListAsync();

            var inerviewStarted = await context.Interviews
                .Where(i => i.UserId == userId)
                .Select(i => new UserActivityRaw
                {
                    Type = "Interview",
                    Date = i.StartedAt,
                    Activity = $"Interview started"
                })
                .ToListAsync();

            var inerviewCompleted = await context.Interviews
                .Where(i => i.UserId == userId && i.EndedAt != null)
                .Select(i => new UserActivityRaw
                {
                    Type = "Interview",
                    Date = i.EndedAt!.Value,
                    Activity = $"Interview completed with score {i.Score}"
                })
                .ToListAsync();

            return jobActivities
                    .Concat(assessmentActivites)
                    .Concat(cvActivites)
                    .Concat(inerviewStarted)
                    .Concat(inerviewCompleted)
                    .OrderByDescending(a => a.Date)
                    .Take(count);
        }
    }
}
