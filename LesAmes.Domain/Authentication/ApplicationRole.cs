using Microsoft.AspNetCore.Identity;

namespace LesAmes.Domain.Authentication;

public class ApplicationRole : IdentityRole<string>
{
    public ApplicationRole() : base()
    {
        Id = Guid.NewGuid().ToString();
    }

    public ApplicationRole(string name) : base(name)
    {
        Id = Guid.NewGuid().ToString();
    }

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}
