using LesAmes.Domain.Authentication;

namespace LesAmes.Domain.Users;

public class Tutor : ApplicationUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}