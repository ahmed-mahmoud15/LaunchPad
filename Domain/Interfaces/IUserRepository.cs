using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.RawData;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<UserActivityRaw>> GetRecentActivitiesAsync(int userId, int count);
        Task<CardInfoRaw> GetUsersCountAsync();
    }
}
