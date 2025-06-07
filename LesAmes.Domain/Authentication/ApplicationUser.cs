using Microsoft.AspNetCore.Identity;

namespace LesAmes.Domain.Authentication;

public class ApplicationUser : IdentityUser<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}
