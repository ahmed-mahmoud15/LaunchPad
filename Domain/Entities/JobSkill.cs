using Domain.Enums;

namespace Domain.Entities
{
    public class JobSkill
    {
        public int JobTrackId { get; set; }
        public int SkillId { get; set; }
        public SkillLevel RequiredLevel { get; set; }

        public JobTrack JobTrack { get; set; }
        public Skill Skill { get; set; }
    }
}