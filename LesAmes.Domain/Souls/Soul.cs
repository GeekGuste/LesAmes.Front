using LesAmes.Domain.Users;

namespace LesAmes.Domain.Souls;
public class Soul
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TutorId { get; set; }
    public Tutor Tutor { get; set; }
}
