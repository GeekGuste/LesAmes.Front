using LesAmes.Domain.Port;
using LesAmes.Domain.Users;
using LesAmes.Dto.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LesAmes.Application.Services.Users;

public class UserService : LesAmesApiService
{
    private readonly IUserService _userService;

    public UserService(IHttpContextAccessor httpContextAccessor, IUserService userService) : base(httpContextAccessor)
    {
        _userService = userService;
    }

    public Task<bool> RegisterUserAsync(string email, string password)
    {
        return _userService.RegisterAsync(email, password);
    }

    public async Task<TokenResponseDto> LoginUserAsync(string email, string password)
    {
        var response = await _userService.LoginAsync(email, password);
        return new TokenResponseDto
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken
        };
    }
    public Task<IdentityResult> ChangeCurrentPasswordAsync(ChangePasswordDto dto)
    {
        return _userService.ChangePasswordAsync(CurrentUserId, dto.NewPassword);
    }
    public Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto dto)
    {
        return _userService.ChangePasswordAsync(CurrentUserId, dto.NewPassword);
    }

    public async Task<ApplicationUserDto> GetCurrentUserAsync()
    {
        var user = await _userService.GetCurrentAsync(CurrentUserId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }

        return new ApplicationUserDto
        {
            Id = user?.Id,
            Email = user?.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }

    public Task<IdentityResult> DisableUserAsync(string userId)
    {
        return _userService.DisableAsync(userId);
    }

    public Task<IdentityResult> EnableUserAsync(string userId)
    {
        return _userService.EnableAsync(userId);
    }

    public Task<IdentityResult> UpdateCurrentUserAsync(UpdateUserDto updateUserDto)
    {
        return UpdateUserAsync(CurrentUserId, updateUserDto);
    }

    public Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
    {
        if (updateUserDto == null)
        {
            throw new ArgumentNullException(nameof(updateUserDto));
        }
        var updateUser = new UpdateUser
        {
            FirstName = updateUserDto.FirstName,
            LastName = updateUserDto.LastName
        };
        return _userService.UpdateAsync(userId, updateUser);
    }

    public Task<IdentityResult> AddRoleAsync(string userId, string role)
    {
        return _userService.AddRoleAsync(userId, role);
    }

    public Task<IdentityResult> RemoveRoleAsync(string userId, string role)
    {
        return _userService.RemoveRoleAsync(userId, role);
    }
}
