using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Job_Tracker
{
    public class ChangeJobTrackStatusDto
    {
        public ApplicationStatus OldStatus { get; set; }
        public ApplicationStatus NewStatus { get; set; }
        public int JobTrackId { get; set; }
        public string? Notes { get; set; }
    }
}
