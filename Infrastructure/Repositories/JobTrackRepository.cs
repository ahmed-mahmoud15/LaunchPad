using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobTrackRepository : Repository<JobTrack>, IJobTrackRepository
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<JobTrack> set;

        public JobTrackRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            this.set = context.JobTracks;
        }

        public async Task<PagedResponse<JobTrack>> GetJobTracksForUserPaginatedAsync(int userId, PagedRequest request)
        {
            var query = set.Include(e => e.Job).Where(e => e.Job.UserId == userId);

            var totalCount = await query.CountAsync();

            var items = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            return new PagedResponse<JobTrack>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
