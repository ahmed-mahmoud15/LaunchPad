
namespace Domain.Entities
{
    public class UserExperience
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }

        public User User { get; set; }
    }
}
