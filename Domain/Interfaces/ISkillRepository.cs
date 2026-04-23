using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task DeleteJobSkillsByJobIdAsync(int jobId);
    }
}
