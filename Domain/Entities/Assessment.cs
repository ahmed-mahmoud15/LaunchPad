using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EasyCount { get; set; }
        public int MediumCount { get; set; }
        public int HardCount { get; set; }
        public int TotalCount { get
            {
                return  EasyCount + MediumCount + HardCount;
            }
        }

        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int Score { get; set; }

        public User User { get; set; }
        public ICollection<AssessmentQuestion> Questions = new List<AssessmentQuestion>();
    }
}
