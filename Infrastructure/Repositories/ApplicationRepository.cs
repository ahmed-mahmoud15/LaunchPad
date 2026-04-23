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
    public class ApplicationRepository : Repository<ApplicationHistory>, IApplicationRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<ApplicationHistory> set;
        public ApplicationRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            set = context.Set<ApplicationHistory>();
        }

        public async Task DeleteAllByJobTrackIdAsync(int jobTrackId)
        {
            var items = await set.Where(a => a.JobTrackId == jobTrackId).ToListAsync();
            if (items.Count > 0) {
                set.RemoveRange(items);
            }
        }
    }
}
