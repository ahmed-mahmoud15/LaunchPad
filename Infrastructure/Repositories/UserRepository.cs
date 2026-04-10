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

        public async Task<User> GetUserWithAllEntitiesAsync(int userId)
        {
            //return await set.AsSplitQuery()
            //                .Include(u => u.Assessments)
            //                .Include(u => u.Interviews)
            //                .Include(u => u.CvJobAnalyses)
            //                .Include(u => u.Jobs)
            //                    .ThenInclude(u => u.JobTrack)
            //                .FirstOrDefaultAsync(u => u.Id == userId);

            return await set
                        .Where(u => u.Id == userId).Select(u => new User{
                            Assessments =  u.Assessments.OrderBy(a => a.CompletedAt == null ? 0 : 1).ThenByDescending(a => a.CreatedAt).ToList(),
                            Interviews = u.Interviews.OrderBy(i => i.EndedAt == null ? 0 : 1).ThenByDescending(i => i.StartedAt).ToList(),
                            Jobs = u.Jobs.Select(j => new Job
                            {
                                Id = j.Id,
                                Title = j.Title,
                                JobTrack = j.JobTrack
                            }).ToList()
                        }).FirstOrDefaultAsync();
        }
    }
}
