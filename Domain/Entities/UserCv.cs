using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserCv
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsDefault { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public User User { get; set; }
    }
}
