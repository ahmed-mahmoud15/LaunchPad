using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
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
