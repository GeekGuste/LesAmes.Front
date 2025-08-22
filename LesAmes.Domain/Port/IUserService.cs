using LesAmes.Domain.Authentication;
using LesAmes.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace LesAmes.Domain.Port;

public interface IUserService
{
    Task<bool> RegisterAsync(string email, string password);
    Task<TokenResponse> LoginAsync(string email, string password);
    Task<ApplicationUser?> GetByIdAsync(string id);
    Task<ApplicationUser?> GetCurrentAsync(string userId);
    Task<IdentityResult> UpdateAsync(string id, UpdateUser updateUser);
    Task<IdentityResult> ChangePasswordAsync(string id, string newPassword);
    Task<IdentityResult> DisableAsync(string id);
    Task<IdentityResult> EnableAsync(string id);
    Task<IdentityResult> AddRoleAsync(string id, string role);
    Task<IdentityResult> RemoveRoleAsync(string id, string role);
}
