using Microsoft.AspNetCore.Identity;

namespace LesAmes.Domain.Authentication;

public class ApplicationRole : IdentityRole<string>
{
    public ApplicationRole() : base() { }

    public ApplicationRole(string name) : base(name) { }
}
