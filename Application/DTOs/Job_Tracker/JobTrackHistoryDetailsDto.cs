using Domain.Enums;

namespace Application.DTOs.Job_Tracker
{
    public class JobTrackHistoryDetailsDto
    {
        public ApplicationStatus OldStatus { get; set; }
        public ApplicationStatus NewStatus { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
