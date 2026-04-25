using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;

namespace Application.DTOs.Job_Tracker
{
    public class ViewJobTrackDetailsDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }

        // will replaced by cv data
        public UserCvDto Cv {  get; set; }

        public List<JobTrackHistoryDetailsDto> History { get; set; } = new List<JobTrackHistoryDetailsDto>();

    }
}
