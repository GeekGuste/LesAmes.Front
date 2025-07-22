using LesAmes.Domain.Authentication;
using LesAmes.Domain.Hobbies;
using LesAmes.Domain.Souls;

namespace LesAmes.Domain.Users;

public class Tutor : ApplicationUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int BirthYear { get; set; }
    public List<AgeRange> SoulsAgeRanges { get; set; }

    public ICollection<Soul> Mentees { get; set; }
    public List<Hobby> Hobbies { get; set; } = new List<Hobby>();

}