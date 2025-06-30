using LesAmes.Domain.Port;
using LesAmes.Dto.Users;

namespace LesAmes.Application.Services;

public class UserService
{
    private readonly IUserService _userService;

    public UserService(IUserService userService)
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
}
