using Domain.Enums;

namespace Domain.Entities
{
    public class ApplicationHistory
    {
        public int Id { get; set; }
        public int JobTrackId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public JobTrack JobTrack { get; set; }
    }
}