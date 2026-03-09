
namespace Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNormalized { get
            {
                return Name.ToUpper();
            }
        }

        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    }
}
