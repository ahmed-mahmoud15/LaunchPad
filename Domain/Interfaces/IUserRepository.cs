using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<UserActivityRaw>> GetRecentActivitiesAsync(int userId, int count);
    }
    public class UserActivityRaw
    {
        public string Type { get; set; }
        public string Activity { get; set; }
        public DateTime Date { get; set; }
    }
}
