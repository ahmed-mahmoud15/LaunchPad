using Domain.Enums;

namespace Domain.Entities
{
    public class UserSkill
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public SkillLevel Level { get; set; }

        public User User { get; set; }
        public Skill Skill { get; set; }
    }
}