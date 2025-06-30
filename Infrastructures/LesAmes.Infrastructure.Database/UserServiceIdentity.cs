using LesAmes.Domain.Authentication;
using LesAmes.Domain.Port;
using LesAmes.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LesAmes.Infrastructure.Database;

public class UserServiceIdentity : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly AppDbContext _dbContext;

    public UserServiceIdentity(UserManager<ApplicationUser> userManager, IConfiguration config, AppDbContext dbContext)
    {
        _userManager = userManager;
        _config = config;
        _dbContext = dbContext;
    }

    public async Task<bool> RegisterAsync(string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded;
    }

    public async Task<TokenResponse> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        return await GenerateTokenResponseFromUser(user);
    }

    private async Task<TokenResponse> GenerateTokenResponseFromUser(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, Roles.User),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        var tokenText = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse
        {
            AccessToken = tokenText,
            RefreshToken = await GenerateRefreshTokenFromToken(user, tokenText)
        };
    }

    private async Task<string> GenerateRefreshTokenFromToken(ApplicationUser user, string tokenText)
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            UserId = user.Id
        };
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();

        return refreshToken.Token;
    }

    public async Task<TokenResponse> GenerateNewTokensFromRefreshToken(string refreshToken)
    {
        // 1. Get the refresh token from database
        var storedRefreshToken = await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .SingleOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedRefreshToken is null || storedRefreshToken.IsRevoked || storedRefreshToken.Expires < DateTime.UtcNow)
            return null;

        // 2. Revoke old refreshToken
        storedRefreshToken.IsRevoked = true;

        // 3. On crée un nouveau JWT
        return await GenerateTokenResponseFromUser(storedRefreshToken.User);

    }

    public async Task<ApplicationUser?> GetByIdAsync(string id) =>
        await _userManager.FindByIdAsync(id);

    public async Task<ApplicationUser?> GetCurrentAsync(ClaimsPrincipal userPrincipal)
    {
        var userId = _userManager.GetUserId(userPrincipal);
        return await GetByIdAsync(userId);
    }

    public async Task<IdentityResult> UpdateAsync(string id, UpdateUser updateUser)
    {
        var user = await GetByIdAsync(id);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        if (!string.IsNullOrWhiteSpace(updateUser.FirstName)) user.FirstName = updateUser.FirstName;
        if (!string.IsNullOrWhiteSpace(updateUser.LastName)) user.LastName = updateUser.LastName;

        return await _userManager.UpdateAsync(user);
    }
}
