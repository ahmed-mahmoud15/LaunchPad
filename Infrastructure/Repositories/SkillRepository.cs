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
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly AppDbContext context;
        private readonly DbSet<Skill> skillsSet;
        private readonly DbSet<JobSkill> jobSkillSet;
        private readonly DbSet<UserSkill> userSkillSet;
        public SkillRepository(AppDbContext context) : base(context)
        {
            this.context = context;
            this.skillsSet = context.Set<Skill>();
            this.jobSkillSet = context.Set<JobSkill>();
            this.userSkillSet = context.Set<UserSkill>();
        }

        public async Task DeleteJobSkillsByJobIdAsync(int jobId)
        {
            var items = await jobSkillSet.Where(s => s.JobTrackId == jobId).ToListAsync();
            if (items.Count > 0) {
                jobSkillSet.RemoveRange(items);
            }
        }
    }
}
