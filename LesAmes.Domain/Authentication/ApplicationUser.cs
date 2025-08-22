using Microsoft.AspNetCore.Identity;

namespace LesAmes.Domain.Authentication;

public class ApplicationUser : IdentityUser<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
