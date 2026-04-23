using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;

namespace Application.DTOs.Job_Tracker
{
    public class CreateJobTrackDto
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string? JobUrl { get; set; }
        public decimal? Salary { get; set; }
        public string? Notes { get; set; }
        public int? CvId { get; set; }
        public UploadCvDto? NewCvDto { get; set; }
        public List<CreateJobSkillDto> Skills { get; set; } = new List<CreateJobSkillDto>();
    }
}
