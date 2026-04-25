using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cv
{
    public class UserCvDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public int Score { get; set; }

        public string Url { get; set; }
    }
}
