using LesAmes.Domain.Authentication;
using LesAmes.Domain.Souls;

namespace LesAmes.Domain.Users;

public class Tutor : ApplicationUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Soul> Mentees { get; set; }

}