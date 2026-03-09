using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class ApplicationHistory
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Job Job { get; set; }
    }
}
