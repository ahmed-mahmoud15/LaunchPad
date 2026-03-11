using Domain.Enums;

namespace Domain.Entities
{
    public class JobSkill
    {
        public int JobId { get; set; }
        public int SkillId { get; set; }
        public SkillLevel RequiredLevel { get; set; }

        public JobTrack Job { get; set; }
        public Skill Skill { get; set; }
    }
}