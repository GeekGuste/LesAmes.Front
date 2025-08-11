using LesAmes.Domain.Authentication;
using LesAmes.Domain.Hobbies;
using LesAmes.Domain.Souls;

namespace LesAmes.Domain.Users;

public class Tutor : ApplicationUser
{
    public int BirthYear { get; set; }
    public TutorState State { get; set; } = TutorState.Demand;
    public List<AgeRange> SoulsAgeRanges { get; set; }

    public ICollection<Soul> Mentees { get; set; }
    public List<Hobby> Hobbies { get; set; } = new List<Hobby>();

}