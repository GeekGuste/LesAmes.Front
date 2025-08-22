using LesAmes.Domain.Souls;
using LesAmes.Domain.Users;

namespace LesAmes.Domain.Hobbies;

public class Hobby
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Description { get; set; }
    public string HobbyCategoryId { get; set; }

    public virtual HobbyCategory HobbyCategory { get; set; }
    public List<Soul> Souls { get; set; }
    public ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();
}
