using LesAmes.Domain.Hobbies;
using LesAmes.Domain.Users;

namespace LesAmes.Domain.Souls;
public class Soul
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Sexe Sexe { get; set; }
    public string TutorId { get; set; }
    public Tutor? Tutor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Telephone { get; set; }
    public string Quartier { get; set; }
    public string Email { get; set; }
    public string AgeRangeId { get; set; }
    public AgeRange AgeRange { get; set; }
    public DateTime DateCreation { get; set; }
    public List<Hobby> Hobbies { get; set; } = new List<Hobby>();
}
