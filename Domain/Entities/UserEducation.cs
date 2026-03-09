
namespace Domain.Entities
{
    public class UserEducation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string University { get; set; }
        public string Program { get; set; }
        public string Degree { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Grade { get; set; }

        public User User { get; set; }
    }
}
